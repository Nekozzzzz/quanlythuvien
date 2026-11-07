using BookManagement._3.Models;

namespace BookManagement._4.Helpers
{
    public static class SessionManager
    {
        public static User? CurrentUser { get; set; }

        public static bool IsLoggedIn => CurrentUser != null;

        public static bool IsAdmin => CurrentUser != null && string.Equals(CurrentUser.Role, "Admin", System.StringComparison.OrdinalIgnoreCase);

        public static bool IsCustomer => CurrentUser != null && string.Equals(CurrentUser.Role, "Customer", System.StringComparison.OrdinalIgnoreCase);

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
