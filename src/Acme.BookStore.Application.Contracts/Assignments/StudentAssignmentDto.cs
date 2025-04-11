using System;

namespace Acme.BookStore.Assignments
{
    public class StudentAssignmentDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid AssignmentId { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
}