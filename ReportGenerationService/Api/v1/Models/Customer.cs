using Framework.Common.Enums;
using Framework.Common.Interfaces;
using Framework.Common.Utilities;
using ReportGenerationService.Api.v1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportGenerationService.Api.v1.Models
{
    public class Customer : ICustomer, IValidate<HttpResponse>
    {
        public CustomerPreference CustomerPreference { get; set; }

        public string Name { get; set; }

        public HttpResponse Validate()
        {
            var valResult = CustomerPreference.Validate();
            if (valResult != null)
            {
                return valResult;
            }

            return null;
        }

        public CustomerPreferenceReport GenerateSingleCustomerReport(IReport<CustomerPreferenceReport, Customer> report)
        {
            return report.GenerateReportForSingle(this);
        }

        public static CustomerPreferenceReport GenerateAllCustomersReport(Customer[] customers, IReport<CustomerPreferenceReport, Customer> report)
        {
            return report.GenerateReportForAll(customers);
        }
    }
}
