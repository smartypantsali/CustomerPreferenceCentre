using Framework.Common.Interfaces;
using Framework.Common.Utilities;
using Microsoft.AspNetCore.Mvc;
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
        /// System which takes customers preferred days to receive marketting information and returns a "report"
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        [HttpGet("CustomerPreferences")]
        public ActionResult<CustomerPreferenceReport> GetCustomerPreferencesReport(PreferenceForm preferenceForm)
        {
            var res = preferenceForm.Customers.Select(c => c.CustomerPreference.Validate())
                .Where(r => r != null).ToArray();
            if (res != null && res.Count() > 0)
            {
                // Return 418 if validation fails
                return HttpResponse.GetResultFromHttpResponses(res);
            }

            var report = new CustomerPreferenceReport();

            foreach (var customer in preferenceForm.Customers)
            {
                report = customer.GenerateReport(_report);
            }

            return report;
        }
    }
}
