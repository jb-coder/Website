using Microsoft.Extensions.Options;
using Portfolio.Web.Components;
using Portfolio.Web.Services;
using Portfolio.Web.Services.Options;

// When the published app is launched from another directory (for example the
// CI runner root), fall back to the application directory so appsettings.json
// and wwwroot are still found.
var contentRoot = File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
    ? Directory.GetCurrentDirectory()
    : AppContext.BaseDirectory;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = contentRoot,
});

builder.Services.AddRazorComponents();
builder.Services.AddPortfolio(builder.Configuration);

var app = builder.Build();

var portfolio = app.Services.GetRequiredService<IOptions<PortfolioOptions>>().Value;
var basePath = portfolio.NormalizedBasePath;
if (basePath != "/")
{
    app.UsePathBase(basePath);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapPortfolioEndpoints();
app.MapRazorComponents<App>();

app.Run();
