using Application.DTOs.Lesson;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Tags("Lessons Management")]
public class LessonsController(ILessonService lessonService) : ControllerBase
{
    private readonly ILessonService _lessonService = lessonService;

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateLesson([FromBody] CreateLessonDto request)
    {
        var lessonId = await _lessonService.CreateLessonAsync(request);
        return CreatedAtAction(nameof(GetLessonById), new { id = lessonId }, lessonId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LessonDto>> GetLessonById(Guid id)
    {
        var lesson = await _lessonService.GetLessonByIdAsync(id);
        return Ok(lesson);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetAllLessons()
    {
        var lessons = await _lessonService.GetAllLessonsAsync();
        return Ok(lessons);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateLesson([FromBody] UpdateLessonDto request)
    {
        await _lessonService.UpdateLessonAsync(request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLesson(Guid id)
    {
        await _lessonService.DeleteLessonAsync(id);
        return NoContent();
    }
}