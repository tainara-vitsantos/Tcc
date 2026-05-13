using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaEscolaBase.ViewModels;

public class PlantaoPsicologicoViewModel
{
    [Required]
    public Guid PacienteId { get; set; }

    public string? PacienteNome { get; set; }

    [Required]
    public int ProntuarioId { get; set; }

    [Display(Name = "Data do Atendimento")]
    [DataType(DataType.DateTime)]
    public DateTime? DataAtendimento { get; set; }

    [Display(Name = "Síntese da Queixa")]
    [StringLength(2000)]
    public string? SinteseQueixaInicial { get; set; }

    [Display(Name = "Relato do Atendimento")]
    [StringLength(4000)]
    public string? RelatoAtendimento { get; set; }

    [Display(Name = "Conduta e Encaminhamento")]
    [StringLength(2000)]
    public string? CondutaEncaminhamento { get; set; }

    [Display(Name = "Nome do Aluno")]
    [StringLength(200)]
    public string? NomeEstagiario { get; set; }

    [Display(Name = "Nome do Supervisor")]
    [StringLength(200)]
    public string? NomeSupervisor { get; set; }

    [Display(Name = "CRP do Supervisor")]
    [StringLength(50)]
    public string? CRPSupervisor { get; set; }
}
