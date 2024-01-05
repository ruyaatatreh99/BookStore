using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Model
{
    [Table("Review")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int ISBN { get; set; }
        public int customerid { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerReview{ get; set; }
    }
}
