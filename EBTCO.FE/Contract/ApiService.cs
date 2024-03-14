using Newtonsoft.Json;

namespace EBTCO.FE.Contract
{
    public class ApiService : IApiService
    {
        private readonly String _baseUrl = "https://localhost:44323/api";
        public async Task<ApiResponse<T>> Call<T>(HttpMethod method, string actionUrl, object? data = null, bool Auth = false)
        {
            var client = new HttpClient();

            String Url = $"{_baseUrl}/{actionUrl}";
            var httpRequest = new HttpRequestMessage(method, Url);
            if (Auth)
            {
                String token = String.Empty;
                httpRequest.Headers.Add("Authorization", $"Bearer {token}");
            }

            if (data != null)
            {
                var requestBody = JsonConvert.SerializeObject(data);
                httpRequest.Content = new StringContent(requestBody, null, "application/json");
            }
            var response = await client.SendAsync(httpRequest);
            var value = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse<T>
                {
                    Data = JsonConvert.DeserializeObject<T>(value)
                };
            }
            return new ApiResponse<T> 
            { 
                Error = JsonConvert.DeserializeObject<List<String>>(value)?.FirstOrDefault() ?? string.Empty,
            };
        }
    }
}
