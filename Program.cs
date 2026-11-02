using ApolzanTeodoraLab2.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApolzanTeodoraLab2Context") ?? throw new InvalidOperationException("Connection string 'ApolzanTeodoraLab2Context' not found.");

// 1. Your primary context
builder.Services.AddDbContext<ApolzanTeodoraLab2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApolzanTeodoraLab2Context") ?? throw new InvalidOperationException("Connection string 'ApolzanTeodoraLab2Context' not found.")));

// 2. Your identity context (FIXED: Added  here)
builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryIdentityContext") ?? throw new InvalidOperationException("Connection string 'LibraryIdentityContext' not found.")));

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
