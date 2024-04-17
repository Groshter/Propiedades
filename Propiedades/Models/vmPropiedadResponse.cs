using System.Reflection.PortableExecutable;

namespace Propiedades.Models
{
    public class vmPropiedadResponse
    {
        public ApiResponse Response { get; set; }
    }

    public class ApiResponse
    {
        public List<Property> Items { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int HttpCode { get; set; }
    }

    public class Property
    {
        public int IdProperty { get; set; }
        public string IdExternal { get; set; }
        public int IdUser { get; set; }
        public int IdClient { get; set; }
        public int IdType { get; set; }
        public int IdState { get; set; }
        public string WebAddress { get; set; }
        public bool Sale { get; set; }
        public bool Rent { get; set; }
        public bool SeasonalRental { get; set; }
        public string FirstImage { get; set; }
        public string FirstImageUrl { get; set; }
        public decimal PriceSale { get; set; }
        public decimal PriceRent { get; set; }
        public decimal PriceSeasonalRental { get; set; }
        public int IdCurrencySale { get; set; }
        public int IdCurrencyRent { get; set; }
        public int IdCurrencySeasonalRental { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public int Unit { get; set; }
        public string Letter { get; set; }
        public string Phase { get; set; }
        public string Role { get; set; }
        public int IdCondominium { get; set; }
        public int IdBorough { get; set; }
        public int IdSector { get; set; }
        public string CoordinatesLat { get; set; }
        public string CoordinatesLong { get; set; }
        public bool Featured { get; set; }
        public bool OnWeb { get; set; }
        public string Observations { get; set; }
        public string InternalObservations { get; set; }
        public string WayToVisit { get; set; }
        public bool ShowMapOnWeb { get; set; }
        public bool SendCoordinatesToPortals { get; set; }
        public bool MarkAsSale { get; set; }
        public bool MarkAsRent { get; set; }
        public bool Visible { get; set; }
        public string PropertyTitle { get; set; }
        public Currency CurrencySale { get; set; }
        public Currency CurrencyRent { get; set; }
        public Currency CurrencySeasonalRental { get; set; }
        public string PriceSaleFormatted { get; set; }
        public string PriceRentFormatted { get; set; }
        public string PriceSeasonFormatted { get; set; }
        public User User { get; set; }
        public Characteristics Characteristics { get; set; }
        public List<PropertyMedia> PropertyMedia { get; set; }
    }

    public class Currency
    {
        public int IdCurrency { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MotherLastName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public int IdBranch { get; set; }
        public string FullName { get; set; }
    }

    public class Characteristics
    {
        public decimal TotalSurface { get; set; }
        public decimal BuildedSurface { get; set; }
        public decimal UtildSurface { get; set; }
        public decimal LandSurface { get; set; }
        public decimal TerraceSurface { get; set; }
        public decimal HouseSurface { get; set; }
        public decimal OfficeSurface { get; set; }
        public decimal WarehouseSurface { get; set; }
        public decimal PastedSurface { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Privates { get; set; }
        public int Offices { get; set; }
        public int Parkings { get; set; }
        public int Warehouses { get; set; }
        public bool Pool { get; set; }
    }

    public class PropertyMedia
    {
        public int IdPropertyMedia { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public string Type { get; set; }
        public int Visibility { get; set; }
    }
}
