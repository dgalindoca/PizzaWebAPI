using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaWebAPI.Models
{
    [Table("pizzas")]
    public class Pizzas
    {
        public int Id { get; set; }
        public string PizzaName { get; set; } = string.Empty;
        public string PizzaDescription { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string PizzaSize { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Stock { get; set; }
    }
}
