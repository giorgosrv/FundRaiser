using FundRaiser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Pages.Posts;

public class Index : PageModel
{
    private readonly FundRaiserContext _context;
    [BindProperty]
    public Models.Post? Post { get;set; }
    
    [BindProperty]
    public List<Models.Post> Posts { get; set; }
    public Models.Project Project { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string? Id { get; set; }
    
    public Index(FundRaiserContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> OnGetAsync(string id)
    {
        var project = await _context.Projects!.Include(p => p.Creator).FirstOrDefaultAsync(p => p.Id == new Guid(id));
        if (project == null)
        {
            return NotFound("Project not found");
        }

        Project = project;
        Posts = await _context.Posts
            .Where(p => p.Project.Id == project.Id)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        var project = await _context.Projects!.SingleOrDefaultAsync(p => p.Id.Equals(new Guid(Id)));
        Post!.Project = project;
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Id == null)
        {
            return NotFound("Can't create a post");
        }
        if (Post != null) await _context.Posts!.AddAsync(Post);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Creators/Dashboard", new
        {
            id = project.CreatorId
        });
    }
}