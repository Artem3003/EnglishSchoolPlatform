using Application.DTOs.CalendarEvent;
using Application.DTOs.Homework;
using Application.DTOs.HomeworkAssignment;
using Application.DTOs.Lesson;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Lesson
        CreateMap<Lesson, LessonDto>();
        CreateMap<CreateLessonDto, Lesson>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<UpdateLessonDto, Lesson>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Homework
        CreateMap<Homework, HomeworkDto>();
        CreateMap<CreateHomeworkDto, Homework>();
        CreateMap<UpdateHomeworkDto, Homework>();

        // HomeworkAssignment
        CreateMap<HomeworkAssignment, HomeworkAssignmentDto>();
        CreateMap<CreateHomeworkAssignmentDto, HomeworkAssignment>();
        CreateMap<UpdateHomeworkAssignmentDto, HomeworkAssignment>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<SubmitHomeworkAssignmentDto, HomeworkAssignment>()
            .ForMember(dest => dest.SubmittedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Domain.Entities.Enums.AssignmentStatus.Submitted))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<GradeHomeworkAssignmentDto, HomeworkAssignment>()
            .ForMember(dest => dest.GradedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Domain.Entities.Enums.AssignmentStatus.Graded))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // CalendarEvent
        CreateMap<CalendarEvent, CalendarEventDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        CreateMap<CreateCalendarEventDto, CalendarEvent>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<Domain.Entities.Enums.EventType>(src.Type.ToString())));
        CreateMap<UpdateCalendarEventDto, CalendarEvent>();
    }
}
