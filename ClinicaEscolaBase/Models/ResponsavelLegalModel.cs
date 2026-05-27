namespace ClinicaEscolaBase.Models;

public class ResponsavelLegalModel : EntityBase
{
    public Guid PacienteId { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string? RG { get; set; }
    public string? CPF { get; set; }
    public string? GrauParentesco { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string? Endereco { get; set; }
    public bool ResponsavelPrincipal { get; set; }

    public PacienteModel Paciente { get; set; } = null!;
    public ICollection<TermoAutorizacaoMenorModel> TermosAutorizacaoMenor { get; set; } = new List<TermoAutorizacaoMenorModel>();
}
