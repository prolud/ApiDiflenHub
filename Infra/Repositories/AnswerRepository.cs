using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories
{
    public class AnswerRepository(AppDbContext context) : BaseRepository<Answer>(context), IAnswerRepository { }
}