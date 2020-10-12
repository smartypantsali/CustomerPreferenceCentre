using Framework.Common.Enums;
using Framework.Common.Interfaces;
using Framework.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportGenerationService.Api.v1.Interfaces
{
    public interface ICustomerPreference : IValidate<HttpResponse>
    {
        byte? SpecificMonthDay { get; set; }

        Framework.Common.Enums.DayOfWeek[] SpecificDaysOfWeek { get; set; }

        DayPreferenceType Type { get; set; }
    }
}
