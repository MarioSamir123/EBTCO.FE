using EBTCO.FE.Contract;
using EBTCO.FE.Feature.Owner;

namespace EBTCO.FE.Widgets.Owner;

public partial class OwnersIndex : ContentPage
{
    private readonly IApiService _apiService;
    private readonly String _apiUrl = "Owner";
    public OwnersIndex(IApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var result = await _apiService.Call<GetAllOwnersQueryResponse>(HttpMethod.Get, _apiUrl);
        if (result.IsSucessful && result.Data != null) 
        {
            OwnersList.ItemsSource = result.Data.Owners;
        }
    }

    private async void Delete_Btn_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var result = await _apiService.Call<Object>(HttpMethod.Delete, $"{_apiUrl}?ownerId={btn.ClassId}");
    }

    private async void EditBtn_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        await Navigation.PushAsync(new EditOwner(_apiService, Guid.Parse(btn.ClassId)));
    }

    private async void DeleteBtn_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        String url = $"{_apiUrl}?ownerId={btn.ClassId}";
        var result = await _apiService.Call<Object>(HttpMethod.Delete, url);
        if (result.IsSucessful)
        {
            await Navigation.PushAsync(new OwnersIndex(_apiService));
        }
    }

    private async void AddOwner_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddOwner(_apiService));
    }
}