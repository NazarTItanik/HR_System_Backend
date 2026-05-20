using System.Text.Json.Serialization;

namespace HR_System.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum WageType
    {
        Monthly,
        Hourly,
        Weekly
    }
}
