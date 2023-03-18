using System.Text.Json.Serialization;

namespace Management.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int ProviderId { get; set; }

        [JsonIgnore]
        public Provider? Provider { get; set; }

        //Delete if not working
        // [JsonIgnore]
        public ICollection<OrderItem> Items { get; set; }
    }
}
