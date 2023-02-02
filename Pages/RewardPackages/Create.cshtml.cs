using FundRaiser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Pages.RewardPackages;

public class CreateModel : PageModel
{
    private readonly FundRaiserContext _context;
    
    [BindProperty(SupportsGet = true)]
    public string Id { get; set; }   //project id
    
    [BindProperty]
    public Models.RewardPackage? RewardPackage { get;set; }
    
    public CreateModel(FundRaiserContext context)
    {
        _context = context;
    }
    
    public IActionResult OnGetAsync()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var reward = await _context.RewardPackages!
            .Include(p => p.Project)
            .SingleOrDefaultAsync(p => p.Title.Equals(RewardPackage!.Title) && p.Project.Id.ToString() == Id);
        if (reward != null)
        {
            return StatusCode(409, $"Reward package {RewardPackage!.Title} already exist.");
        }
        
        var project = await _context.Projects!.SingleOrDefaultAsync(c => c.Id.Equals(new Guid(Id)));
        
        RewardPackage!.Project = project!;
        if (project != null) await _context.RewardPackages!.AddAsync(RewardPackage);
        await _context.SaveChangesAsync();

        return RedirectToPage("/RewardPackages/Index", new
        {
            Id = RewardPackage.ProjectId
        });
    }
}