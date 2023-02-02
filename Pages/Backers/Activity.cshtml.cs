using FundRaiser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Pages.Backers
{
    public class ActivityModel : PageModel
    {
        private readonly FundRaiserContext? _context;

        public ActivityModel(FundRaiserContext context)
        {
            _context = context;
        }

        public List<Models.Fund> MyFunds { get; set; }
        public Models.Backer TheBacker { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            var backerExists = await _context.Backers.SingleOrDefaultAsync(b => b.Id.ToString() == id);
            if (backerExists == null) 
            {
                return NotFound("Backer not found.");
            }
            TheBacker = backerExists;
            MyFunds = await _context.Funds
                .Include(f => f.Project)
                .Include(f => f.RewardPackage)
                .Where(f => f.BackerId.ToString() == id)
                .OrderBy(f => f.CreatedAt)
                .ToListAsync();

            return Page();
        }

    }
}
