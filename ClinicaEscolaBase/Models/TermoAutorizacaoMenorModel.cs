
using System.ComponentModel.DataAnnotations;

namespace ClinicaEscolaBase.Models;

public class TermoAutorizacaoMenorModel
{
    [Key]
    public int DocumentoClinicoId { get; set; }
    public int InfoFamiliarId { get; set; }
    public string? RGResponsavel { get; set; }
    public string? CPFResponsavel { get; set; }
    public string? NomeMenorNoTermo { get; set; }
    public DateTime? DataNascimentoMenorNoTermo { get; set; }
    public bool AutorizaAtendimentoPsicologico { get; set; }
    public bool AutorizaColetaInformacoes { get; set; }
    public bool CienteDevolutivaMensal { get; set; }
    public DateTime? DataAssinatura { get; set; }
    public string? Observacoes { get; set; }

    public DocumentoClinicoModel DocumentoClinico { get; set; } = null!;
    public InfoFamiliarModel InfoFamiliar { get; set; } = null!;
}
