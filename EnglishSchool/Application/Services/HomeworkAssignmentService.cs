using Application.DTOs.HomeworkAssignment;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class HomeworkAssignmentService(IUnitOfWork unitOfWork, IHomeworkAssignmentRepository assignmentRepository, IMapper mapper) : IHomeworkAssignmentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHomeworkAssignmentRepository _assignmentRepository = assignmentRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Guid> CreateAssignmentAsync(CreateHomeworkAssignmentDto dto)
    {
        var assignment = _mapper.Map<HomeworkAssignment>(dto);
        await _assignmentRepository.AddAsync(assignment);
        await _unitOfWork.SaveChangesAsync();
        return assignment.Id;
    }

    public async Task<HomeworkAssignmentDto> GetAssignmentByIdAsync(Guid id)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(id);
        return assignment is null
            ? throw new KeyNotFoundException($"Assignment with ID {id} not found.")
            : _mapper.Map<HomeworkAssignmentDto>(assignment);
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
    }

    public async Task GradeAssignmentAsync(Guid id, GradeHomeworkAssignmentDto dto)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Assignment with ID {id} not found.");
        _mapper.Map(dto, assignment);
        _assignmentRepository.Update(assignment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAssignmentAsync(Guid id)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Assignment with ID {id} not found.");
        _assignmentRepository.Delete(assignment);
        await _unitOfWork.SaveChangesAsync();
    }
}
