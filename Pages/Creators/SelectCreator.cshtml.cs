using FundRaiser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Pages.Creators;

public class SelectCreator : PageModel
{
    private FundRaiserContext _context;
    public SelectCreator(FundRaiserContext context)
    {
        _context = context;
    }
    [BindProperty]
    public List<Models.Creator>? AvailableCreators { get; set; }
    public List<SelectListItem> Options { get; set; }
    public string? Id { get; set; }
    public async Task OnGet()
    {
        AvailableCreators = await _context.Creators!.ToListAsync();
        Options = AvailableCreators.Select(b => 
            new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.FirstName + " " + b.LastName,
            }).ToList();
        
    }

    public RedirectToPageResult OnPost()
    {
        Id = Request.Form["Id"];
        return RedirectToPage($"/Creators/Dashboard", new { id = Id });
    }
}