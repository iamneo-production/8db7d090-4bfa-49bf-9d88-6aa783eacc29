using System.ComponentModel.DataAnnotations;

namespace Loan.Models
{
    public class UserModel
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public String email { get; set; }

        [Required]
        public String password { get; set; }

        [Required]
        public String username { get; set; }

        [Required]
        [MaxLength(10)]
        public String mobileNumber { get; set; }

        [Required]
        public String userRole { get; set; }

    }

}
