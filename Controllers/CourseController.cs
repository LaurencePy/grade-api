using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/course/[controller]")]
public class CourseController : ControllerBase
{
    private static List<Course> courses = new()
    {
        new Course { courseId = 1, courseName = "Software Engineering 1", courseCredits = 10 },
        new Course { courseId = 2, courseName = "Operating Systems", courseCredits = 20 },
        new Course { courseId = 3, courseName = "Functional Programming", courseCredits = 15 },
    };

    [HttpGet]
    public ActionResult<List<Course>> GetAllCourses()
    {
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public ActionResult<Course> GetById(int id)
    {
        var course = courses.FirstOrDefault(s => s.courseId == id);

        if (course is null)
        {
            return NotFound($"No course found with id: {id}");
        }

        return Ok(course);
    }

    [HttpPost]
    public ActionResult<Course> Create(CourseDto newCourse)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest($"State is not valid: {ModelState}");
        }

        var course = new Course
        {
            courseId = courses.Count > 0 ? courses.Max(s => s.courseId) + 1 : 1,
            courseName = newCourse.courseName,
            courseCredits = newCourse.courseCredits
        };
        courses.Add(course);
        return CreatedAtAction(nameof(GetById), new
        {
            id = course.courseId
        },
        course);
    }

    [HttpPut("{id}")]
    public ActionResult<Course> Update(int id, CourseDto updated)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest($"State is not valid: {ModelState}");
        }

        var course = courses.FirstOrDefault(s => s.courseId == id);

        if (course is null)
        {
            return NotFound($"No course found with id: {id}");
        }

        course.courseName = updated.courseName;
        course.courseCredits = updated.courseCredits;
        return NoContent();
    }

    [HttpDelete]
    public ActionResult<Course> Delete(int id)
    {
        var course = courses.FirstOrDefault(s => s.courseId == id);

        if (course is null)
        {
            return NotFound($"No course found with id: {id}");
        }

        courses.Remove(course);
        return NoContent();
    }


}