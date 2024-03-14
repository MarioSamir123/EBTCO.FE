using EBTCO.FE.Contract;
using EBTCO.FE.Feature.Sales;
using EBTCO.FE.Widgets.Employee;

namespace EBTCO.FE.Widgets.SalesOffice;

public partial class SalesOfficesIndex : ContentPage
{
    private readonly IApiService _apiService;
    private readonly String _apiUrl = "SalasOffice";
    public SalesOfficesIndex(IApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var result = await _apiService.Call<GetAllSalesOfficeQueryResponse>(HttpMethod.Get, _apiUrl);
        if (result.IsSucessful && result.Data != null)
        {
            SalesOfficesList.ItemsSource = result.Data.SalesOffices;
        }
    }

    private async void AddSalesOfficeBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddSalesOffice(_apiService));
    }
    private async void AddEmployee_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        await Navigation.PushAsync(new AddEmployee(Guid.Parse(btn.ClassId), _apiService));
    }

    private async void EditBtn_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;   
        await Navigation.PushAsync(new EditSalesOffice(_apiService, Guid.Parse(btn.ClassId)));
    }
    
    private void DeleteBtn_Clicked(object sender, EventArgs e)
    {

    }
}