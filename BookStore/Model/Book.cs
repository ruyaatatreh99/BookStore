
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    [Table("Book")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ISBN { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double rating { get; set; }
        public int NoBook { get; set; }
        public int NoPurchased { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        public string? author { get; set; }
        [Required]
        public int status { get; set; }
        [Required]
        public double price { get; set; }

    }
}
