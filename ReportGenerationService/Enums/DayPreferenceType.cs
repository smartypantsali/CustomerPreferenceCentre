using System.Text.Json.Serialization;

namespace ReportGenerationService.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DayPreferenceType
    {
        Never,
        EveryDay,
        DaysOfWeek,
        SpecificDayOfMonth
    }
}
