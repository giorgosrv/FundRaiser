using FundRaiser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Pages.Backers;

public class SelectBacker : PageModel
{
    private FundRaiserContext _context;
    public SelectBacker(FundRaiserContext context)
    {
        _context = context;
    }
    [BindProperty]
    public List<Models.Backer>? AvailableBackers { get; set; }
    public List<SelectListItem> Options { get; set; }
    public string? Id { get; set; }
    public async Task OnGet()
    {
        AvailableBackers = await _context.Backers!.ToListAsync();
        Options = AvailableBackers.Select(b => 
            new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.FirstName + " " + b.LastName,
            }).ToList();
    }

    public RedirectToPageResult OnPost()
    {
        Id = Request.Form["Id"];
        return RedirectToPage($"/Backers/Dashboard", new { id = Id });
    }
}