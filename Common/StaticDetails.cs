namespace Common
{
    public static class StaticDetails
    {
        public const string Role_Admin = "Admin";
        public const string Role_Customer = "Customer";
        public const string Role_Employee = "Employee";

        public const string Local_InitialBooking = "InitialFlatBookingInfo"; // This prop need to localstorage in the browser. Use in RentalApp.Client/Index.razor
        public const string Local_FlatOrderDetails = "FlatOrderDetails";
        public const string Local_Token = "JWT Token";
        public const string Local_UserDetails = "User Details";

        public const string Status_Pending = "Pending";
        public const string Status_Booked = "Booked";
        public const string Status_CheckedIn = "CheckedIn";
        public const string Status_CheckedOut_Completed = "CheckedOut";
        public const string Status_NoShow = "NoShow";
        public const string Status_Cancelled = "Cancelled";
    }
}
