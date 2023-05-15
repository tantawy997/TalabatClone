using Core.entites.Identity;
using System.ComponentModel.DataAnnotations;

namespace WepAPIAssignment.Dtos
{
    public class AddressDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public string street { get; set; }
        [Required]

        public string city { get; set; }
        [Required]

        public string state { get; set; }
        [Required]

        public string zipcode { get; set; }
    }
}
