using EBTCO.FE.Contract;
using EBTCO.FE.Feature.Owner;

namespace EBTCO.FE.Widgets.Owner;

public partial class AddOwner : ContentPage
{
    private readonly IApiService _apiService;
    private readonly String _apiUrl = "Owner";
    public AddOwner(IApiService apiService)
	{
		InitializeComponent();
        _apiService = apiService;
	}

    private async void Submit_Clicked(object sender, EventArgs e)
    {
        var owner = new OwnerDto(Guid.Empty, FirstName.Text, LastName.Text);
        var result = await _apiService.Call<Object>(HttpMethod.Post, _apiUrl, owner);
        if (result.IsSucessful) 
        {
            await Navigation.PushAsync(new OwnersIndex(_apiService));
        }
    }
}