using FundRaiser.Data;
using FundRaiser.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<FundRaiserContext>(options =>
    options.UseSqlServer(builder.Configuration.
                             GetConnectionString("database") ??
                         throw new InvalidOperationException("Connection string 'database' not found.")));
builder.Services.AddScoped<IBackerService, BackerService>() ;
builder.Services.AddScoped<ICreatorService, CreatorService>();
builder.Services.AddScoped<IProjectServices, ProjectServices>() ;
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IRewardPackageService, RewardPackageService>();
builder.Services.AddScoped<IFundService, FundService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();

    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();

app.Run();