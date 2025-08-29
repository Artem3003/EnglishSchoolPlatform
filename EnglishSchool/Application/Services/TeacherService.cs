using AutoMapper;
using Application.DTOs.Teacher;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class TeacherService(IUnitOfWork unitOfWork, IMapper mapper) : ITeacherService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<TeacherDto?> GetByIdAsync(Guid id)
    {
        var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
        return teacher != null ? _mapper.Map<TeacherDto>(teacher) : null;
    }

    public async Task<IEnumerable<TeacherDto>> GetAllAsync()
    {
        var teachers = await _unitOfWork.Teachers.GetAllAsync();
        return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
    }

    public async Task<TeacherDto> CreateAsync(CreateTeacherDto createTeacherDto)
    {

        if (await _unitOfWork.Users.GetByIdAsync(createTeacherDto.UserId) == null)
        {
            throw new ArgumentException("User not found or not a teacher");
        }

        var teacher = _mapper.Map<Teacher>(createTeacherDto);
        var createdTeacher = await _unitOfWork.Teachers.AddAsync(teacher);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TeacherDto>(createdTeacher);
    }

    public async Task<TeacherDto> UpdateAsync(UpdateTeacherDto updateTeacherDto)
    {
        var existingTeacher = await _unitOfWork.Teachers.GetByIdAsync(updateTeacherDto.Id) ?? throw new ArgumentException("Teacher not found");
        _mapper.Map(updateTeacherDto, existingTeacher);
        await _unitOfWork.Teachers.UpdateAsync(existingTeacher);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TeacherDto>(existingTeacher);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
        if (teacher == null)
        {
            return false;
        }

        await _unitOfWork.Teachers.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
