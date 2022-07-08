namespace TanPhucShopApi.Middleware.Exceptions
{
    public static class MessageErrors
    {
        public static string UniqueUser = "[User Has been Used]";
        public static string UniqueEmail = "Email Has been Used";
        public static string NotFound = "User Not Found";
        public static string ItemNotFound = "Item Not Found";
        public static string NoRoleAdd = "Roles is invalid";
        public static string CannotAddRole = "Cannot Add Role to User!";
        public static string UpdateUserFail = "Fail To Update User!";
    }
}
