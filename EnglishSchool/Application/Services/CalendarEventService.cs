using Application.Constants;
using Application.DTOs.CalendarEvent;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class CalendarEventService(IUnitOfWork unitOfWork, ICalendarEventRepository eventRepository, IMapper mapper, IOptions<CacheSettings> cacheSettings, IMemoryCache memoryCache) : ICalendarEventService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICalendarEventRepository _eventRepository = eventRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly int _cacheExpirationMinutes = cacheSettings.Value.DefaultExpirationMinutes;

    public async Task<Guid> CreateEventAsync(CreateCalendarEventDto dto)
    {
        var calendarEvent = _mapper.Map<CalendarEvent>(dto);
        await _eventRepository.AddAsync(calendarEvent);
        await _unitOfWork.SaveChangesAsync();

        var eventDto = _mapper.Map<CalendarEventDto>(calendarEvent);
        _memoryCache.Set(CacheKeys.CalendarEvents, eventDto, TimeSpan.FromMinutes(_cacheExpirationMinutes));

        return calendarEvent.Id;
    }

    public async Task<CalendarEventDto> GetEventByIdAsync(Guid id)
    {
        if (_memoryCache.TryGetValue(CacheKeys.CalendarEvents, out CalendarEventDto? cachedEvent))
        {
            return cachedEvent!;
        }

        var calendarEvent = await _eventRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Calendar event with ID {id} not found.");
        var eventDto = _mapper.Map<CalendarEventDto>(calendarEvent);
        _memoryCache.Set(CacheKeys.CalendarEvents, eventDto, TimeSpan.FromMinutes(_cacheExpirationMinutes));

        return eventDto;
    }

    public async Task UpdateEventAsync(UpdateCalendarEventDto dto)
    {
        var calendarEvent = await _eventRepository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException($"Calendar event with ID {dto.Id} not found.");
        _mapper.Map(dto, calendarEvent);
        _eventRepository.Update(calendarEvent);
        await _unitOfWork.SaveChangesAsync();

        _memoryCache.Remove(CacheKeys.CalendarEvents);
    }

    public async Task DeleteEventAsync(Guid id)
    {
        var calendarEvent = await _eventRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Calendar event with ID {id} not found.");
        _eventRepository.Delete(calendarEvent);
        await _unitOfWork.SaveChangesAsync();

        _memoryCache.Remove(CacheKeys.CalendarEvents);
    }
}
