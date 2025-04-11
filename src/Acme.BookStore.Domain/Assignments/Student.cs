using System;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore.Assignments
{
    public class Student : Entity<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public Student(Guid id, string name, string email) : base(id)
        {
            Name = name;
            Email = email;
        }

        public Student() { }
    }
}