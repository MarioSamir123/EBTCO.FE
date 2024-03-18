namespace EBTCO.FE.Feature.Properties
{
    public record AddPropertyCommand(String officeId, String City, int NoOfBedroom, int NoOfBathroom, double PriceFrom, double PriceTo);
    public record PropertyItem(Guid Id, decimal PriceFrom, decimal PriceTo, int NoOfBedroom, int NoOfBathroom, PropStatus Status, String City, int OwningPer)
    {
        public String Prices => $"{PriceFrom}$ - {PriceTo}$";
        public Color Color => Status switch
        {
            PropStatus.Active => Color.FromRgb(32, 126, 82), // Green
            PropStatus.Sold => Color.FromRgb(208, 67, 81),   // Red
            PropStatus.Pending => Color.FromRgb(253, 126, 20), // Orange
            PropStatus.Hold => Color.FromRgb(208, 67, 81), // Red (assuming same as Sold)
            _ => Color.FromRgb(173, 181, 189), // Gray
        };
    }
    public record GetPropertiesQueryResponse(List<PropertyItem> properties);
}
