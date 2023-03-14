using System.Text.Json.Serialization;

namespace Management.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }

        [JsonIgnore]
        public Provider? Provider { get; set; }
    }
}
