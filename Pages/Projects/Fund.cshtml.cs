using FundRaiser.Data;
using FundRaiser.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FundRaiser.Pages.Projects;

public class Fund : PageModel
{
    private readonly FundRaiserContext? _context;
    public Fund(FundRaiserContext context)
    {
        _context = context;
    }

    public string? SelectedPackageId { get; set; }
    
    [BindProperty]
    public Guid Id { get; set; }
    
    [BindProperty]
    public int FundAmount { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string? BackerId { get; set; }
    
    [BindProperty]
    public List<Models.RewardPackage>? AvailablePackages { get; set; }
    public List<SelectListItem> Options { get; set; }
    public async Task OnGet(string id)
    {
        Id = new Guid(id);
        AvailablePackages = await _context.RewardPackages.Where(p => p.ProjectId.Equals(Id)).ToListAsync();
        Options = AvailablePackages.Select(b => 
            new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Title,
            }).ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        SelectedPackageId = Request.Form["SelectedPackageId"];
        var selectedPackage = await _context.RewardPackages.FindAsync(new Guid(SelectedPackageId));

        var project = await _context
            .Projects!
            .Include(p => p.BackersFund)
            .SingleOrDefaultAsync(p => p.Id.Equals(Id));
        if (project == null)
        {
            return NotFound("Project not found");
        }

        if (FundAmount != null && FundAmount >= selectedPackage.NeedToFund)
        {
            project.CurrentFund += FundAmount;
        }
        else
        {
            return BadRequest("You must fund at least the minimum required amount.");
        }

        var backerFund = await _context.Backers!.FindAsync(new Guid(BackerId));
        var exists = project.BackersFund!.Where(b => b.Email.Equals(backerFund!.Email)).ToList();
        if (exists.Count == 0)
        {
            project.BackersFund!.Add(backerFund!);
        }
        
        var newFund = new Models.Fund()
        {
            BackerId = backerFund!.Id,
            Backer = backerFund,
            Project = project,
            ProjectId = project.Id,
            Amount = FundAmount,
            RewardPackage = selectedPackage,
            RewardPackageId = selectedPackage.Id
        };
        
        selectedPackage?.Funds.Add(newFund);
        await _context.Funds!.AddAsync(newFund);

        var needToFund = selectedPackage.NeedToFund;
        var returnAmount = selectedPackage.ReturnAmount;

        var fund = await _context.Funds
            .Where(f => f.ProjectId.Equals(project.Id))
            .Where(f => f.BackerId.Equals(backerFund.Id))
            .Where(f => f.RewardPackageId.Equals(selectedPackage.Id))
            .ToListAsync();

        var totalBackerFundBefore = fund.Sum(item => item.Amount);
        bool flag = totalBackerFundBefore < needToFund;
        var totalBackerFund = fund.Sum(item => item.Amount) + FundAmount;
        if ( totalBackerFund >= needToFund && flag)
        {
            backerFund.ReturnedAmount += returnAmount;
        }
        
        await _context.SaveChangesAsync();
        return RedirectToPage("/Backers/Dashboard", new { id = backerFund.Id.ToString()});
    }
}