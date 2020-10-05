using ReportGenerationService.Enums;
using ReportGenerationService.Models;
using ReportGenerationService.Utilities;
using System.Linq;

namespace ReportGenerationService.ValidationProviders
{
    public class CustomerPreferencesValidations : IValidation<CustomerPreferencesForm>
    {
        /// <summary>
        /// Validate CustomerMarketInfoPreferenceForm.
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public HttpResponses Validate(CustomerPreferencesForm form)
        {          
            if (form.CustomerPreferences.Any(i => i.SpecificMonthDay < 1 || i.SpecificMonthDay > 28))
            {
                return HttpResponses.TeapotResult(ApiOffences.SpecificMonthDayBetween1And28, nameof(CustomerPreferencesModel.SpecificMonthDay));
            }

            if (form.CustomerPreferences.Any(i => i.Type == DayPreferenceType.SpecificDayOfMonth && i.SpecificMonthDay == null))
            {
                return HttpResponses.TeapotResult(ApiOffences.SpecificDayOfMonthMustHaveValue, nameof(form));
            }
  
            if (form.CustomerPreferences.Any(i => i.Type == DayPreferenceType.DaysOfWeek && i.SpecificDaysOfWeek == null))
            {
                return HttpResponses.TeapotResult(ApiOffences.SpecificDaysOfWeekMustHaveValue.ErrorCode, nameof(form));
            }

            return null;
        }
    }
}
