using ClinicaEscolaBase.Enums;
using Microsoft.AspNetCore.Identity;

namespace ClinicaEscolaBase.Models;

public class ApplicationUserModel : IdentityUser
{
    public string NomeCompleto { get; set; } = string.Empty;
    public string? Cpf { get; set; }
    public string? Matricula { get; set; }
    public string? Crp { get; set; }
    public TipoUsuarioEnum TipoUsuario { get; set; }
    public bool Ativo { get; set; } = true;
   
    public ICollection<DocumentoClinicoModel> DocumentosCriados { get; set; } = [];
    public ICollection<EvolucaoAtendimentoModel> EvolucoesCriadas { get; set; } = [];    
    public ICollection<AnexoModel> AnexosEnviados { get; set; } = [];
    public ICollection<AuditoriaModel> Auditorias { get; set; } = [];
}
