using Elearning.DAL;

namespace Elearning.Services;

public class LayoutService
{
    private readonly AppDbContext _context;

    public LayoutService(AppDbContext context)
    {
        _context = context;
    }
    public Dictionary<string,string> GetService()
    {
        return  _context.Settings.ToDictionary(x => x.Key, x => x.Value);
    }
}
