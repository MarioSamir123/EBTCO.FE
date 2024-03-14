namespace EBTCO.FE.Contract
{
    public interface IApiService
    {
        Task<ApiResponse<T>> Call<T>(HttpMethod method, String actionUrl, Object? data = null, bool Auth = false);
    }
}
