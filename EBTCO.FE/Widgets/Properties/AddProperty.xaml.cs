using EBTCO.FE.Contract;
using EBTCO.FE.Feature.Properties;

namespace EBTCO.FE.Widgets.Properties;

public partial class AddProperty : ContentPage
{
    private readonly IApiService _apiService;
    private readonly string _officeId;
    private readonly string _apiUrl = "Properties";
    public AddProperty(IApiService apiService, string officeId)
    {
        InitializeComponent();
        _apiService = apiService;
        _officeId = officeId;
    }
    private async void Submit_Clicked(object sender, EventArgs e)
    {
        var property = new AddPropertyCommand(
            _officeId, 
            City.Text, 
            int.Parse(NoOfBedrooms.Text), 
            int.Parse(NoOfBathrooms.Text),
            double.Parse(PriceFrom.Text),
            double.Parse(PriceTo.Text));

        var result = await _apiService.Call<Object>(HttpMethod.Post, _apiUrl, property);
        
        if (result.IsSucessful && result.Data != null) 
        {
            await Navigation.PushAsync(new PropertiesIndex(_apiService, _officeId));
        }
    }
}