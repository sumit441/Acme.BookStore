using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Assignments
{
    public class CreateUpdateAssignmentDto
    {
        [Required]
        [StringLength(256)]
        public string? Title { get; set; }

        [StringLength(1024)]
        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}
