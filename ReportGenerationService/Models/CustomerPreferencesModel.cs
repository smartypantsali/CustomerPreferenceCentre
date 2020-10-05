using ReportGenerationService.Enums;

namespace ReportGenerationService.Models
{
    public class CustomerPreferencesModel
    {
        public byte? SpecificMonthDay { get; set; }

        public DayOfWeek[] SpecificDaysOfWeek { get; set; }

        public DayPreferenceType Type { get; set; }

        public string Customer { get; set; }
    }
}
