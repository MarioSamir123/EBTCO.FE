using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using EBTCO.FE.Contract;
using EBTCO.FE.Feature.Owner;
namespace EBTCO.FE.Popups;

public partial class SaleProperty : Popup
{
    private readonly String _propID;
    private readonly IApiService _apiService;
    public event EventHandler PopupClosed;
    public SaleProperty(string propID, IApiService apiService)
    {
        InitializeComponent();
        _propID = propID;
        _apiService = apiService;
        FillOwnersPicker();
    }
    private async void FillOwnersPicker()
    {
        var url = $"Owner";
        var result = await _apiService.Call<GetAllOwnersQueryResponse>(HttpMethod.Get, url);
        if (result.IsSucessful && result.Data != null)
        {
            var owners = result.Data.Owners.Select(row => new KeyValuePair<String, String>(row.Id.ToString(), row.FullName)).ToList();
            ownersPicker.ItemsSource = owners;
        }
    }
    private void Cancel_Clicked(object sender, EventArgs e) => Close();

    protected virtual void OnPopupClosed()
    {
        PopupClosed?.Invoke(this, EventArgs.Empty);
    }
    private async void Sale_Clicked(object sender, EventArgs e)
    {
        if (int.TryParse(OwnPer.Text, out int per) && per > 0 && per <= 100)
        {
            var owner = ( KeyValuePair<String, String>)ownersPicker.SelectedItem;
            Dictionary<String, Object> requestData = new Dictionary<String, Object>()
            {
                { "ownerId", owner.Key },
                { "propId", _propID },
                { "percentage", per },
            };

            var url = "Properties/Sale";
            var result = await _apiService.Call<Object>(HttpMethod.Post, url, requestData);
            if (result.IsSucessful && result.Data != null)
            {
                OnPopupClosed();
                Close();
            }
            if (result.Error != null)
            {
                await Toast.Make(result.Error, CommunityToolkit.Maui.Core.ToastDuration.Short, 18).Show();
            }
        }
    }
}