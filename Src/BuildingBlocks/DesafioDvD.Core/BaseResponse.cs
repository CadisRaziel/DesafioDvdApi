namespace DesafioDvD.Core
{
    public class BaseResponse
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; } //-> aqui sera nossos objetos (DvdResponse etc..)
    }
}
