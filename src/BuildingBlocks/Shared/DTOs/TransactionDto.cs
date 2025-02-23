using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class TransactionAPIResponse
    {
        public int Error { get; set; }
        public List<TransactionDto> data { get; set; }
    }

    public class TransactionDto
    {
        public int Id { get; set; }
        public string Tid { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal cusum_balance { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime When { get; set; }

        public string bank_sub_acc_id { get; set; }
        public string SubAccId { get; set; }
        public string BankName { get; set; }
        public string BankAbbreviation { get; set; }
        public string? VirtualAccount { get; set; }
        public string? VirtualAccountName { get; set; }
        public string CorresponsiveName { get; set; }
        public string CorresponsiveAccount { get; set; }
        public string CorresponsiveBankId { get; set; }
        public string CorresponsiveBankName { get; set; }
    }

    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? dateString = reader.GetString();

            if (string.IsNullOrEmpty(dateString))
            {
                return DateTime.UtcNow; // Or another appropriate default UTC time
            }

            // Parse the date and explicitly specify as UTC
            var parsedDate = DateTime.ParseExact(dateString, DateFormat, CultureInfo.InvariantCulture);
            return DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Ensure the DateTime is in UTC before writing
            DateTime utcDateTime = (value.Kind == DateTimeKind.Local)
                ? value.ToUniversalTime()
                : (value.Kind == DateTimeKind.Unspecified)
                    ? DateTime.SpecifyKind(value, DateTimeKind.Utc)
                    : value;

            writer.WriteStringValue(utcDateTime.ToString(DateFormat, CultureInfo.InvariantCulture));
        }
    }
}