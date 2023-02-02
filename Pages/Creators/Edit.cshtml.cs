using FundRaiser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Pages.Creators
{
    public class Edit : PageModel
    {
        private readonly FundRaiserContext _context;

        public Edit(FundRaiserContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Models.Creator Creator { get; set; }

        [BindProperty]
        public string Id { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            Id = id;
            Creator = await _context.Creators.FindAsync(new Guid(id));
            if (Creator == null)
            {
                return NotFound("Creator not found");
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var creator = await _context.Creators!.FindAsync(Creator.Id);
            creator!.FirstName = Creator.FirstName;
            creator.LastName = Creator.LastName;
            creator.Email = Creator.Email;
            creator.Profession = Creator.Profession;
            creator.IdentityId = Creator.IdentityId;

            await _context.SaveChangesAsync();
            return RedirectToPage("/Creators/Dashboard", new { id = (creator.Id).ToString() });
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var forDeletion = await _context.Creators!.SingleOrDefaultAsync(p => p.Id.Equals(new Guid(Id)));
            if (forDeletion == null)
            {
                return NotFound();
            }

            var projects = await _context.Projects!.Where(p => p.CreatorId.Equals(forDeletion.Id)).ToListAsync();
            foreach (var project in projects)  //delete funds and reward packages
            {
                _context.RewardPackages!.RemoveRange(_context.RewardPackages.Where(p=> p.ProjectId.Equals(project.Id)));
                _context.Funds!.RemoveRange(_context.Funds.Where(f => f.ProjectId.Equals(project.Id)));
            }
            _context.Projects!.RemoveRange(projects);
            _context.Creators!.Remove(forDeletion);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Creators/SelectCreator");
        }
    }
}