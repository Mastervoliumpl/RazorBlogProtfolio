using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Reposetories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. AddRazorPages adds services for Razor Pages to the app.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IBlogPostRepo, BlogPostRepo>()
                .AddSingleton<IImageRepo, ImageRepo>()
                .AddSingleton<IPortfolioRepo, PortfolioRepo>()
                .AddSingleton<ITagRepo, TagRepo>()
                .AddSingleton<IPostRepo, PostRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
// Uncomment if you want to redirect HTTP to HTTPS
// app.UseHttpsRedirection();
app.UseStaticFiles(); // Can't use wwwroot or bootstrap if this is removed

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages(); // MapRazorPages adds endpoints for Razor Pages to the IEndpointRouteBuilder.

app.Run();

// You will get a 404 error if the above does not run in order from top to bottom, or one of them is missing.
// index is the same as default, so you can use either one.