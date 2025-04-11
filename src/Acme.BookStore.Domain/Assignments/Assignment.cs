using System;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore.Assignments
{
    public class Assignment : Entity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public Assignment(Guid id, string title, string description, DateTime dueDate) : base(id)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
        }

        public Assignment() { }
    }
}