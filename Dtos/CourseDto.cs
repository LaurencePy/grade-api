using System.ComponentModel.DataAnnotations;

public class CourseDto
{
    [Required]
    public string courseName { get; set; } = "";

    [Range(0,100)]
    public int courseCredits { get; set; }
}

