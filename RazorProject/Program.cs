using Microsoft.EntityFrameworkCore;
using YourProjectNamespace.Data; // Ensure this namespace matches your project structure

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true; // Make the cookie HTTP-only
    options.Cookie.IsEssential = true; // Ensure the cookie is essential
});

builder.Services.AddDistributedMemoryCache(); // Required for session

// Add and configure the database context
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDbConnection")));

var app = builder.Build();

// Enable session middleware
app.UseSession();
app.UseRouting();

app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Ensure static files middleware is added

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
