using ReportGenerationService.Models;
using System.Collections.Generic;

namespace ReportGenerationService.Gateways
{
    public interface ICustomerPreferencesGateway
    {
        public Dictionary<string, string[]> GenerateCustomerMarketInfoReport(CustomerPreferencesForm form);
    }
}
