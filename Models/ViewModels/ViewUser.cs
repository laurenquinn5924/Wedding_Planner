using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class RegisterUser : BaseEntity
    {
        [Display(Name = "First Name")]
        [Required]
        [MinLength(2)]
        public string FirstName { get; set;}

        [Display(Name = "Last Name")]
        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress] //Creates the need for user to input @ in email
        public string Email { get; set; }


        [Required]
        [MinLength(4)]
        [DataType(DataType.Password)]
        public string Password { get; set;}

        [Required]
        [Display(Name = "PW Confirm")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Confirm {get; set;}
    }

    public class LoginUser : BaseEntity
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email Not Found")]
        public string LogEmail { get; set;}

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string LogPassword { get; set;}
    }
}