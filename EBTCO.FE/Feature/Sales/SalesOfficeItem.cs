namespace EBTCO.FE.Feature.Sales
{
    public record AddressDto(String BuildingNo, String Street, String City, String State, String ZipCode)
    {
        public String GetAddress => $"{BuildingNo}, {Street}, {City}, {State}";
    }
    public record SalesOfficeDto(Guid ID, String OfficeName, AddressDto Address,int NoOfProperties, String ManagerName);
    public record GetAllSalesOfficeQueryResponse(List<SalesOfficeDto> SalesOffices);
    public record GetSalseOfficeByIdQueryResponse(SalesOfficeDto SalesOffice);
}
