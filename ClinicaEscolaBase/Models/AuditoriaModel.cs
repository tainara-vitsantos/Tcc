using ClinicaEscolaBase.Enums;

namespace ClinicaEscolaBase.Models;

public class AuditoriaModel : EntityBase
{
    public string UsuarioId { get; set; } = string.Empty;
    public TipoAcaoAuditoriaEnum TipoAcao { get; set; }
    public string Entidade { get; set; } = string.Empty;
    public string RegistroId { get; set; } = string.Empty;
    public int? PacienteId { get; set; }
    public int? ProntuarioId { get; set; }
    public DateTime DataHora { get; set; } = DateTime.UtcNow;
    public string? IP { get; set; }
    public string? UserAgent { get; set; }
    public string? ValoresAntesJson { get; set; }
    public string? ValoresDepoisJson { get; set; }
    public string? Observacoes { get; set; }

    public ApplicationUserModel Usuario { get; set; } = null!;
    public PacienteModel? Paciente { get; set; }
    public ProntuarioModel? Prontuario { get; set; }
}
