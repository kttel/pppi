using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyLabProject
{
    public class User
    {
        [Key]
        public int Id { get; set; }

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
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Password { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastAuthorizationDate { get; set; }

        public int FailedAuthorizationAttempts { get; set; }

        [NotMapped]
        public string PlainTextPassword { get; set; }
    }
}
