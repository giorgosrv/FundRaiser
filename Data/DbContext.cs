using FundRaiser.Models;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Data;

public class FundRaiserContext: DbContext
{
    public FundRaiserContext(DbContextOptions<FundRaiserContext> options) : base(options)
    {
    }
    
    public DbSet<Project>? Projects { get; set; }
    public DbSet<Backer>? Backers { get; set; }
    public DbSet<Post>? Posts { get; set; }
    public DbSet<Creator>? Creators { get; set; }
    
    public DbSet<RewardPackage>? RewardPackages { get; set; }
    public DbSet<Fund>? Funds { get; set; }
}