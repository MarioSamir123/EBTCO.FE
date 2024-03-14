using EBTCO.FE.Contract;
using EBTCO.FE.Feature.Sales;
namespace EBTCO.FE.Widgets.SalesOffice;
public partial class EditSalesOffice : ContentPage
{
    private readonly IApiService _apiService;
    private readonly String _apiUrl = "SalasOffice";
    private readonly Guid _officeId;
    public EditSalesOffice(IApiService apiService, Guid officeId)
	{
		InitializeComponent();
        _apiService = apiService;
        _officeId = officeId;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var url = $"{_apiUrl}/GetOne?Id={_officeId}";
        var result = await _apiService.Call<GetSalseOfficeByIdQueryResponse>(HttpMethod.Get, url);
        if (result.IsSucessful && result.Data != null) 
        { 
            SalesOfficeName.Text = result.Data.SalesOffice.OfficeName;
            BuildingNo.Text = result.Data.SalesOffice.Address.BuildingNo;
            Street.Text = result.Data.SalesOffice.Address.Street;
            City.Text = result.Data.SalesOffice.Address.City;
            State.Text = result.Data.SalesOffice.Address.State;
            ZipCode.Text = result.Data.SalesOffice.Address.ZipCode;
        }
    }

    private async void Submit_Clicked(object sender, EventArgs e)
    {
        var office = new SalesOfficeDto(_officeId,
            SalesOfficeName.Text,
            new AddressDto(BuildingNo.Text, Street.Text, City.Text, State.Text, ZipCode.Text),
            0,String.Empty);
        var result = await _apiService.Call<Object>(HttpMethod.Put, _apiUrl, office);
        if (result.IsSucessful)
        {
            await Navigation.PushAsync(new SalesOfficesIndex(_apiService));
        }
    }
}