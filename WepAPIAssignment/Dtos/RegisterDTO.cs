using System.ComponentModel.DataAnnotations;

namespace WepAPIAssignment.Dtos
{
    public class RegisterDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        

        public string DisplayName { get; set; }
        [Required]
        //[RegularExpression("^[0-9]{8}$")] 
        public string Password { get; set; }


    }
}
