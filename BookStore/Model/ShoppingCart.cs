using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; }
            [Required]
            public int ISBN { get; set; }
            [Required]
            public int CustomerID { get; set; }
            [Required]
            public int NoBook { get; set; }
            [Required]
            public double Bookprice { get; set; }
        

    }
    }
