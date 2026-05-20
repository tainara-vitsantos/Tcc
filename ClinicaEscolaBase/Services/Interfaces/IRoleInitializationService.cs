namespace ClinicaEscolaBase.Services.Interfaces;

/// <summary>
/// Contrato para o serviço de inicialização automática de perfis (Roles) do sistema.
/// </summary>
public interface IRoleInitializationService
{
    /// <summary>
    /// Inicializa os roles padrão do sistema se eles ainda não existirem.
    /// </summary>
    Task InitializeAsync();
}