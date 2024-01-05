using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required(ErrorMessage = "password required")]
        [MinLength(9)]
        public string? password { get; set; }
        [Required(ErrorMessage = "Email required")]
        [EmailAddress]
        public string? email { get; set; }
    }
}
