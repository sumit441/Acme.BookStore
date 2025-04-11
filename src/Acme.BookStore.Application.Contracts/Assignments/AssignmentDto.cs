using System;

namespace Acme.BookStore.Assignments
{
    public class AssignmentDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}