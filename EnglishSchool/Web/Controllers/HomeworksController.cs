using Application.DTOs.Homework;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Tags("Homework Management")]
public class HomeworksController(IHomeworkService homeworkService) : ControllerBase
{
    private readonly IHomeworkService _homeworkService = homeworkService;

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateHomework([FromBody] CreateHomeworkDto request)
    {
        var homeworkId = await _homeworkService.CreateHomeworkAsync(request);
        return CreatedAtAction(nameof(GetHomeworkById), new { id = homeworkId }, homeworkId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HomeworkDto>> GetHomeworkById(Guid id)
    {
        var homework = await _homeworkService.GetHomeworkByIdAsync(id);
        return Ok(homework);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HomeworkDto>>> GetAllHomeworks()
    {
        var homeworks = await _homeworkService.GetAllHomeworksAsync();
        return Ok(homeworks);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateHomework([FromBody] UpdateHomeworkDto request)
    {
        await _homeworkService.UpdateHomeworkAsync(request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteHomework(Guid id)
    {
        await _homeworkService.DeleteHomeworkAsync(id);
        return NoContent();
    }
}