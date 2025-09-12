namespace Application.DTOs.CalendarEvent;

public class CreateCalendarEventDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public string Type { get; set; }

    public string UserId { get; set; }
}
