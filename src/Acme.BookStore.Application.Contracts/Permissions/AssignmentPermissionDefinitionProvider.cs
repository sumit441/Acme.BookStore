using Acme.BookStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.BookStore.Permissions;

public class AssignmentPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var assignmentGroup = context.AddGroup(AssignmentPermissions.GroupName, L("Permission:Assignments"));

        // Students Permissions
        var studentsPermission = assignmentGroup.AddPermission(AssignmentPermissions.Students.Default, L("Permission:Students"));
        studentsPermission.AddChild(AssignmentPermissions.Students.Create, L("Permission:Create"));
        studentsPermission.AddChild(AssignmentPermissions.Students.Edit, L("Permission:Edit"));
        studentsPermission.AddChild(AssignmentPermissions.Students.Delete, L("Permission:Delete"));
        studentsPermission.AddChild(AssignmentPermissions.Students.Get, L("Permission:Get"));
        studentsPermission.AddChild(AssignmentPermissions.Students.GetList, L("Permission:GetList"));

        // Assignments Permissions
        var assignmentsPermission = assignmentGroup.AddPermission(AssignmentPermissions.Assignments.Default, L("Permission:Assignments"));
        assignmentsPermission.AddChild(AssignmentPermissions.Assignments.Create, L("Permission:Create"));
        assignmentsPermission.AddChild(AssignmentPermissions.Assignments.Edit, L("Permission:Edit"));
        assignmentsPermission.AddChild(AssignmentPermissions.Assignments.Delete, L("Permission:Delete"));
        assignmentsPermission.AddChild(AssignmentPermissions.Assignments.Get, L("Permission:Get"));
        assignmentsPermission.AddChild(AssignmentPermissions.Assignments.GetList, L("Permission:GetList"));

        // StudentAssignments Permissions
        var studentAssignmentsPermission = assignmentGroup.AddPermission(AssignmentPermissions.StudentAssignments.Default, L("Permission:StudentAssignments"));
        studentAssignmentsPermission.AddChild(AssignmentPermissions.StudentAssignments.Create, L("Permission:Create"));
        studentAssignmentsPermission.AddChild(AssignmentPermissions.StudentAssignments.Edit, L("Permission:Edit"));
        studentAssignmentsPermission.AddChild(AssignmentPermissions.StudentAssignments.Delete, L("Permission:Delete"));
        studentAssignmentsPermission.AddChild(AssignmentPermissions.StudentAssignments.Get, L("Permission:Get"));
        studentAssignmentsPermission.AddChild(AssignmentPermissions.StudentAssignments.GetList, L("Permission:GetList"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookStoreResource>(name);
    }
}