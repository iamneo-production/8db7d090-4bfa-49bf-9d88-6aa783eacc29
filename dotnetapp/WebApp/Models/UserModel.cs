using System.ComponentModel.DataAnnotations;

namespace Aloans.Models
{
    public class UserModel
    {
        [Key]
        public int ID { get; set; }
        public string userRole { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public string confirmpassword { get; set; }
        public string mobileNumber { get; set; }
        public string Token { get; set; }
    }
}
