using Framework.Common.Interfaces;
using ReportGenerationService.Api.v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportGenerationService.Api.v1.Interfaces
{
    public interface ICustomer
    {
        CustomerPreference CustomerPreference { get; set; }

        string Name { get; set; }

        CustomerPreferenceReport GenerateSingleCustomerReport(IReport<CustomerPreferenceReport, Customer> report);

        static CustomerPreferenceReport GenerateAllCustomersReport(Customer[] customers, IReport<CustomerPreferenceReport, Customer> report)
        {
            throw new NotImplementedException();
        }
    }
}
