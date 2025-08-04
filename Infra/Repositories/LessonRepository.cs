using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infra.Repositories
{
    public class LessonRepository(AppDbContext context) : BaseRepository<Lesson>(context), ILessonRepository { }
}