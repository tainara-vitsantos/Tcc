using ClinicaEscolaBase.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ClinicaEscolaBase.Services;

/// <summary>
/// Serviço para inicializar os Roles (perfis) do sistema automaticamente.
/// Define Professor (acesso total) e Aluno (acesso restrito).
/// </summary>
public class RoleInitializationService(RoleManager<IdentityRole> roleManager):IRoleInitializationService
{

    /// <summary>
    /// Inicializa os roles padrão do sistema se não existirem.
    /// </summary>
    public async Task InitializeAsync()
    {
        var roles = new[] { "Professor", "Aluno" };

        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
