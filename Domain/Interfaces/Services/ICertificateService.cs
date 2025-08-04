namespace Domain.Interfaces.Services
{
    public interface ICertificateService
    {
        Task<bool> WasCertificateAlreadyIssued(string userId, int unityId);
    }
}