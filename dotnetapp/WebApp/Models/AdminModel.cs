using System.ComponentModel.DataAnnotations;

namespace Loans.Models
{
    public class AdminModel
    {
        [Key]
        public int ID { get; set; }
        public String email { get; set; }

        [Required]
        public String password { get; set; }

        [Required]
        [MaxLength(10)]
        public String mobileNumber { get; set; }

        [Required]
        public String userRole { get; set; }
    }
}
