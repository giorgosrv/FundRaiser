using FundRaiser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Pages.Creators;

public class CreateModel : PageModel
{
    private readonly FundRaiserContext _context;

    public CreateModel(FundRaiserContext context)
    {
        _context = context;
    }
    [BindProperty]
    public Models.Creator? Creator { get;set; }
    
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
        
        var creatorExists = await _context.Creators!.SingleOrDefaultAsync(p => p.Email.Equals(Creator!.Email));
        if (creatorExists != null)
        {
            return StatusCode(409, $"Creator with {Creator!.Email} already exist.");
        }
        if (Creator != null) await _context.Creators!.AddAsync(Creator);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Creators/SelectCreator");
    }
}