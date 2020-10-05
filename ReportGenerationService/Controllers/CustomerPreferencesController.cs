using Microsoft.AspNetCore.Mvc;
using ReportGenerationService.Gateways;
using ReportGenerationService.Models;
using ReportGenerationService.ValidationProviders;
using System.Collections.Generic;

namespace ReportGenerationService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerPreferencesController : Controller
    {
        private readonly ICustomerPreferencesGateway _customerMarketInfoGateway;
        private readonly IValidation<CustomerPreferencesForm> _cusomerPreferencesValidations;

        public CustomerPreferencesController(ICustomerPreferencesGateway customerPreferencesGateway, IValidation<CustomerPreferencesForm> cusomerPreferencesValidations)
        {
            _customerMarketInfoGateway = customerPreferencesGateway;
            _cusomerPreferencesValidations = cusomerPreferencesValidations;
        }

        /// <summary>
        /// System which takes customers preferred days to receive marketting information and returns a "report"
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpGet("")]
        public ActionResult<Dictionary<string, string[]>> GetCustomerMarketInfoReport(CustomerPreferencesForm form)
        {
            var res = _cusomerPreferencesValidations.Validate(form);
            if (res != null)
            {
                // Return 418 if validation fails
                return res;
            }

            // TODO: Can create a new service to handle generation of report
            var report = _customerMarketInfoGateway.GenerateCustomerMarketInfoReport(form);

            return report;
        }
    }
}
