using Application.Constants;
using Application.DTOs.Lesson;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class LessonService(IUnitOfWork unitOfWork, ILessonRepository lessonRepository, IMapper mapper, IOptions<CacheSettings> cacheSettings, IMemoryCache memoryCache) : ILessonService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILessonRepository _lessonRepository = lessonRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly int _cacheExpirationMinutes = cacheSettings.Value.DefaultExpirationMinutes;

    public async Task<Guid> CreateLessonAsync(CreateLessonDto dto)
    {
        var lesson = _mapper.Map<Lesson>(dto);
        await _lessonRepository.AddAsync(lesson);
        await _unitOfWork.SaveChangesAsync();

        var lessonDto = _mapper.Map<LessonDto>(lesson);
        _memoryCache.Set(CacheKeys.Lessons, lessonDto, TimeSpan.FromMinutes(_cacheExpirationMinutes));

        return lesson.Id;
    }

    public async Task<LessonDto> GetLessonByIdAsync(Guid id)
    {
        if (_memoryCache.TryGetValue(CacheKeys.Lessons, out LessonDto? cachedLesson))
        {
            return cachedLesson!;
        }

        var lesson = await _lessonRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Lesson with ID {id} not found.");
        var lessonDto = _mapper.Map<LessonDto>(lesson);
        _memoryCache.Set(CacheKeys.Lessons, lessonDto, TimeSpan.FromMinutes(_cacheExpirationMinutes));

        return lessonDto;
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

        _memoryCache.Remove(CacheKeys.Lessons);
    }

    public async Task DeleteLessonAsync(Guid id)
    {
        var lesson = await _lessonRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Lesson with ID {id} not found.");
        _lessonRepository.Delete(lesson);
        await _unitOfWork.SaveChangesAsync();

        _memoryCache.Remove(CacheKeys.Lessons);
    }
}
