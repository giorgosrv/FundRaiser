using FundRaiser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Pages.Creators;

public class Dashboard : PageModel
{
    private readonly FundRaiserContext? _context;

    [BindProperty(SupportsGet = true)] public string Id { get; set; }
    
    public IList<Models.Project> Projects { get;set; }  = default!;

    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }

    public SelectList? Categories { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Category { get; set; }

    public Dashboard(FundRaiserContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        IQueryable<string> genreQuery = from m in _context!.Projects
            orderby m.Category
            select m.Category;

        var projects = from m in _context.Projects
            select m;

        if (!string.IsNullOrEmpty(SearchString))
        {
            projects = projects.Where(s => s.Title.Contains(SearchString));
        }

        if (!string.IsNullOrEmpty(Category))
        {
            projects = projects.Where(x => x.Category == Category);
        }

        Categories = new SelectList(await genreQuery.Distinct().ToListAsync());
        Projects = await projects.Where(p => p.CreatorId.Equals(new Guid(Id))).ToListAsync();
    }
    
    public async Task<IActionResult> OnPostDelete(string id)
    {
        
        var forDeletion = await _context.Projects!.SingleOrDefaultAsync(p => p.Id.Equals(new Guid(id)));
        if (forDeletion == null)
        {
            return NotFound();
        }

        _context.Projects!.Remove(forDeletion);
        await _context.SaveChangesAsync();
        return RedirectToPage("/Creators/Dashboard", new
        {
            id = Id
        });
    }
}