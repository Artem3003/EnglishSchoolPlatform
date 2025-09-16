using Domain.Data;
using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Repositories;

public class LessonRepository(ApplicationDbContext context) : AbstractRepository<Lesson>(context), ILessonRepository
{
}
