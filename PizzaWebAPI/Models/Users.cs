using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaWebAPI.Models
{
    [Table("users")]
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string DeliveryAdress { get; set; } = string.Empty;
    }
}
