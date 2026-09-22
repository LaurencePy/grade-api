using Microsoft.AspNetCore.Mvc;
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Score { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private static List<Student> students = new()
    {
        new Student { Id = 1, Name = "Amara Osei", Score = 72 },
        new Student { Id = 2, Name = "Dev Patel", Score = 58 },
        new Student { Id = 3, Name = "Sofia Mensah", Score = 91 },
    };

    [HttpGet]
    public ActionResult<List<Student>> GetAll()
    {
        return Ok(students);
    }

    [HttpGet("{id}")]
    public ActionResult<Student> GetById(int id)
    {
        var student = students.FirstOrDefault(s => s.Id == id);

        if (student is null)
        {
            return NotFound($"No student found with id: {id}");
        }

        return Ok(student);
    }

    [HttpPost]
    public ActionResult<Student> Create(StudentDto newStudent)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest($"State is not valid: {ModelState}");
        }

        var student = new Student
        {
            Id = students.Count > 0 ? students.Max(s => s.Id) + 1 : 1,
            Name = newStudent.Name,
            Score = newStudent.Score
        };
        students.Add(student);
        return CreatedAtAction(nameof(GetById), new
        {
            id = student.Id
        },
        student);
    }

    [HttpPut("{id}")]
    public ActionResult<Student> Update(int id, StudentDto updated)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest($"State is not valid: {ModelState}");
        }

        var student = students.FirstOrDefault(s => s.Id == id);

        if (student is null)
        {
            return NotFound($"No student found with id: {id}");
        }

        student.Name = updated.Name;
        student.Score = updated.Score;
        return NoContent();
    }

    [HttpDelete]
    public ActionResult<Student> Delete(int id)
    {
        var student = students.FirstOrDefault(s => s.Id == id);

        if (student is null)
        {
            return NotFound($"No student found with id: {id}");
        }

        students.Remove(student);
        return NoContent();
    }


}
    
