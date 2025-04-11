using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace Acme.BookStore.Permissions
{
    public class RolePermissionDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IPermissionDataSeeder _permissionDataSeeder;
        private readonly IIdentityRoleRepository _roleRepository;

        public RolePermissionDataSeeder(
            IPermissionDataSeeder permissionDataSeeder,
            IIdentityRoleRepository roleRepository)
        {
            _permissionDataSeeder = permissionDataSeeder;
            _roleRepository = roleRepository;
        }

        [UnitOfWork]
        public async Task SeedAsync(DataSeedContext context)
        {
            // Seed permissions for Admin role
            var adminRole = await _roleRepository.FindByNormalizedNameAsync("ADMIN");
            if (adminRole != null)
            {
                await _permissionDataSeeder.SeedAsync(
                    RolePermissionValueProvider.ProviderName,
                    adminRole.Name,
                    new[]
                    {
                        AssignmentPermissions.Students.Create,
                        AssignmentPermissions.Students.Edit,
                        AssignmentPermissions.Students.Delete,
                        AssignmentPermissions.Students.Get,
                        AssignmentPermissions.Students.GetList,
                        AssignmentPermissions.Assignments.Create,
                        AssignmentPermissions.Assignments.Edit,
                        AssignmentPermissions.Assignments.Delete,
                        AssignmentPermissions.Assignments.Get,
                        AssignmentPermissions.Assignments.GetList,
                        AssignmentPermissions.StudentAssignments.Create,
                        AssignmentPermissions.StudentAssignments.Edit,
                        AssignmentPermissions.StudentAssignments.Delete,
                        AssignmentPermissions.StudentAssignments.Get,
                        AssignmentPermissions.StudentAssignments.GetList
                    }
                );
            }

            // Seed permissions for Teacher role
            var teacherRole = await _roleRepository.FindByNormalizedNameAsync("TEACHER");
            if (teacherRole != null)
            {
                await _permissionDataSeeder.SeedAsync(
                    RolePermissionValueProvider.ProviderName,
                    teacherRole.Name,
                    new[]
                    {
                        AssignmentPermissions.Students.Get,
                        AssignmentPermissions.Students.GetList,
                        AssignmentPermissions.Assignments.Create,
                        AssignmentPermissions.Assignments.Edit,
                        AssignmentPermissions.Assignments.Get,
                        AssignmentPermissions.Assignments.GetList,
                        AssignmentPermissions.StudentAssignments.Create,
                        AssignmentPermissions.StudentAssignments.Edit,
                        AssignmentPermissions.StudentAssignments.Get,
                        AssignmentPermissions.StudentAssignments.GetList
                    }
                );
            }

            // Seed permissions for Student role
            var studentRole = await _roleRepository.FindByNormalizedNameAsync("STUDENT");
            if (studentRole != null)
            {
                await _permissionDataSeeder.SeedAsync(
                    RolePermissionValueProvider.ProviderName,
                    studentRole.Name,
                    new[]
                    {
                        AssignmentPermissions.StudentAssignments.Get,
                        AssignmentPermissions.StudentAssignments.GetList
                    }
                );
            }
        }
    }
}
