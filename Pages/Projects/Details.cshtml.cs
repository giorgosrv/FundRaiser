using FundRaiser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Pages.Projects
{
    public class DetailsModel : PageModel
    {

        private readonly FundRaiserContext _context;

        public DetailsModel(FundRaiserContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Project Project { get; set; }
        public Models.Post? Post { get; set; }
        public List<Models.Post> Posts { get; set; }
        public string[] Images { get; set; }
        public string[] Videos { get; set; }
        public List<Models.RewardPackage>? Packages { get; set; }
        
        [BindProperty]
        public List<Models.Fund>? Funds { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            Project = await _context.Projects.Include(p => p.Creator)
                .Include(p => p.BackersFund)
                .SingleOrDefaultAsync(p => p.Id.Equals(new Guid(id)));
            if (Project == null)
            {
                return NotFound("Project not found");
            }

            Posts = await _context.Posts
                .Where(p => p.Project.Id == Project.Id)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            var filePathImages = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\Images", id.ToString());
            if (Directory.Exists(filePathImages) is false)
            {
                Directory.CreateDirectory(filePathImages);
            }
            Images = Directory.GetFiles(filePathImages);

            var filePathVideos = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\Videos", id.ToString());
            if (Directory.Exists(filePathVideos) is false)
            Packages = await _context.RewardPackages!.Where(p => p.ProjectId.Equals(Project.Id)).ToListAsync();
            Funds = await _context.Funds!
                .Where(f => f.ProjectId.Equals(Project.Id))
                .Include(f => f.Backer)
                .ToListAsync();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", id.ToString());
            if (Directory.Exists(filePath) is false)
            {
                Directory.CreateDirectory(filePathVideos);
            }
            Videos = Directory.GetFiles(filePathVideos);

            return Page();
        }
    }
}
