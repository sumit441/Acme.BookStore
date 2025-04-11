using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Assignments
{
    public class CreateUpdateStudentAssignmentDto
    {
        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public Guid AssignmentId { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; }
    }
}
