using EBTCO.FE.Contract;
using EBTCO.FE.Feature.Employee;

namespace EBTCO.FE.Widgets.Employee;

public partial class EditEmployee : ContentPage
{
    private readonly IApiService _apiService;
    private readonly String _apiUrl = "Employee";
    private readonly String _employeeId;
    public EditEmployee(IApiService apiService, String employeeId)
    {
        InitializeComponent();
        _apiService = apiService;
        _employeeId = employeeId;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var url = $"{_apiUrl}/GetOne?Id={_employeeId}";
        var result = await _apiService.Call<EmployeesDto>(HttpMethod.Get, url);
        if (result.IsSucessful && result.Data != null)
        {
            FirstName.Text = result.Data.FirstName;
            LastName.Text = result.Data.LastName;
            BirthDay.Date = result.Data.Birthday;
        }
    }

    private async void Submit_Clicked(object sender, EventArgs e)
    {
        var employee = new EmployeesDto(Guid.Parse(_employeeId), Guid.Empty, FirstName.Text, LastName.Text, String.Empty, BirthDay.Date);
        var result = await _apiService.Call<Object>(HttpMethod.Put, _apiUrl, employee);
        if (result.IsSucessful)
        {
            await Navigation.PushAsync(new EmployeesIndex(_apiService));
        }
    }
}