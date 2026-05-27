using ClinicaEscolaBase.Enums;

namespace ClinicaEscolaBase.Models;

public class VinculoAlunoPacienteModel : EntityBase
{
    public Guid PacienteId { get; set; }
    public string AlunoId { get; set; } = string.Empty;
    public string LiberadoPorUsuarioId { get; set; } = string.Empty;
    public DateTime DataLiberacao { get; set; }
    public string? RevogadoPorUsuarioId { get; set; }
    public DateTime? DataRevogacao { get; set; }
    public StatusVinculoEnum StatusVinculo { get; set; } = StatusVinculoEnum.Ativo;
    public string? Observacoes { get; set; }
    public bool PermiteLeitura { get; set; } = true;
    public bool PermiteEscrita { get; set; } = true;

    public PacienteModel Paciente { get; set; } = null!;
    public ApplicationUserModel Aluno { get; set; } = null!;
    public ApplicationUserModel LiberadoPorUsuario { get; set; } = null!;
    public ApplicationUserModel? RevogadoPorUsuario { get; set; }
}
