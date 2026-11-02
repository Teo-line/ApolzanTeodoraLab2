
using ApolzanTeodoraLab2.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration
    .GetConnectionString("ApolzanTeodoraLab2Context")
    ?? throw new InvalidOperationException(
        "Connection string not found.");

// Contextul principal al aplicației
builder.Services.AddDbContext<ApolzanTeodoraLab2Context>(options =>
    options.UseSqlServer(connectionString));

// Contextul pentru Identity
builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("LibraryIdentityContext")
        ?? throw new InvalidOperationException(
            "Identity connection string not found.")));

// Configurarea Identity
builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<LibraryIdentityContext>();

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
