using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaEscolaBase.ViewModels;

/// <summary>
/// ViewModel para criar/editar Anamnese de Adolescente
/// Encapsula a lógica de apresentação sem poluir a Model.
/// Estruturada em seções para melhor UX em formulários longos.
/// </summary>
public class AnamneseAdolescenteViewModel
{
    // ==================== IDENTIFICAÇÃO ====================
    [Required(ErrorMessage = "O ID do paciente é obrigatório")]
    public Guid PacienteId { get; set; }

    [Display(Name = "Nome do Paciente")]
    public string? PacienteNome { get; set; }

    [Required(ErrorMessage = "O ID do prontuário é obrigatório")]
    public int ProntuarioId { get; set; }

    public int? DocumentoClinicoId { get; set; }
    public int? AtendimentoId { get; set; }

    // ==================== SEÇÃO 1: DADOS ESCOLARES E PAIS ====================
    [Display(Name = "Escolaridade")]
    [StringLength(100)]
    public string? Escolaridade { get; set; }

    [Display(Name = "Escola")]
    [StringLength(200)]
    public string? Escola { get; set; }

    [Display(Name = "Nome do Pai")]
    [StringLength(200)]
    public string? PaiNome { get; set; }

    [Display(Name = "Idade do Pai")]
    public int? PaiIdade { get; set; }

    [Display(Name = "Instrução do Pai")]
    [StringLength(100)]
    public string? PaiInstrucao { get; set; }

    [Display(Name = "Profissão do Pai")]
    [StringLength(150)]
    public string? PaiProfissao { get; set; }

    [Display(Name = "Nome da Mãe")]
    [StringLength(200)]
    public string? MaeNome { get; set; }

    [Display(Name = "Idade da Mãe")]
    public int? MaeIdade { get; set; }

    [Display(Name = "Instrução da Mãe")]
    [StringLength(100)]
    public string? MaeInstrucao { get; set; }

    [Display(Name = "Profissão da Mãe")]
    [StringLength(150)]
    public string? MaeProfissao { get; set; }

    [Display(Name = "Condição Conjugal dos Pais")]
    [StringLength(150)]
    public string? CondicaoConjugalPais { get; set; }

    // ==================== SEÇÃO 2: QUEIXA PRINCIPAL ====================
    [Required(ErrorMessage = "A queixa principal é obrigatória")]
    [Display(Name = "Queixa Principal")]
    [StringLength(500)]
    public string? QueixaPrincipal { get; set; }

    [Display(Name = "Desde quando?")]
    [StringLength(500)]
    public string? DesdeQuando { get; set; }

    [Display(Name = "Atitude da Mãe frente à Queixa")]
    [StringLength(500)]
    public string? AtitudeMaeFrenteQueixa { get; set; }

    [Display(Name = "Atitude do Pai frente à Queixa")]
    [StringLength(500)]
    public string? AtitudePaiFrenteQueixa { get; set; }

    [Display(Name = "Atitude de Outros Familiares")]
    [StringLength(500)]
    public string? AtitudeOutrosFamiliares { get; set; }

    // ==================== SEÇÃO 3: HISTÓRIA PESSOAL ====================
    [Display(Name = "Criança foi Desejada?")]
    [StringLength(200)]
    public string? FoiDesejado { get; set; }

    [Display(Name = "Linguagem")]
    [StringLength(500)]
    public string? Linguagem { get; set; }

    [Display(Name = "Desenvolvimento Psicomotor")]
    [StringLength(1000)]
    public string? DesenvolvimentoPsicomotor { get; set; }

    [Display(Name = "Padrão de Sono")]
    [StringLength(500)]
    public string? Sono { get; set; }

    [Display(Name = "Hábitos Alimentares")]
    [StringLength(500)]
    public string? Alimentacao { get; set; }

    [Display(Name = "Tiques/Maneirismos")]
    [StringLength(500)]
    public string? Tiques { get; set; }

    [Display(Name = "Dificuldade Escolar")]
    [StringLength(1000)]
    public string? DificuldadeEscolar { get; set; }

    // ==================== SEÇÃO 4: HISTÓRICO FAMILIAR ====================
    [Display(Name = "Há Familiares com Problemas Nervosos?")]
    [StringLength(200)]
    public string? FamiliarNervoso { get; set; }

    [Display(Name = "Descrição do Familiar Nervoso")]
    [StringLength(1000)]
    public string? DescricaoFamiliarNervoso { get; set; }

    [Display(Name = "Há Familiares com Problemas Mentais?")]
    [StringLength(200)]
    public string? FamiliarProblemaMental { get; set; }

    [Display(Name = "Vícios na Família")]
    [StringLength(500)]
    public string? ViciosFamilia { get; set; }

    // ==================== SEÇÃO 5: ATIVIDADES E COMPANHEIRAS ====================
    [Display(Name = "Local de Estudo")]
    [StringLength(500)]
    public string? LocalEstudo { get; set; }

    [Display(Name = "Tipos de Diversão")]
    [StringLength(500)]
    public string? TiposDiversao { get; set; }

    [Display(Name = "Família faz Visitas?")]
    [StringLength(500)]
    public string? FamiliaFazVisitas { get; set; }

    [Display(Name = "Família recebe Visitas?")]
    [StringLength(500)]
    public string? FamiliaRecebeVisitas { get; set; }

    [Display(Name = "Companheiros/Amigas")]
    [StringLength(500)]
    public string? Companheiros { get; set; }

    [Display(Name = "Quem escolhe os Companheiros?")]
    [StringLength(500)]
    public string? QuemEscolheCompanheiros { get; set; }

    [Display(Name = "Religião")]
    [StringLength(200)]
    public string? Religiao { get; set; }
}

