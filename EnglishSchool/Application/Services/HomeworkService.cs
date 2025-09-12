using Application.Constants;
using Application.DTOs.Homework;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class HomeworkService(IUnitOfWork unitOfWork, IHomeworkRepository homeworkRepository, IMapper mapper, IOptions<CacheSettings> cacheSettings, IMemoryCache memoryCache) : IHomeworkService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHomeworkRepository _homeworkRepository = homeworkRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly int _cacheExpirationMinutes = cacheSettings.Value.DefaultExpirationMinutes;

    public async Task<Guid> CreateHomeworkAsync(CreateHomeworkDto dto)
    {
        var homework = _mapper.Map<Homework>(dto);
        await _homeworkRepository.AddAsync(homework);
        await _unitOfWork.SaveChangesAsync();

        var homeworkDto = _mapper.Map<HomeworkDto>(homework);
        _memoryCache.Set(CacheKeys.Homework, homeworkDto, TimeSpan.FromMinutes(_cacheExpirationMinutes));

        return homework.Id;
    }

    public async Task<HomeworkDto> GetHomeworkByIdAsync(Guid id)
    {
        if (_memoryCache.TryGetValue(CacheKeys.Homework, out HomeworkDto? cachedHomework))
        {
            return cachedHomework!;
        }

        var homework = await _homeworkRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Homework with ID {id} not found.");
        var homeworkDto = _mapper.Map<HomeworkDto>(homework);
        _memoryCache.Set(CacheKeys.Homework, homeworkDto, TimeSpan.FromMinutes(_cacheExpirationMinutes));

        return homeworkDto;
    }

    public async Task<IEnumerable<HomeworkDto>> GetAllHomeworksAsync()
    {
        var homeworks = await _homeworkRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<HomeworkDto>>(homeworks) ?? [];
    }

    public async Task UpdateHomeworkAsync(UpdateHomeworkDto dto)
    {
        var homework = await _homeworkRepository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException($"Homework with ID {dto.Id} not found.");
        _mapper.Map(dto, homework);
        _homeworkRepository.Update(homework);
        await _unitOfWork.SaveChangesAsync();

        _memoryCache.Remove(CacheKeys.Homework);
    }

    public async Task DeleteHomeworkAsync(Guid id)
    {
        var homework = await _homeworkRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Homework with ID {id} not found.");
        _homeworkRepository.Delete(homework);
        await _unitOfWork.SaveChangesAsync();

        _memoryCache.Remove(CacheKeys.Homework);
    }
}
