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

    public ICollection<VinculoAlunoPacienteModel> VinculosComoAluno { get; set; } = new List<VinculoAlunoPacienteModel>();
    public ICollection<VinculoAlunoPacienteModel> VinculosLiberados { get; set; } = new List<VinculoAlunoPacienteModel>();
    public ICollection<VinculoAlunoPacienteModel> VinculosRevogados { get; set; } = new List<VinculoAlunoPacienteModel>();
    public ICollection<AtendimentoModel> AtendimentosComoAluno { get; set; } = new List<AtendimentoModel>();
    public ICollection<AtendimentoModel> AtendimentosComoSupervisor { get; set; } = new List<AtendimentoModel>();
    public ICollection<DocumentoClinicoModel> DocumentosCriados { get; set; } = new List<DocumentoClinicoModel>();
    public ICollection<DocumentoClinicoModel> DocumentosSupervisionados { get; set; } = new List<DocumentoClinicoModel>();
    public ICollection<EvolucaoAtendimentoModel> EvolucoesCriadas { get; set; } = new List<EvolucaoAtendimentoModel>();
    public ICollection<EvolucaoAtendimentoModel> EvolucoesSupervisionadas { get; set; } = new List<EvolucaoAtendimentoModel>();
    public ICollection<TermoResponsabilidadeEstagiarioModel> TermosResponsabilidade { get; set; } = new List<TermoResponsabilidadeEstagiarioModel>();
    public ICollection<AnexoModel> AnexosEnviados { get; set; } = new List<AnexoModel>();
    public ICollection<AuditoriaModel> Auditorias { get; set; } = new List<AuditoriaModel>();
}
