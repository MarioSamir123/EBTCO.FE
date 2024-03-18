using CommunityToolkit.Maui.Views;
using EBTCO.FE.Contract;
using EBTCO.FE.Feature.Sales;
using EBTCO.FE.Popups;
using EBTCO.FE.Widgets.Employee;
using EBTCO.FE.Widgets.Properties;

namespace EBTCO.FE.Widgets.SalesOffice;

public partial class SalesOfficesIndex : ContentPage
{
    private readonly IApiService _apiService;
    private readonly String _apiUrl = "SalasOffice";
    private String _queryBody = "?SortingBy=0&SortingDir=0";
    public SalesOfficesIndex(IApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadData();
    }

    private async Task LoadData()
    {
        var result = await _apiService.Call<GetAllSalesOfficeQueryResponse>(HttpMethod.Get, $"{_apiUrl}{_queryBody}");
        if (result.IsSucessful && result.Data != null)
        {
            SalesOfficesList.ItemsSource = result.Data.SalesOffices;
        }
    }
    private async void Sort_SalesOffices(object sender, EventArgs e) 
    { 
        var btn = (Button)sender;
        var btnClassIds = btn.ClassId.Split(',');
        _queryBody = $"?SortingBy={btnClassIds[0].Trim()}&SortingDir={btnClassIds[1].Trim()}";
        await LoadData();
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

    private async void HireManager_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var popup = new HireManager(btn.ClassId, _apiService);
        popup.PopupClosed += PopupDismissed_Handler;
        await this.ShowPopupAsync<HireManager>(popup);
    }
    private async void PopupDismissed_Handler(object sender, EventArgs e) => await LoadData();
    
    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var grid = (Grid)sender;    
        await Navigation.PushAsync(new PropertiesIndex(_apiService, grid.ClassId));
    }
}