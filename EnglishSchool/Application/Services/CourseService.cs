using Application.Constants;
using Application.DTOs.Course;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class CourseService(
    IUnitOfWork unitOfWork,
    ICourseRepository courseRepository,
    IMapper mapper,
    IOptions<CacheSettings> cacheSettings,
    IMemoryCache memoryCache,
    ILogger<CourseService> logger) : ICourseService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICourseRepository _courseRepository = courseRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly ILogger<CourseService> _logger = logger;
    private readonly int _cacheExpirationMinutes = cacheSettings.Value.DefaultExpirationMinutes;

    public async Task<Guid> CreateCourseAsync(CreateCourseDto dto)
    {
        _logger.LogInformation($"Starting course creation for title: {dto.Title}");

        // Check if title already exists
        var titleExists = await _courseRepository.TitleExistsAsync(dto.Title);
        if (titleExists)
        {
            _logger.LogWarning($"Course with title '{dto.Title}' already exists");
            throw new InvalidOperationException($"Course with title '{dto.Title}' already exists.");
        }

        var course = _mapper.Map<Course>(dto);
        if (course is null)
        {
            _logger.LogError($"Course mapping failed - unable to map request to Course entity for title: {dto.Title}");
            throw new InvalidOperationException("Course cannot be null.");
        }

        _logger.LogDebug($"Mapped course DTO to entity for title: {dto.Title}");

        await _courseRepository.AddAsync(course);
        await _unitOfWork.SaveChangesAsync();

        var courseDto = _mapper.Map<CourseDto>(course);
        _memoryCache.Set(CacheKeys.Courses, courseDto, TimeSpan.FromMinutes(_cacheExpirationMinutes));
        _memoryCache.Remove(CacheKeys.TotalCoursesCount);
        _logger.LogDebug($"Cleared courses cache after creating course");

        _logger.LogInformation($"Successfully created course with ID: {course.Id}, Title: {course.Title}");

        return course.Id;
    }

    public async Task<CourseDto> GetCourseByIdAsync(Guid id)
    {
        _logger.LogInformation($"Retrieving course by ID: {id}");

        if (_memoryCache.TryGetValue(CacheKeys.Courses, out CourseDto? cachedCourse))
        {
            _logger.LogInformation($"Course found in cache for ID: {id}");
            return cachedCourse;
        }

        var course = await _courseRepository.GetByIdAsync(id);
        if (course is null)
        {
            _logger.LogError($"Course with ID {id} not found.");
            throw new KeyNotFoundException($"Course with ID {id} not found.");
        }

        _logger.LogDebug($"Course retrieved from repository for ID: {id}");

        var courseDto = _mapper.Map<CourseDto>(course);
        _memoryCache.Set(CacheKeys.Courses, courseDto, TimeSpan.FromMinutes(_cacheExpirationMinutes));
        _logger.LogDebug($"Course cached for ID: {id}");

        _logger.LogInformation($"Successfully retrieved course: {id}");

        return courseDto;
    }

    public async Task<CourseDto?> GetCourseByTitleAsync(string title)
    {
        _logger.LogInformation($"Retrieving course by title: {title}");

        var course = await _courseRepository.GetByTitleAsync(title);
        if (course is null)
        {
            _logger.LogInformation($"Course with title '{title}' not found.");
            return null;
        }

        var courseDto = _mapper.Map<CourseDto>(course);

        _logger.LogInformation($"Successfully retrieved course by title: {title}");

        return courseDto;
    }

    public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
    {
        _logger.LogInformation("Retrieving all courses");

        var courses = await _courseRepository.GetAllAsync();
        var courseDtos = _mapper.Map<IEnumerable<CourseDto>>(courses);

        _logger.LogInformation($"Successfully retrieved {courseDtos.Count()} courses");

        return courseDtos;
    }

    public async Task<IEnumerable<CourseDto>> GetAvailableCoursesAsync(Guid? excludeLessonId = null)
    {
        _logger.LogInformation($"Retrieving available courses (not at max capacity){(excludeLessonId.HasValue ? $", excluding lesson {excludeLessonId.Value}" : string.Empty)}");

        var courses = await _courseRepository.GetAvailableCoursesAsync(excludeLessonId);
        var courseDtos = _mapper.Map<IEnumerable<CourseDto>>(courses);

        _logger.LogInformation($"Successfully retrieved {courseDtos.Count()} available courses");

        return courseDtos;
    }

    public async Task UpdateCourseAsync(UpdateCourseDto dto)
    {
        _logger.LogInformation($"Starting course update for ID: {dto.Id}");

        var course = await _courseRepository.GetByIdAsync(dto.Id);
        if (course is null)
        {
            _logger.LogError($"Course with ID {dto.Id} not found.");
            throw new KeyNotFoundException($"Course with ID {dto.Id} not found.");
        }

        // Check if title already exists (excluding current course)
        if (!string.IsNullOrEmpty(dto.Title))
        {
            var titleExists = await _courseRepository.TitleExistsAsync(dto.Title, dto.Id);
            if (titleExists)
            {
                _logger.LogWarning($"Course with title '{dto.Title}' already exists");
                throw new InvalidOperationException($"Course with title '{dto.Title}' already exists.");
            }
        }

        _logger.LogDebug($"Found existing course: {course.Id}, Title: {course.Title}");

        _mapper.Map(dto, course);
        _courseRepository.Update(course);
        await _unitOfWork.SaveChangesAsync();

        _memoryCache.Remove(CacheKeys.Courses);
        _memoryCache.Remove(CacheKeys.TotalCoursesCount);
        _logger.LogDebug($"Cleared courses cache after updating course");

        _logger.LogInformation($"Successfully updated course: {course.Id}, Title: {course.Title}");
    }

    public async Task DeleteCourseAsync(Guid id)
    {
        _logger.LogInformation($"Starting course deletion for ID: {id}");

        var course = await _courseRepository.GetByIdAsync(id);
        if (course is null)
        {
            _logger.LogError($"Course with ID {id} not found.");
            throw new KeyNotFoundException($"Course with ID {id} not found.");
        }

        _courseRepository.Delete(course);
        await _unitOfWork.SaveChangesAsync();

        _memoryCache.Remove(CacheKeys.Courses);
        _memoryCache.Remove(CacheKeys.TotalCoursesCount);
        _logger.LogDebug($"Cleared courses cache after deleting course");

        _logger.LogInformation($"Successfully deleted course: {course.Id}");
    }
}
