using Application.Constants;
using Application.DTOs.HomeworkAssignment;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class HomeworkAssignmentService(IUnitOfWork unitOfWork, IHomeworkAssignmentRepository assignmentRepository, IMapper mapper, IOptions<CacheSettings> cacheSettings, IMemoryCache memoryCache) : IHomeworkAssignmentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHomeworkAssignmentRepository _assignmentRepository = assignmentRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly int _cacheExpirationMinutes = cacheSettings.Value.DefaultExpirationMinutes;

    public async Task<Guid> CreateAssignmentAsync(CreateHomeworkAssignmentDto dto)
    {
        var assignment = _mapper.Map<HomeworkAssignment>(dto);
        await _assignmentRepository.AddAsync(assignment);
        await _unitOfWork.SaveChangesAsync();

        var assignmentDto = _mapper.Map<HomeworkAssignmentDto>(assignment);

        _memoryCache.Set(CacheKeys.HomeworkAssignments, assignmentDto, TimeSpan.FromMinutes(_cacheExpirationMinutes));

        return assignment.Id;
    }

    public async Task<HomeworkAssignmentDto> GetAssignmentByIdAsync(Guid id)
    {
        if (_memoryCache.TryGetValue(CacheKeys.HomeworkAssignments, out HomeworkAssignmentDto? cachedAssignment))
        {
            return cachedAssignment!;
        }

        var assignment = await _assignmentRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Assignment with ID {id} not found.");
        var assignmentDto = _mapper.Map<HomeworkAssignmentDto>(assignment);
        _memoryCache.Set(CacheKeys.HomeworkAssignments, assignmentDto, TimeSpan.FromMinutes(_cacheExpirationMinutes));

        return assignmentDto;
    }

    public async Task<IEnumerable<HomeworkAssignmentDto>> GetAllAssignmentsAsync()
    {
        var assignments = await _assignmentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<HomeworkAssignmentDto>>(assignments) ?? [];
    }

    public async Task SubmitAssignmentAsync(Guid id, SubmitHomeworkAssignmentDto dto)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Assignment with ID {id} not found.");
        _mapper.Map(dto, assignment);
        _assignmentRepository.Update(assignment);
        await _unitOfWork.SaveChangesAsync();

        _memoryCache.Remove(CacheKeys.HomeworkAssignments);
    }

    public async Task GradeAssignmentAsync(Guid id, GradeHomeworkAssignmentDto dto)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Assignment with ID {id} not found.");
        _mapper.Map(dto, assignment);
        _assignmentRepository.Update(assignment);
        await _unitOfWork.SaveChangesAsync();

        _memoryCache.Remove(CacheKeys.HomeworkAssignments);
    }

    public async Task DeleteAssignmentAsync(Guid id)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Assignment with ID {id} not found.");
        _assignmentRepository.Delete(assignment);
        await _unitOfWork.SaveChangesAsync();

        _memoryCache.Remove(CacheKeys.HomeworkAssignments);
    }
}
