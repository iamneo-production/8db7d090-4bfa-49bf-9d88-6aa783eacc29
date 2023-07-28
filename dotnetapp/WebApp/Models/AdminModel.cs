using System.ComponentModel.DataAnnotations;

namespace Aloans.Model
{
    public class AdminModel
    {
        [Key]
        public int Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string mobileNumber { get; set; }
        public string userRole { get; set; }
        public string Token { get; set; }
    }
}
