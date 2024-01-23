namespace DataAccessLayer.Models.Enums
{
    public enum UserRole
    {
        User,
        Manager,
        Admin
    }

    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string User = "User";
    }
}
