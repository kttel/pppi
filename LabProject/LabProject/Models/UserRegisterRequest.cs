using System.ComponentModel.DataAnnotations;

namespace LabProject
{
    public class UserRegisterRequest
    {
        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        [Required]
        [StringLength(15)]
        public string SecondName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
