
using System.Text.Json.Serialization;

namespace CNTT36_WebXemPhim.DTO.Admin.Subscription
{
    public class SubscriptionDto
    {
        public int SubscriptionId { get; set; }
        public string Username { get; set; }
        public string Plan { get; set; }
        public decimal? Price { get; set; }

        [JsonIgnore]
        public DateOnly StartDate { get; set; }
        public string Start_Date => StartDate.ToString("dd/MM/yyyy");

        [JsonIgnore]
        public DateOnly EndDate { get; set; }
        public string End_Date => EndDate.ToString("dd/MM/yyyy");

        public string Status { get; set; }

        public string PaymentLink { get; set; }
    }
}