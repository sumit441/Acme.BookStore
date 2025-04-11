namespace Acme.BookStore.Permissions;

public static class BookStorePermissions
{
    public const string GroupName = "BookStore";

    // other permissions...
    // other permissions...

    // *** ADDED a NEW NESTED CLASS ***
    public static class Books
    {
        public const string Default = GroupName + ".Books";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Get = Default + ".Get"; // Added
        public const string GetList = Default + ".GetList"; // Added
        public const string Update = Default + ".Update"; // Added
    }
}
