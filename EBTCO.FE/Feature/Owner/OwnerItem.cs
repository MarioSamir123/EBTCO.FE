namespace EBTCO.FE.Feature.Owner
{
    public record OwnerDto(Guid Id, String FirstName, String LastName)
    {
        public String FullName => $"{FirstName} {LastName}";
    }
    public record GetAllOwnersQueryResponse(List<OwnerDto> Owners);
}
