using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Assignments
{
    public class StudentAssignmentDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Student, Guid> _studentRepository;
        private readonly IRepository<Assignment, Guid> _assignmentRepository;
        private readonly IRepository<StudentAssignment, Guid> _studentAssignmentRepository;

        public StudentAssignmentDataSeeder(
            IRepository<Student, Guid> studentRepository,
            IRepository<Assignment, Guid> assignmentRepository,
            IRepository<StudentAssignment, Guid> studentAssignmentRepository)
        {
            _studentRepository = studentRepository;
            _assignmentRepository = assignmentRepository;
            _studentAssignmentRepository = studentAssignmentRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            // Seed Students
            var student1 = await _studentRepository.InsertAsync(new Student(Guid.NewGuid(), "John Doe", "john.doe@example.com"));
            var student2 = await _studentRepository.InsertAsync(new Student(Guid.NewGuid(), "Jane Smith", "jane.smith@example.com"));

            // Seed Assignments
            var assignment1 = await _assignmentRepository.InsertAsync(new Assignment(Guid.NewGuid(), "Math Homework", "Solve 10 algebra problems", DateTime.Now.AddDays(7)));
            var assignment2 = await _assignmentRepository.InsertAsync(new Assignment(Guid.NewGuid(), "Science Project", "Build a model volcano", DateTime.Now.AddDays(14)));

            // Seed StudentAssignments
            await _studentAssignmentRepository.InsertAsync(new StudentAssignment(Guid.NewGuid(), student1.Id, assignment1.Id, DateTime.Now.AddDays(5)));
            await _studentAssignmentRepository.InsertAsync(new StudentAssignment(Guid.NewGuid(), student2.Id, assignment2.Id, DateTime.Now.AddDays(10)));
        }
    }
}