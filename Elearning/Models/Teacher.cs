using System.ComponentModel.DataAnnotations.Schema;

namespace Elearning.Models;

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Image { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }
    public string Profession { get; set; }
    public List<Courses>? Courses { get; set; }
}
