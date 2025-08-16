using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class StudentDetails
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Range(10, 100)]
        public int Age { get; set; }
    }
}
