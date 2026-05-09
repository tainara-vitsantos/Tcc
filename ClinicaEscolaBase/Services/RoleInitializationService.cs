using Microsoft.AspNetCore.Identity;

namespace ClinicaEscolaBase.Services;

/// <summary>
/// Serviço para inicializar os Roles (perfis) do sistema automaticamente.
/// Define Professor (acesso total) e Aluno (acesso restrito).
/// </summary>
public class RoleInitializationService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleInitializationService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    /// <summary>
    /// Inicializa os roles padrão do sistema se não existirem.
    /// </summary>
    public async Task InitializeAsync()
    {
        var roles = new[] { "Professor", "Aluno" };

        foreach (var roleName in roles)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
