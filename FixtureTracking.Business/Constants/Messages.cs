using System;

namespace FixtureTracking.Business.Constants
{
    public static class Messages
    {
        /// <summary>
        /// Service Messages
        /// </summary>
        public static string AuthUserRegistered = "User registered";
        public static string AuthEmailExists = "Email already exists";
        public static string AuthUsernameExists = "Username already exists";
        public static string AuthUserNotFound = "Email or password wrong";

        public static string CategoryAdded = "Category added";
        public static string CategoryUpdated = "Category updated";
        public static string CategoryDeleted = "Category deleted";
        public static string CategoryNotFound = "Category not found";

        public static string DebitAdded = "Debit added";
        public static string DebitUpdated = "Debit updated";
        public static string DebitDeleted = "Debit deleted";
        public static string DebitNotFound = "Debit not found";

        public static string DepartmentAdded = "Department added";
        public static string DepartmentUpdated = "Department updated";
        public static string DepartmentClaimUpdated = "Department - Operation Claims updated";
        public static string DepartmentDeleted = "Department deleted";
        public static string DepartmentNotFound = "Department not found";

        public static string FixtureAdded = "Fixture added";
        public static string FixtureUpdated = "Fixture updated";
        public static string FixtureDeleted = "Fixture deleted";
        public static string FixtureWasNotDeleted = "Fixture was not deleted";
        public static string FixtureNotFound = "Fixture not found";

        public static string SupplierAdded = "Supplier added";
        public static string SupplierUpdated = "Supplier updated";
        public static string SupplierDeleted = "Supplier deleted";
        public static string SupplierNotFound = "Supplier not found";

        public static string UserDeleted = "User deleted";
        public static string UserNotFound = "User not found";

        /// <summary>
        /// Business Aspect Messages
        /// </summary>
        public static string AuthorizationDenied = "Authorization denied";

        /// <summary>
        /// Validation Messages
        /// </summary>
        public static string EmailIsNotValid = "'Email' is not a valid email address.";
        public static string UsernameIsNotValid = "'Username' is not a valid username. 'Username' must be 3-20 characters and contain only lowercase letters and dots.";
        public static string PasswordIsNotValid = "'Password' is not a valid password. 'Password' must be 6-20 characters, include at least one lowercase letter, one uppercase letter, a special character and a digit.";
        public static string BirthDateIsNotValid_LessThan(DateTime dateLessThan) => $"'Birth Date' must be less than '{dateLessThan.ToShortDateString()}'.";
        public static string BirthDateIsNotValid_GreaterThan(DateTime dateGreaterThan) => $"'Birth Date' must be greater than '{dateGreaterThan.ToShortDateString()}'.";
    }
}
