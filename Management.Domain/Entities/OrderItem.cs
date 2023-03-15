﻿using System.Text.Json.Serialization;

namespace Management.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; } // единица измерения

        [JsonIgnore]
        public Order? Order { get; set; }
    }
}
