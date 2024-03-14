using EBTCO.FE.Contract;
using EBTCO.FE.Feature.Sales;

namespace EBTCO.FE.Widgets.SalesOffice;

public partial class AddSalesOffice : ContentPage
{
	private readonly IApiService _apiService;
    private readonly String _apiUrl = "SalasOffice";

    public AddSalesOffice(IApiService apiService)
	{
		InitializeComponent();
		_apiService = apiService;
	}

    private async void Submit_Clicked(object sender, EventArgs e)
    {
        var office = new SalesOfficeDto(Guid.Empty, 
            SalesOfficeName.Text, 
            new AddressDto(BuildingNo.Text, Street.Text, City.Text, State.Text, ZipCode.Text),
            0,
            String.Empty);

        var result = await _apiService.Call<Object>(HttpMethod.Post, _apiUrl, office);
        if (result.IsSucessful)
        {
            await Navigation.PushAsync(new SalesOfficesIndex(_apiService));
        }
    }
}