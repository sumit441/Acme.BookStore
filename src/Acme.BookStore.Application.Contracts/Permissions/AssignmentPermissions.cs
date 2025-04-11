namespace Acme.BookStore.Permissions;

public static class AssignmentPermissions
{
    public const string GroupName = "Assignments";

    public static class Students
    {
        public const string Default = GroupName + ".Students";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Get = Default + ".Get";
        public const string GetList = Default + ".GetList";
    }

    public static class Assignments
    {
        public const string Default = GroupName + ".Assignments";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Get = Default + ".Get";
        public const string GetList = Default + ".GetList";
    }

    public static class StudentAssignments
    {
        public const string Default = GroupName + ".StudentAssignments";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Get = Default + ".Get";
        public const string GetList = Default + ".GetList";
    }
}
