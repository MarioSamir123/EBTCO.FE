using EBTCO.FE.Contract;

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
        await Navigation.PushAsync(new SalesOfficesIndex(_apiService));
    }
}