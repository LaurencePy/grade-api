using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Score { get; set; }

    public List<int> Courses { get; set; } = [];
}



[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private static List<Student> students = new()
    {
        new Student { Id = 1, Name = "Amara Osei", Score = 72, Courses = [1] },
        new Student { Id = 2, Name = "Dev Patel", Score = 58, Courses = [1,2] },
        new Student { Id = 3, Name = "Sofia Mensah", Score = 91, Courses = [2,3] },
    };

    [HttpGet]
    public async Task<ActionResult<List<Student>>> GetAll()
    {
        var students = await _context.Students.ToListAsync();
        return Ok(students);
    }

    private readonly ILogger<StudentsController> _logger;
    private readonly AppDbContext _context;

    public StudentsController(ILogger<StudentsController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetById(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();
        return Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> Create(StudentDto newStudent)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest($"State is not valid: {ModelState}");
        }

        var student = new Student
        {
            Name = newStudent.Name,
            Score = newStudent.Score,
            Courses = newStudent.Courses
        };
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new
        {
            id = student.Id
        },
        student);
    }
public int Id { get; set; }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, StudentDto updated)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest($"State is not valid: {ModelState}");
        }

        var student = await _context.Students.FindAsync(id);

        if (student is null)
        {
            return NotFound($"No student found with id: {id}");
        }

        student.Name = updated.Name;
        student.Score = updated.Score;
        student.Courses = updated.Courses;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student is null)
        {
            return NotFound($"No student found with id: {id}");
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return NoContent();
    }



}
    
