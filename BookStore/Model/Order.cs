using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Model
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int ISBN { get; set; }
        [Required]
        public int NoBook { get; set; }
        [Required]
        public double Bookprice { get; set; }
        [Required]
        public string? CustomerName { get; set; }
        [Required]
        public string? CustomerPhone { get; set; }
        [Required]
        public string? CustomerAdrress { get; set; }



    }
}
