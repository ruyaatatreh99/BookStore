
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    [Table("Book")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int rating { get; set; }
        public int NoBook { get; set; }
        public int NoPurchased { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        public int status { get; set; }
        public List<string>? reviews { get; set; }
    }
}
