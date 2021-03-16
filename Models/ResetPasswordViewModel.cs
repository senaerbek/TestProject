using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage="Şifreler Eşleşmiyor")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}