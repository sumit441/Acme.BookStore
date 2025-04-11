using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Assignments
{
    public class CreateUpdateStudentDto
    {
        [Required]
        [StringLength(128)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}