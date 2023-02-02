using FundRaiser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Pages.RewardPackages;

public class IndexModel : PageModel
{
    private readonly FundRaiserContext _context;

    [BindProperty]
    public List<Models.RewardPackage>? RewardPackages { get;set; }
    
    [BindProperty(SupportsGet = true)]
    public string Id { get;set; }

    public IndexModel(FundRaiserContext context)
    {
        _context = context;
    }
    
    public async Task OnGetAsync()
    {
        RewardPackages = await _context.RewardPackages!
            .Where(p => p.ProjectId.Equals(new Guid(Id)))
            .ToListAsync();

    }

    public async Task<IActionResult> OnPostDelete(string id)
    {
        var package = await _context.RewardPackages.FindAsync(new Guid(id));
        if (package == null)
        {
            return NotFound("Package not exits");
        }

        _context.RewardPackages.Remove(package);
        await _context.SaveChangesAsync();
        return RedirectToPage("/RewardPackages/Index",new
        {
            Id = package.ProjectId
        });
    }
}