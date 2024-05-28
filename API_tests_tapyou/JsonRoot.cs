using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace API_tests_tapyou
{
    public class JsonRoot
    {
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonPropertyName("errorCode")]
        public long ErrorCode { get; set; }

        [JsonPropertyName("errorMessage")]
        public object? ErrorMessage { get; set; }

        [JsonPropertyName("idList")]
        public long[]? IdList { get; set; }

        [JsonPropertyName("timestamp")]
        public string? Timestamp { get; set; }

        [JsonPropertyName("status")]
        public long Status { get; set; }

        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("path")]
        public string? Path { get; set; }

        [JsonPropertyName("user")]
        public User? User { get; set; }
    }

    public partial class User
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("gender")]
        public string? Gender { get; set; }

        [JsonPropertyName("age")]
        public long? Age { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("registrationDate")]
        public string? RegistrationDate { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Gender: {Gender}, Age: {Age}, City: {City}, RegistrationDate: {RegistrationDate}";
        }
    }
}

