using Elearning.Models;

namespace Elearning.ViewModels;

public class HomeVM
{
    public List<About> Abouts { get; set; }
    public List<Courses> Courses { get; set; }
    public List<Teacher> Teachers { get; set; }
    public Setting Settings { get; set; }
}
