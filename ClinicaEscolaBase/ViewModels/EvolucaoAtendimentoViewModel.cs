using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaEscolaBase.ViewModels;

public class EvolucaoAtendimentoViewModel
{
    public int? Id { get; set; }
    public int DocumentoClinicoId { get; set; }
    public int? AtendimentoId { get; set; }

    [Display(Name = "Data da Evolução")]
    [DataType(DataType.DateTime)]
    public DateTime DataEvolucao { get; set; } = DateTime.UtcNow;

    [Required]
    [Display(Name = "Relato da Evolução")]
    public string TextoEvolucao { get; set; } = string.Empty;

    [Display(Name = "ID do autor (aluno)")]
    public string CriadoPorUsuarioId { get; set; } = string.Empty;

    [Display(Name = "ID do supervisor")]
    public string? SupervisorId { get; set; }

    [Display(Name = "Assinatura do Aluno")]
    [StringLength(200)]
    public string? AssinaturaAluno { get; set; }

    [Display(Name = "Assinatura do Supervisor")]
    [StringLength(200)]
    public string? AssinaturaSupervisor { get; set; }
}
