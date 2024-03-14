namespace EBTCO.FE.Contract
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public String? Error { get; set; }
        public bool IsSucessful => Data != null && Error == null;
    }
}
