using ClinicaEscolaBase.Enums;

namespace ClinicaEscolaBase.Models;

public class InfoFamiliarModel
{
    public int Id { get; set; }
    public string? NomeCompleto { get; set; }  
    public ParentescoEnum? Parentesco { get; set; }    
    public string? GrauInstrucao { get; set; }
    public string? Profissao { get; set; }
    public DateTime? DataNascimento { get; set; }
     public string? RG { get; set; }
    public string? CPF { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public Endereco? Endereco { get; set; }
    public string? CondicaoConjugal { get; set; }
    public bool ResponsavelPrincipal { get; set; } // o sistema deve permitir somente 1 responsavel principal
     public PacienteModel Paciente { get; set; } = null!;
     public ICollection<TermoAutorizacaoMenorModel> TermosAutorizacaoMenor { get; set; } = [];
}