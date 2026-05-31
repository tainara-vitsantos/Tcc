using Microsoft.AspNetCore.Identity;
using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUserModel>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
      
    }
}