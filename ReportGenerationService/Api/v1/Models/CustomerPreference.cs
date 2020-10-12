using Framework.Common.Enums;
using Framework.Common.Utilities;
using ReportGenerationService.Api.v1.Interfaces;

namespace ReportGenerationService.Api.v1.Models
{
    public class CustomerPreference : ICustomerPreference
    {
        public byte? SpecificMonthDay { get; set; }

        public DayOfWeek[] SpecificDaysOfWeek { get; set; }

        public DayPreferenceType Type { get; set; }

        /// <summary>
        /// Validate CustomerPreference
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public HttpResponse Validate()
        {
            if (SpecificMonthDay < 1 || SpecificMonthDay > 28)
            {
                return HttpResponse.TeapotResult(ApiOffences.SpecificMonthDayBetween1And28, nameof(SpecificMonthDay));
            }

            if (Type == DayPreferenceType.SpecificDayOfMonth && SpecificMonthDay == null)
            {
                return HttpResponse.TeapotResult(ApiOffences.SpecificDayOfMonthMustHaveValue, nameof(SpecificMonthDay));
            }

            if (Type == DayPreferenceType.DaysOfWeek && SpecificDaysOfWeek == null)
            {
                return HttpResponse.TeapotResult(ApiOffences.SpecificDaysOfWeekMustHaveValue.ErrorCode, nameof(SpecificDaysOfWeek));
            }

            return null;
        }
    }
}
