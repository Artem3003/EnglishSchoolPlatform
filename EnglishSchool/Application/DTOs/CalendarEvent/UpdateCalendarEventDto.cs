namespace Application.DTOs.CalendarEvent;

public class UpdateCalendarEventDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
}
