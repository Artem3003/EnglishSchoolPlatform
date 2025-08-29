using AutoMapper;
using Domain.Entities;
using Application.Interfaces;
using Application.DTOs.Student;
using Domain.Interfaces;

namespace Application.Services;

public class StudentService(IUnitOfWork unitOfWork, IMapper mapper) : IStudentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<StudentDto?> GetByIdAsync(Guid id)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(id);
        return student != null ? _mapper.Map<StudentDto>(student) : null;
    }

    public async Task<IEnumerable<StudentDto>> GetAllAsync()
    {
        var students = await _unitOfWork.Students.GetAllAsync();
        return _mapper.Map<IEnumerable<StudentDto>>(students);
    }

    public async Task<StudentDto> CreateAsync(CreateStudentDto createStudentDto)
    {
        if (await _unitOfWork.Users.GetByIdAsync(createStudentDto.UserId) == null)
        {
            throw new ArgumentException("User not found or not a student");
        }

        var student = _mapper.Map<Student>(createStudentDto);
        var createdStudent = await _unitOfWork.Students.AddAsync(student);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<StudentDto>(createdStudent);
    }

    public async Task<StudentDto> UpdateAsync(UpdateStudentDto updateStudentDto)
    {
        var existingStudent = await _unitOfWork.Students.GetByIdAsync(updateStudentDto.Id) ?? throw new ArgumentException("Student not found");
        _mapper.Map(updateStudentDto, existingStudent);
        await _unitOfWork.Students.UpdateAsync(existingStudent);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<StudentDto>(existingStudent);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(id);
        if (student == null)
        {
            return false;
        }

        await _unitOfWork.Students.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
