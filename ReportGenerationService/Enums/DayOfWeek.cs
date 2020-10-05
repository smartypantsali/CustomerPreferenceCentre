using System.Text.Json.Serialization;

namespace ReportGenerationService.Enums
{
    // This exists as a hack to be able to pass in string in request body
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DayOfWeek
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }
}
