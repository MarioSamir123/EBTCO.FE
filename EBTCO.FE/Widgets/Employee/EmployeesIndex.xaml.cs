using EBTCO.FE.Contract;
using EBTCO.FE.Feature.Employee;
using EBTCO.FE.Feature.Sales;

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
        await FillOfficesPicker();
        salesOfficePicker.SelectedIndex = 0;
    }
    
    private async Task LoadEmployees(String officeID)
    {
        var url = $"{_apiUrl}?officeId={officeID}";
        var result = await _apiService.Call<GetAllEmployeesQueryResponse>(HttpMethod.Get, url);

        if (result.IsSucessful && result.Data != null)
        {
            EmployeesList.ItemsSource = result.Data.Employees;
        }
    }
    private async void EditBtn_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        await Navigation.PushAsync(new EditEmployee(_apiService, btn.ClassId));
    }

    private async void DeleteBtn_Clicked(object sender, EventArgs e)
    {
        var confirm = await DisplayAlert("Warning", "Are you sure to delete this Employee", "Yes", "No");
        if (confirm)
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

    private async Task FillOfficesPicker()
    {
        var result = await _apiService.Call<GetOfficeNamesQueryResponse>(HttpMethod.Get, "SalasOffice/Names");
        if (result.IsSucessful && result.Data != null)
        {
            var offices = result.Data.Offices.ToList();
            offices.Insert(0, new KeyValuePair<string, string>("", "All"));
            salesOfficePicker.ItemsSource = offices;
        }
    }

    private async void salesOfficePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var officeItem = salesOfficePicker.SelectedItem;
        if (officeItem != null)
        {
            var office = (KeyValuePair<String, String>)officeItem;
            await LoadEmployees(office.Key);
        }
    }
}