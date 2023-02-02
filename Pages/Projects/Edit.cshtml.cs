using FundRaiser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FundRaiser.Pages.Projects;

public class Edit : PageModel
{
    private readonly FundRaiserContext _context;

    public Edit(FundRaiserContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public Models.Project Project { get; set; }
    
    public async Task<IActionResult> OnGetAsync(string id)
    {
        Project = await _context.Projects.FindAsync(new Guid(id));
        if (Project == null)
        {
            return NotFound("Project not found");
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var project = await _context.Projects!.FindAsync(Project.Id);
        project!.Title = Project.Title;
        project.Description = Project.Description;
        project.FinancialGoal = Project.FinancialGoal;
        project.Category = Project.Category;
        await _context.SaveChangesAsync();
        return RedirectToPage("/Creators/Dashboard", new
        {
            id = project.CreatorId
        });
    }
}