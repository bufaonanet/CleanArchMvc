using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.API.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Emails is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(20, ErrorMessage = "The {0} must be least {2} and at max {1} characteres long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}