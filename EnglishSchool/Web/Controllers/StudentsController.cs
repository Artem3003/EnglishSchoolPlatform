using Application.DTOs.Student;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IStudentService studentService) : ControllerBase
{
    private readonly IStudentService _studentService = studentService;

    [HttpGet("{id}")]
    [ResponseCache(Duration = 60)]
    public async Task<ActionResult<StudentDto>> GetById(Guid id)
    {
        var student = await _studentService.GetByIdAsync(id);
        return student == null ? (ActionResult<StudentDto>)NotFound() : (ActionResult<StudentDto>)Ok(student);
    }

    [HttpGet]
    [ResponseCache(Duration = 60)]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
    {
        var students = await _studentService.GetAllAsync();
        return Ok(students);
    }

    [HttpPost]
    public async Task<ActionResult<StudentDto>> Create([FromBody] CreateStudentDto createStudentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var student = await _studentService.CreateAsync(createStudentDto);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<StudentDto>> Update([FromBody] UpdateStudentDto updateStudentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var student = await _studentService.UpdateAsync(updateStudentDto);
            return Ok(student);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _studentService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}
