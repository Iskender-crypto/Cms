using Cms.Domain.Entities;
using Cms.Domain.Services;
using Cms.Infrastructure.Ef;
using Cms.Infrastructure.Ef.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json").AddEnvironmentVariables()
    .AddCommandLine(args).Build();
var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton(configuration);
serviceCollection.AddScoped<IAuthService, AuthService>();
serviceCollection.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
});
serviceCollection.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredLength = 5;
    }).AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();
serviceCollection.AddLogging();
var serviceProvider = serviceCollection.BuildServiceProvider();
var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var dataContext = serviceProvider.GetRequiredService<DataContext>();
var transaction = await dataContext.Database.BeginTransactionAsync();
try
{
    if (!await roleManager.RoleExistsAsync("Administrator"))
    {
        await roleManager.CreateAsync(new IdentityRole("Administrator"));
    }

    var adminUser = await userManager.FindByEmailAsync("admin@example.com");
    if (adminUser == null)
    {
        var user = new IdentityUser { UserName = "admin@example.com", Email = "admin@example.com" };
        await userManager.CreateAsync(user, "Admin@001");
        await userManager.AddToRoleAsync(user, "Administrator");
        var profile = await dataContext.Set<Profile>().FirstOrDefaultAsync(p => p.SystemName == "AdminProfile");
        if (profile == null)
        {
            dataContext.Set<Profile>().Add(new Profile()
            {
                SystemName = "AdminProfile",
                FirstName = "Admin", LastName = "User",
                UserId = user.Id
            });
            await dataContext.SaveChangesAsync();
        }
    }

    await transaction.CommitAsync();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    await transaction.RollbackAsync();
}