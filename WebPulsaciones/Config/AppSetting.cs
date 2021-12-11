namespace WebPulsaciones.Config
{
    public class AppSetting
    {
        public string Secret { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public int ExpiryInMinutes { get; set; }
        
    }
}
