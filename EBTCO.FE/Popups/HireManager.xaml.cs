using CommunityToolkit.Maui.Views;
using EBTCO.FE.Contract;
using EBTCO.FE.Feature.Employee;

namespace EBTCO.FE.Popups;

public partial class HireManager : Popup
{
    private readonly String _officeID;
    private readonly IApiService _apiService;
    public event EventHandler PopupClosed;
    public HireManager(string officeID, IApiService apiService)
    {
        InitializeComponent();
        _officeID = officeID;
        _apiService = apiService;
        FillEmployeesPicker();
    }

    private async void Hire_Clicked(object sender, EventArgs e)
    {
        var selectedEmp = (KeyValuePair<String, String>)employeesPicker.SelectedItem;
        String url = "SalasOffice/HireManager";
        Dictionary<String, String> requestData = new Dictionary<string, string>
        {
            { "employeeID", selectedEmp.Key },
            { "officeID", _officeID },
        };
        var result = await _apiService.Call<Object>(HttpMethod.Put, url, requestData);
        if (result.IsSucessful && result.Data != null)
        {
            OnPopupClosed();
            Close();
        }
    }

    private async void FillEmployeesPicker()
    {
        var url = $"Employee/Names?officeId={_officeID}";
        var result = await _apiService.Call<GetEmployeesNamesQueryResponse>(HttpMethod.Get, url);
        if (result.IsSucessful && result.Data != null)
        {
            var employees = result.Data.Employees.ToList();
            employeesPicker.ItemsSource = employees;
        }
    }
    private void Cancel_Clicked(object sender, EventArgs e) => Close();
    
    protected virtual void OnPopupClosed()
    {
        PopupClosed?.Invoke(this, EventArgs.Empty);
    }
}