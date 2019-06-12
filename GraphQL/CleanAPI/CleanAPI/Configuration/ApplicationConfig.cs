namespace CleanAPI.Configuration
{
    public class ApplicationConfig
    {
        public LoggingConfig Logging { get; set; }
        public string ConnectionString { get; set; }
        public string ServiceBusConnectionString { get; set; }
        public string AllowedHosts { get; set; }
    }
}
