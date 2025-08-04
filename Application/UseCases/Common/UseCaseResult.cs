using System.Net;

namespace Application.UseCases.Common
{
    public class UseCaseResult
    {
        public object? Content { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool IsSuccessStatusCode => (int)StatusCode >= 200 && (int)StatusCode < 400;
    }
}