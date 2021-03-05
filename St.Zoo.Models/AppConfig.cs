namespace St.Zoo.Models
{
    public class AppConfig
    {
        public AppSettings AppSettings { get; set; }

        public AppProfile AppProfile { get; set; }

        public AppStore AppStore { get; set; }

    }

    public class AppSettings
    {
        public string ApplicationName { get; set; }
    }

    public class AppProfile
    {
        public string Label { get; set; }
    }

    public class AppStore
    {
        public string PricesFilePath { get; set; }

        public string AnimalsFilePath { get; set; }

        public string SpeciesFilePath { get; set; }
    }
}
