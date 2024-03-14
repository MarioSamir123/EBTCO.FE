namespace EBTCO.FE.Feature.Employee
{
    public record EmployeesDto(Guid Id, Guid OfficeId, string FirstName, string LastName, String OfficeName, DateTime Birthday)
    {
        public string FullName => $"{FirstName} {LastName}";
        public string BirthDate => Birthday.ToString("dd MMM yyyy");
    }
    public record GetAllEmployeesQueryResponse(List<EmployeesDto> Employees);
}
