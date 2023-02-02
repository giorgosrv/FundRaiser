using FundRaiser.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace FundRaiser.Pages.Projects
{
    public class AddMediaModel : PageModel
    {

        private readonly FundRaiserContext _context;

        public AddMediaModel(FundRaiserContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Project Project { get; set; }

        public string[] Images { get; set; }
        public string[] Videos { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            Project = await _context.Projects
                .Include(p => p.Creator)
                .SingleOrDefaultAsync(p => p.Id.Equals(new Guid(id)));
            if (Project == null)
            {
                return NotFound("Project not found");
            }

            var filePathImages = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\Images", id.ToString());
            if (Directory.Exists(filePathImages) is false)
            {
                Directory.CreateDirectory(filePathImages);
            }
            Images = Directory.GetFiles(filePathImages);

            var filePathVideos = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\Videos", id.ToString());
            if (Directory.Exists(filePathVideos) is false)
            {
                Directory.CreateDirectory(filePathVideos);
            }
            Videos = Directory.GetFiles(filePathVideos);    

            return Page();
        }

        [BindProperty]
        public IFormFile UploadImage { get; set; }
        public async Task<IActionResult> OnPostImageAsync(string id)
        {
            var project = await _context.Projects
                .Include(p => p.Creator)
                .SingleOrDefaultAsync(p => p.Id.Equals(new Guid(id)));
            if (project == null)
            {
                return NotFound("Project not found");
            }
            //System.IO.Directory.CreateDirectory("uploads" + id.ToString());
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\Images", id.ToString());
            if (Directory.Exists(filePath) is false)
            {
                Directory.CreateDirectory(filePath);
            }
            try
            {
                var bug = UploadImage.FileName;
            }
            catch (NullReferenceException e)
            {
                return NotFound("No file selected.");
            }

            var file = Path.Combine(filePath, UploadImage.FileName);

            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await UploadImage.CopyToAsync(fileStream);
            }

            var currentFile = new FileInfo(file);
            //var temp = DateTime.UnixEpoch.Ticks.ToString();
            //var dto = new DateTimeOffset(0, 0, 0, 0, 0, 0, 0, new TimeSpan(10000));
            var offset = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            currentFile.MoveTo(currentFile.Directory.FullName + "\\" + offset + currentFile.Extension);

            return RedirectToPage("/Creators/Dashboard", new { id = project.Creator.Id.ToString() });
        }

        [BindProperty]
        public IFormFile UploadVideo { get; set; }
        public async Task<IActionResult> OnPostVideoAsync(string id)
        {
            var project = await _context.Projects
                .Include(p => p.Creator)
                .SingleOrDefaultAsync(p => p.Id.Equals(new Guid(id)));
            if (project == null)
            {
                return NotFound("Project not found");
            }
            //System.IO.Directory.CreateDirectory("uploads" + id.ToString());
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\Videos", id.ToString());
            if (Directory.Exists(filePath) is false)
            {
                Directory.CreateDirectory(filePath);
            }
            try
            {
                var bug = UploadVideo.FileName;
            }
            catch (NullReferenceException e)
            {
                return NotFound("No file selected.");
            }

            var file = Path.Combine(filePath, UploadVideo.FileName);

            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await UploadVideo.CopyToAsync(fileStream);
            }

            var currentFile = new FileInfo(file);
            //var temp = DateTime.UnixEpoch.Ticks.ToString();
            //var dto = new DateTimeOffset(0, 0, 0, 0, 0, 0, 0, new TimeSpan(10000));
            var offset = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            currentFile.MoveTo(currentFile.Directory.FullName + "\\" + offset + currentFile.Extension);

            return RedirectToPage("/Creators/Dashboard", new { id = project.Creator.Id.ToString() });
        }

        public IActionResult OnPostRemove(string filepath)
        {
            var fullPath = "wwwroot" + filepath;
            System.IO.File.Delete(fullPath);
            return RedirectToPage("");
        }

    }
}
