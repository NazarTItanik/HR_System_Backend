using System.Text.Json.Serialization;

namespace HR_System.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
