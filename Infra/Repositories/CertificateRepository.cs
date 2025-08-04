using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories
{
    public class CertificateRepository(AppDbContext context) : BaseRepository<Certificate>(context), ICertificateRepository { }
}