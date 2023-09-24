using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaWebAPI.Models
{
    [Table("courier")]
    public class Courier
    {
        [Key]
        public int Id { get; set; }
        public string CourierName { get; set; } = string.Empty;
        public string Deliveries { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
