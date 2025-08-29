using Application.DTOs.Admin;
using Application.DTOs.Student;
using Application.DTOs.Teacher;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

        // Teacher mappings
        CreateMap<Teacher, TeacherDto>();
        CreateMap<CreateTeacherDto, Teacher>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<UpdateTeacherDto, Teacher>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore());

        // Student mappings
        CreateMap<Student, StudentDto>();
        CreateMap<CreateStudentDto, Student>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<UpdateStudentDto, Student>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore());

        // Admin mappings
        CreateMap<Admin, AdminDto>();
        CreateMap<CreateAdminDto, Admin>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<UpdateAdminDto, Admin>();
    }
}
