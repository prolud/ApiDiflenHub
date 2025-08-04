using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories
{
    public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository { }
}