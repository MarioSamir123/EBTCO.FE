using CommunityToolkit.Maui.Views;
using EBTCO.FE.Contract;
using EBTCO.FE.Feature.Properties;
using EBTCO.FE.Popups;

namespace EBTCO.FE.Widgets.Properties;

public partial class PropertiesIndex : ContentPage
{
	private readonly IApiService _apiService;
	private readonly string _officeId;
    private readonly string _apiUrl = "Properties";
    public PropertiesIndex(IApiService apiService, string officeId)
    {
        InitializeComponent();
        _apiService = apiService;
        _officeId = officeId;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadData();   
    }
    private async Task LoadData()
    {
        var url = $"{_apiUrl}/GetPerOfficer?Id={_officeId}";
        var result = await _apiService.Call<GetPropertiesQueryResponse>(HttpMethod.Get, url);
        if (result.IsSucessful && result.Data != null)
        {
            PropertiesList.ItemsSource = result.Data.properties;
        }
    }
    private async void AddNewProperty_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        await Navigation.PushAsync(new AddProperty(_apiService, _officeId));
    }
    private async void PopupDismissed_Handler(object sender, EventArgs e) => await LoadData();

    private async void SaleProp_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var popup = new SaleProperty(btn.ClassId, _apiService);
        popup.PopupClosed += PopupDismissed_Handler;
        await this.ShowPopupAsync(popup);
    }
}