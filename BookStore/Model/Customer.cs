using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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
        [Required(ErrorMessage = "phone required")]
        [MinLength(9)]
        public string? phone { get; set; }
        [Required(ErrorMessage = "Email required")]
        [EmailAddress]
        public string? email { get; set; }
        [Required]
        public int TotalNoBook { get; set; }
        [Required]
        public double Totalprice { get; set; }
        public string? address { get; set; }
    }
}
