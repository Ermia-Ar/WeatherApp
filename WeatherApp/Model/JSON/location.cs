
namespace WeatherApp.JSON
{
    public class L10n
    {
        public string currencyName { get; set; }
        public string currencyCode { get; set; }
        public string currencySymbol { get; set; }
        public List<string> langCodes { get; set; }
    }

    public class LocationData
    {
        public string countryName { get; set; }
        public string countryCode { get; set; }
        public string stateName { get; set; }
        public string stateCode { get; set; }
        public string cityName { get; set; }
        public int cityGeonamesId { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string tz { get; set; }
        public string continentCode { get; set; }
    }

    public class Root1
    {
        public string ip { get; set; }
        public bool isEuropeanUnion { get; set; }
        public L10n l10n { get; set; }
        public LocationData locationData { get; set; }
    }


}
