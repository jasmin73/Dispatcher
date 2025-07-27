using Dispatcher.Bussiness.Mapper;
using Dispatcher.Bussiness.Repository;
using Dispatcher.Bussiness.Repository.IRepository;
using Dispatcher.DataAccess;
using Dispatcher.DataAccess.Data;
using Dispatcher.Server.Components;
using Dispatcher.Server.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzk2MzU2NUAzMzMwMmUzMDJlMzAzYjMzMzAzYkpzVWpQYjVJaXlYc0Uvd2plTGhtdkdheisyaGpZV0laVDE3clJhNnN3RDQ9");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRazorPages();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(); // koristi Default scheme iz Identity-ja

builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddSyncfusionBlazor();
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();



var app = builder.Build();
await DbInitializer.Seed(app.Services);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var user = await userManager.FindByEmailAsync("admin@firma.rs");
    if (user == null)
    {
        var newUser = new ApplicationUser
        {
            UserName = "admin@firma.rs",
            Email = "admin@firma.rs",
            FullName = "Administrator",
            TenantId = 1 // ili nešto iz baze ako postoji
        };
        await userManager.CreateAsync(newUser, "Test123!");
    }
}

