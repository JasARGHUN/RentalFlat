namespace RentalFlat_API.Helpers
{
    public class APISettings
    {
        // This model handle data from  appsettings.json/APISettings
        public string SecretKey { get; set; }
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
    }
}
