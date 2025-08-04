using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories
{
    public class UnityRepository(AppDbContext context) : BaseRepository<Unity>(context), IUnityRepository { }
}