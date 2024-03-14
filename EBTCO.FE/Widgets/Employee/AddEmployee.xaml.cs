using EBTCO.FE.Contract;
using EBTCO.FE.Feature.Employee;

namespace EBTCO.FE.Widgets.Employee;

public partial class AddEmployee : ContentPage
{
	private readonly Guid _officeID;
	private readonly IApiService _apiService;
    private readonly String _apiUrl = "Employee";
    public AddEmployee(Guid officeID, IApiService apiService)
	{
		InitializeComponent();
		_officeID = officeID;
		_apiService = apiService;
	}

    private async void Submit_Clicked(object sender, EventArgs e)
    {
		String firstName = FirstName.Text;
		String lastName = LastName.Text;
		var birthday = BirthDay.Date;
        var employee = new EmployeesDto(Guid.Empty, _officeID, firstName, lastName, String.Empty, birthday);
        var result = await _apiService.Call<Object>(HttpMethod.Post, _apiUrl, employee);
		await Navigation.PushAsync(new EmployeesIndex(_apiService));
    }
}