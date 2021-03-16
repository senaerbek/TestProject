using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}