using EBTCO.FE.Contract;
using EBTCO.FE.Feature.Owner;

namespace EBTCO.FE.Widgets.Owner;

public partial class EditOwner : ContentPage
{
    private readonly IApiService _apiService;
    private readonly String _apiUrl = "Owner";
    private readonly Guid _ownerId;
    public EditOwner(IApiService apiService, Guid ownerID)
	{
		InitializeComponent();
        _apiService = apiService;
        _ownerId = ownerID;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        String url = $"{_apiUrl}/GetOne?Id={_ownerId}";
        var result = await _apiService.Call<OwnerDto>(HttpMethod.Get, url);
        if (result.IsSucessful && result.Data != null)
        {
            FirstName.Text = result.Data.FirstName;
            LastName.Text = result.Data.LastName;
        }
    }

    private async void Submit_Clicked(object sender, EventArgs e)
    {
        var owner = new OwnerDto(_ownerId, FirstName.Text, LastName.Text);
        var result = await _apiService.Call<Object>(HttpMethod.Put, _apiUrl, owner);
        if (result.IsSucessful)
        {
            await Navigation.PushAsync(new OwnersIndex(_apiService));
        }
    }
}