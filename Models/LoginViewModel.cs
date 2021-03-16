using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class LoginViewModel
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}