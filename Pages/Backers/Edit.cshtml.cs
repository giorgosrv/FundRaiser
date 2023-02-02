using FundRaiser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Pages.Backers;

public class Edit : PageModel
{
    private readonly FundRaiserContext _context;

    public Edit(FundRaiserContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Models.Backer Backer { get; set; }

    [BindProperty]
    public string Id { get; set; }
    public async Task<IActionResult> OnGetAsync(string id)
    {
        Id = id;
        Backer = await _context.Backers.FindAsync(new Guid(id));
        if (Backer == null)
        {
            return NotFound("Backer not found");
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var backer = await _context.Backers!.FindAsync(Backer.Id);
        backer!.FirstName = Backer.FirstName;
        backer.LastName = Backer.LastName;
        backer.Email = Backer.Email;

        await _context.SaveChangesAsync();
        return RedirectToPage("/Backers/Dashboard", new { id = (backer.Id).ToString() });
    }

    public async Task<IActionResult> OnPostDelete()
    {
        var forDeletion = await _context.Backers!.SingleOrDefaultAsync(p => p.Id.Equals(new Guid(Id)));
        if (forDeletion == null)
        {
            return NotFound();
        }

        _context.Backers!.Remove(forDeletion);
        await _context.SaveChangesAsync();
        return RedirectToPage("/Backers/SelectBacker");
    }
}
