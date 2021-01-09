namespace FixtureTracking.Business.Constants
{
    public static class RegexExpressions
    {
        public static string EmailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        public static string UsernameRegex = @"^(?=[a-z0-9.]{3,20}$)(?!.*[.]{2})[^.].*[^.]$";

        public static string PasswordRegex = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{6,20}$";
    }
}
