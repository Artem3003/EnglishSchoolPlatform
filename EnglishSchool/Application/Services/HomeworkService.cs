using Application.DTOs.Homework;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class HomeworkService(IUnitOfWork unitOfWork, IHomeworkRepository homeworkRepository, IMapper mapper) : IHomeworkService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHomeworkRepository _homeworkRepository = homeworkRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Guid> CreateHomeworkAsync(CreateHomeworkDto dto)
    {
        var homework = _mapper.Map<Homework>(dto);
        await _homeworkRepository.AddAsync(homework);
        await _unitOfWork.SaveChangesAsync();
        return homework.Id;
    }

    public async Task<HomeworkDto> GetHomeworkByIdAsync(Guid id)
    {
        var homework = await _homeworkRepository.GetByIdAsync(id);
        return homework is null ? throw new KeyNotFoundException($"Homework with ID {id} not found.") : _mapper.Map<HomeworkDto>(homework);
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
    }

    public async Task DeleteHomeworkAsync(Guid id)
    {
        var homework = await _homeworkRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Homework with ID {id} not found.");
        _homeworkRepository.Delete(homework);
        await _unitOfWork.SaveChangesAsync();
    }
}
