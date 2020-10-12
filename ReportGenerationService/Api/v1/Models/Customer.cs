using Framework.Common.Enums;
using Framework.Common.Interfaces;
using ReportGenerationService.Api.v1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportGenerationService.Api.v1.Models
{
    public class Customer : ICustomer
    {
        public CustomerPreference CustomerPreference { get; set; }

        public string Name { get; set; }

        public CustomerPreferenceReport GenerateReport(IReport<CustomerPreferenceReport, Customer> report)
        {
            return report.GenerateReport(this);
        }
    }
}
