using System.Runtime.CompilerServices;
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
            return NotFound();
        }

        return Ok(student);
    }

    [HttpPost]
    public ActionResult<Student> Create(Student newStudent)
    {
        newStudent.Id = students.Count > 0  ? students.Max(s => s.Id) + 1 : 1;
        students.Add(newStudent);
        return CreatedAtAction(nameof(GetById), new
        {
            id = newStudent.Id
        },
        newStudent);
    }

    [HttpPut]
    public ActionResult<Student> Update(int id, Student updated)
    {
        var student = students.FirstOrDefault(s => s.Id == id);

        if (student is null)
        {
            return NotFound();
        }

        student.Name = updated.Name;
        student.Score = updated.Score;
        return Ok(student);
    }

    [HttpDelete]
    public ActionResult<Student> Delete(int id)
    {
        var student = students.FirstOrDefault(s => s.Id == id);

        if (student is null)
        {
            return NotFound();
        }

        students.Remove(student);
        return NoContent();
    }


}
    
