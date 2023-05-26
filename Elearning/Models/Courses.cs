using System.ComponentModel.DataAnnotations.Schema;

namespace Elearning.Models;

public class Courses
{
    public int Id { get; set; }
    public string? Image { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; } = null!;
    public string Name { get; set; }= null!;
    public decimal Price { get; set; }
    public int TeacherId { get; set; }
    public Teacher? Teacher { get; set; }

}
