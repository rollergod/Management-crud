using Management.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Management.Application.Shared.Dto
{
    public record OrderDto
    {
        [NotMapped]
        public int Id { get; set; }
        public string Number { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int ProviderId { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
    }
}
