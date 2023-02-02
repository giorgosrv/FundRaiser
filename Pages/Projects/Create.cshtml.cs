using FundRaiser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Pages.Projects;

public class CreateModel : PageModel
{
    private readonly FundRaiserContext _context;

    public CreateModel(FundRaiserContext context)
    {
        _context = context;
    }
    [BindProperty]
    public Models.Project? Project { get;set; }
    
    [BindProperty(SupportsGet = true)]
    public string Id { get; set; }
    
    public IActionResult OnGetAsync()
    {
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var projectExists = await _context.Projects!.SingleOrDefaultAsync(p => p.Title.Equals(Project!.Title));
        if (projectExists != null)
        {
            return StatusCode(409, $"Project {Project!.Title} already exist.");
        }
        
        var creator = await _context.Creators!.SingleOrDefaultAsync(c => c.Id.Equals(new Guid(Id)));
        Project!.Creator = creator!;
        if (Project != null) await _context.Projects!.AddAsync(Project);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Creators/Dashboard", new 
        {
            id = creator.Id
        });
    }
}