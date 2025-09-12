using Application.DTOs.Lesson;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class LessonService(IUnitOfWork unitOfWork, ILessonRepository lessonRepository, IMapper mapper) : ILessonService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    private readonly ILessonRepository _lessonRepository = lessonRepository;

    private readonly IMapper _mapper = mapper;

    public async Task<Guid> CreateLessonAsync(CreateLessonDto dto)
    {
        var lesson = _mapper.Map<Lesson>(dto);
        await _lessonRepository.AddAsync(lesson);
        await _unitOfWork.SaveChangesAsync();
        return lesson.Id;
    }

    public async Task<LessonDto> GetLessonByIdAsync(Guid id)
    {
        var lesson = await _lessonRepository.GetByIdAsync(id);
        return lesson is null ? throw new KeyNotFoundException($"Lesson with ID {id} not found.") : _mapper.Map<LessonDto>(lesson);
    }

    public async Task<IEnumerable<LessonDto>> GetAllLessonsAsync()
    {
        var lessons = await _lessonRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<LessonDto>>(lessons) ?? [];
    }

    public async Task UpdateLessonAsync(UpdateLessonDto dto)
    {
        var lesson = await _lessonRepository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException($"Lesson with ID {dto.Id} not found.");
        _mapper.Map(dto, lesson);
        _lessonRepository.Update(lesson);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteLessonAsync(Guid id)
    {
        var lesson = await _lessonRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Lesson with ID {id} not found.");
        _lessonRepository.Delete(lesson);
        await _unitOfWork.SaveChangesAsync();
    }
}
