using FundRaiser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Pages.Backers;

public class CreateModel : PageModel
{
    private readonly FundRaiserContext _context;

    public CreateModel(FundRaiserContext context)
    {
        _context = context;
    }
    [BindProperty]
    public Models.Backer? Backer { get;set; }
    
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
        
        var backerExists = await _context.Backers!.SingleOrDefaultAsync(p => p.Email.Equals(Backer!.Email));
        if (backerExists != null)
        {
            return StatusCode(409, $"Backer with {Backer!.Email} already exist.");
        }
        if (Backer != null) await _context.Backers!.AddAsync(Backer);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Backers/SelectBacker");
    }
}