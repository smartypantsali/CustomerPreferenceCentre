using Framework.Common.Interfaces;
using Framework.Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using ReportGenerationService.Api.v1.Interfaces;
using ReportGenerationService.Api.v1.Models;
using System.Collections.Generic;
using System.Linq;

namespace ReportGenerationService.Api.v1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportController : Controller
    {
        private readonly IReport<CustomerPreferenceReport, Customer> _report;

        public ReportController(IReport<CustomerPreferenceReport, Customer> report)
        {
            _report = report;
        }

        /// <summary>
        /// System which takes a single customers preferred days to receive marketting information and returns a "report"
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        [HttpGet("SingleCustomerPreferences")]
        public ActionResult<CustomerPreferenceReport> GetSingleCustomerPreferencesReport(CustomersForm form)
        {
            // Validation
            if (form.Customer == null)
            {
                return HttpResponse.TeapotResult(ApiOffences.CustomerCannotBeNull, nameof(Customer));
            }

            var valResult = form.Customer.Validate();
            if (valResult != null)
            {
                return valResult;
            }

            return form.Customer.GenerateSingleCustomerReport(_report);
        }

        /// <summary>
        /// System which takes a multiple customers preferred days to receive marketting information and returns a "report"
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        [HttpGet("AllCustomerPreferences")]
        public ActionResult<CustomerPreferenceReport> GetAllCustomerPreferencesReport(CustomersForm form)
        {
            // Validation
            var valResult = form.Customers.Select(c => c.Validate())
                .Where(r => r != null).ToArray();
            if (valResult.Count() > 0)
            {
                return HttpResponse.GetResultFromHttpResponses(valResult);
            }

            return Customer.GenerateAllCustomersReport(form.Customers, _report);
        }
    }
}
