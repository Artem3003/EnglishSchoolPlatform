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
        CreateMap<Lesson, LessonDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<CreateLessonDto, Lesson>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<Domain.Entities.Enums.LessonType>(src.Type)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<Domain.Entities.Enums.LessonStatus>(src.Status)));
        CreateMap<UpdateLessonDto, Lesson>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<Domain.Entities.Enums.LessonStatus>(src.Status)));

        // Homework
        CreateMap<Homework, HomeworkDto>();
        CreateMap<CreateHomeworkDto, Homework>();
        CreateMap<UpdateHomeworkDto, Homework>();

        // HomeworkAssignment
        CreateMap<HomeworkAssignment, HomeworkAssignmentDto>();
        CreateMap<CreateHomeworkAssignmentDto, HomeworkAssignment>();
        CreateMap<SubmitHomeworkAssignmentDto, HomeworkAssignment>();
        CreateMap<GradeHomeworkAssignmentDto, HomeworkAssignment>();

        // CalendarEvent
        CreateMap<CalendarEvent, CalendarEventDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        CreateMap<CreateCalendarEventDto, CalendarEvent>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<Domain.Entities.Enums.EventType>(src.Type)));
        CreateMap<UpdateCalendarEventDto, CalendarEvent>();
    }
}
