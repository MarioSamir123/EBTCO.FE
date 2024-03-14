using EBTCO.FE.Contract;
using EBTCO.FE.Feature.Employee;

namespace EBTCO.FE.Widgets.Employee;

public partial class EmployeesIndex : ContentPage
{
    private readonly IApiService _apiService;
    private readonly String _apiUrl = "Employee";
	public EmployeesIndex(IApiService apiService)
	{
		InitializeComponent();
        _apiService = apiService;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var result = await _apiService.Call<GetAllEmployeesQueryResponse>(HttpMethod.Get, _apiUrl);
        if (result.IsSucessful && result.Data != null)
        {
            EmployeesList.ItemsSource = result.Data.Employees;
        }
    }

    private void EditBtn_Clicked(object sender, EventArgs e)
    {

    }

    private async void DeleteBtn_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        String url = $"{_apiUrl}?Id={btn.ClassId}";
        var result = await _apiService.Call<Object>(HttpMethod.Delete, url);
        if (result.IsSucessful)
        {
            await Navigation.PushAsync(new EmployeesIndex(_apiService));
        }
    }
}