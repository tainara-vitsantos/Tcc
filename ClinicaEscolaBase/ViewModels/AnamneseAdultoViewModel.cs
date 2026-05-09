using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaEscolaBase.ViewModels;

/// <summary>
/// ViewModel para criar/editar Anamnese de Adulto
/// Encapsula a lógica de apresentação sem poluir a Model.
/// Estruturada em seções para melhor UX em formulários longos.
/// </summary>
public class AnamneseAdultoViewModel
{
    // ==================== IDENTIFICAÇÃO ====================
    [Required(ErrorMessage = "O ID do paciente é obrigatório")]
    public Guid PacienteId { get; set; }

    [Required(ErrorMessage = "O ID do prontuário é obrigatório")]
    public int ProntuarioId { get; set; }

    public int? DocumentoClinicoId { get; set; }
    public int? AtendimentoId { get; set; }

    // ==================== SEÇÃO 1: DADOS DO ATENDIMENTO ====================
    [Display(Name = "Frequência do Atendimento")]
    [StringLength(100)]
    public string? FrequenciaAtendimento { get; set; }

    [Display(Name = "Data/Hora do Atendimento")]
    [DataType(DataType.DateTime)]
    public DateTime? DataHoraAtendimento { get; set; }

    // ==================== SEÇÃO 2: QUEIXA E SINTOMATOLOGIA ====================
    [Required(ErrorMessage = "A queixa principal é obrigatória")]
    [Display(Name = "Queixa Principal")]
    [StringLength(500)]
    public string? QueixaPrincipal { get; set; }

    [Display(Name = "Queixa Secundária")]
    [StringLength(500)]
    public string? QueixaSecundaria { get; set; }

    [Display(Name = "Sintomas")]
    [StringLength(2000)]
    public string? Sintomas { get; set; }

    [Display(Name = "Início da Patologia")]
    [StringLength(500)]
    public string? InicioPatologia { get; set; }

    [Display(Name = "Frequência da Patologia")]
    [StringLength(500)]
    public string? FrequenciaPatologia { get; set; }

    [Display(Name = "Intensidade da Patologia")]
    [StringLength(500)]
    public string? IntensidadePatologia { get; set; }

    [Display(Name = "Tratamentos Anteriores")]
    [StringLength(2000)]
    public string? TratamentosAnteriores { get; set; }

    [Display(Name = "Medicamentos em Uso")]
    [StringLength(2000)]
    public string? Medicamentos { get; set; }

    // ==================== SEÇÃO 3: HISTÓRIA PESSOAL ====================
    [Display(Name = "História da Infância")]
    [StringLength(2000)]
    public string? HistoriaInfancia { get; set; }

    [Display(Name = "Rotina Diária")]
    [StringLength(2000)]
    public string? Rotina { get; set; }

    [Display(Name = "Vícios/Hábitos Prejudiciais")]
    [StringLength(500)]
    public string? Vicios { get; set; }

    [Display(Name = "Hobbies/Atividades de Lazer")]
    [StringLength(500)]
    public string? Hobbies { get; set; }

    [Display(Name = "Situação Laboral/Profissional")]
    [StringLength(1000)]
    public string? Trabalho { get; set; }

    // ==================== SEÇÃO 4: HISTÓRICO FAMILIAR ====================
    [Display(Name = "Histórico Familiar - Pais")]
    [StringLength(1000)]
    public string? HistoricoFamiliarPais { get; set; }

    [Display(Name = "Histórico Familiar - Irmãos")]
    [StringLength(1000)]
    public string? HistoricoFamiliarIrmaos { get; set; }

    [Display(Name = "Histórico Familiar - Cônjuge")]
    [StringLength(1000)]
    public string? HistoricoFamiliarConjuge { get; set; }

    [Display(Name = "Histórico Familiar - Filhos")]
    [StringLength(1000)]
    public string? HistoricoFamiliarFilhos { get; set; }

    [Display(Name = "Condição do Lar")]
    [StringLength(1000)]
    public string? HistoricoFamiliarLar { get; set; }

    [Display(Name = "História Patológica Pregressa")]
    [StringLength(2000)]
    public string? HistoriaPatologicaPregressa { get; set; }

    // ==================== SEÇÃO 5: EXAME CLÍNICO ====================
    [Display(Name = "Aparência Geral")]
    [StringLength(500)]
    public string? ExameAparencia { get; set; }

    [Display(Name = "Comportamento")]
    [StringLength(500)]
    public string? ExameComportamento { get; set; }

    [Display(Name = "Hipótese Diagnóstica")]
    [StringLength(1000)]
    public string? HipoteseDiagnostica { get; set; }
}

