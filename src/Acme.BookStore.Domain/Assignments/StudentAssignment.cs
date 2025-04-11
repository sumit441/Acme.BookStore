using System;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore.Assignments
{
    public class StudentAssignment : Entity<Guid>
    {
        public Guid StudentId { get; set; }
        public Guid AssignmentId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public StudentAssignmentStatus Status { get; set; }

        public StudentAssignment(Guid id, Guid studentId, Guid assignmentId, DateTime submissionDate) : base(id)
        {
            StudentId = studentId;
            AssignmentId = assignmentId;
            SubmissionDate = submissionDate;
        }

        public StudentAssignment() { }
    }
}

