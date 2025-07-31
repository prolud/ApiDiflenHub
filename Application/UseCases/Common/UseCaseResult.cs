using System.Net;

namespace Application.UseCases.Common
{
    public class UseCaseResult<T>
    {
        public T? Content { get; set; }
        public string Message { get; set; } = string.Empty;
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessStatusCode => (int)StatusCode >= 200 && (int)StatusCode < 400;
    }
}