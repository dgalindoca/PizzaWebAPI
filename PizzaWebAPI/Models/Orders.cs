using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaWebAPI.Models
{
    [Table("orders")]
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        public int USerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public int OrderStatus { get; set; }
        public string? OrderComments { get; set; }
        public string PaymentMethod { get; set; } = String.Empty;
        public string PaymentStatus { get; set; } = String.Empty;
        public int CourierId { get; set; }

    }
}
