using System.ComponentModel.DataAnnotations;

namespace iStolo1.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
