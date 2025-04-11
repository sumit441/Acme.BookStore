using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Acme.BookStore.Assignments
{
    public class StudentAssignmentDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Student, Guid> _studentRepository;
        private readonly IRepository<Assignment, Guid> _assignmentRepository;
        private readonly IRepository<StudentAssignment, Guid> _studentAssignmentRepository;
        private readonly IGuidGenerator _guidGenerator;

        public StudentAssignmentDataSeeder(
            IRepository<Student, Guid> studentRepository,
            IRepository<Assignment, Guid> assignmentRepository,
            IRepository<StudentAssignment, Guid> studentAssignmentRepository, IGuidGenerator guidGenerator)
        {
            _studentRepository = studentRepository;
            _assignmentRepository = assignmentRepository;
            _studentAssignmentRepository = studentAssignmentRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            // Seed Students
            var student1 = await _studentRepository.InsertAsync(new Student(_guidGenerator.Create(), "John Doe", "john.doe@example.com"));
            var student2 = await _studentRepository.InsertAsync(new Student(_guidGenerator.Create(), "Jane Smith", "jane.smith@example.com"));

            // Seed Assignments
            var assignment1 = await _assignmentRepository.InsertAsync(new Assignment(_guidGenerator.Create(), "Math Homework", "Solve 10 algebra problems", DateTime.Now.AddDays(7)));
            var assignment2 = await _assignmentRepository.InsertAsync(new Assignment(_guidGenerator.Create(), "Science Project", "Build a model volcano", DateTime.Now.AddDays(14)));

            // Seed StudentAssignments
            await _studentAssignmentRepository.InsertAsync(new StudentAssignment(_guidGenerator.Create(), student1.Id, assignment1.Id, DateTime.Now.AddDays(5)));
            await _studentAssignmentRepository.InsertAsync(new StudentAssignment(_guidGenerator.Create(), student2.Id, assignment2.Id, DateTime.Now.AddDays(10)));
        }
    }
}