using Application.DTOs.Teacher;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachersController(ITeacherService teacherService) : ControllerBase
{
    private readonly ITeacherService _teacherService = teacherService;

    [HttpGet("{id}")]
    [ResponseCache(Duration = 60)]
    public async Task<ActionResult<TeacherDto>> GetById(Guid id)
    {
        var teacher = await _teacherService.GetByIdAsync(id);
        return teacher == null ? (ActionResult<TeacherDto>)NotFound() : (ActionResult<TeacherDto>)Ok(teacher);
    }

    [HttpGet]
    [ResponseCache(Duration = 60)]
    public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAll()
    {
        var teachers = await _teacherService.GetAllAsync();
        return Ok(teachers);
    }

    [HttpPost]
    public async Task<ActionResult<TeacherDto>> Create([FromBody] CreateTeacherDto createTeacherDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var teacher = await _teacherService.CreateAsync(createTeacherDto);
            return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, teacher);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<TeacherDto>> Update([FromBody] UpdateTeacherDto updateTeacherDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var teacher = await _teacherService.UpdateAsync(updateTeacherDto);
            return Ok(teacher);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _teacherService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}
