using Microsoft.AspNetCore.Identity;
using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // 1. Criar Roles
        string[] roles = { "Professor", "Aluno" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // 2. Criar Professor (Você)
        if (await userManager.FindByEmailAsync("prof@fatec.com") == null)
        {
            var prof = new ApplicationUser { 
                UserName = "prof@fatec.com", 
                Email = "prof@fatec.com", 
                NomeCompleto = "Tainara Vitória (Supervisor)",
                EmailConfirmed = true 
            };
            await userManager.CreateAsync(prof, "Senha@123");
            await userManager.AddToRoleAsync(prof, "Professor");
        }

        // 3. Criar Aluno
        if (await userManager.FindByEmailAsync("aluno@fatec.com") == null)
        {
            var aluno = new ApplicationUser { 
                UserName = "aluno@fatec.com", 
                Email = "aluno@fatec.com", 
                NomeCompleto = "Aluno Estagiário",
                EmailConfirmed = true 
            };
            await userManager.CreateAsync(aluno, "Senha@123");
            await userManager.AddToRoleAsync(aluno, "Aluno");
        }
    }
}