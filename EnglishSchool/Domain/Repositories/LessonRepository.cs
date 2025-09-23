using Domain.Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories;

public class LessonRepository(ApplicationDbContext context) : AbstractRepository<Lesson>(context), ILessonRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<int> CountLessonsAsync()
    {
        return await _context.Lessons.CountAsync();
    }
}
