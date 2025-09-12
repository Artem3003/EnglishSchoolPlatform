using Application.DTOs.CalendarEvent;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class CalendarEventService(IUnitOfWork unitOfWork, ICalendarEventRepository eventRepository, IMapper mapper) : ICalendarEventService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICalendarEventRepository _eventRepository = eventRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Guid> CreateEventAsync(CreateCalendarEventDto dto)
    {
        var calendarEvent = _mapper.Map<CalendarEvent>(dto);
        await _eventRepository.AddAsync(calendarEvent);
        await _unitOfWork.SaveChangesAsync();
        return calendarEvent.Id;
    }

    public async Task<CalendarEventDto> GetEventByIdAsync(Guid id)
    {
        var calendarEvent = await _eventRepository.GetByIdAsync(id);
        return calendarEvent is null
            ? throw new KeyNotFoundException($"Calendar event with ID {id} not found.")
            : _mapper.Map<CalendarEventDto>(calendarEvent);
    }

    public async Task UpdateEventAsync(UpdateCalendarEventDto dto)
    {
        var calendarEvent = await _eventRepository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException($"Calendar event with ID {dto.Id} not found.");
        _mapper.Map(dto, calendarEvent);
        _eventRepository.Update(calendarEvent);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteEventAsync(Guid id)
    {
        var calendarEvent = await _eventRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Calendar event with ID {id} not found.");
        _eventRepository.Delete(calendarEvent);
        await _unitOfWork.SaveChangesAsync();
    }
}
