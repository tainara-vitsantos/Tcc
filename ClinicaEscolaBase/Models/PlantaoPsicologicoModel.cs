
namespace ClinicaEscolaBase.Models;

public class PlantaoPsicologicoModel
{
    public int DocumentoClinicoId { get; set; }
    public DateTime? DataAtendimento { get; set; }
    public string? ResponsavelNome { get; set; }
    public string? NomeEstagiario { get; set; }
    public string? AssinaturaAluno { get; set; }
    public string? NomeSupervisor { get; set; }
    public string? AssinaturaSupervisor { get; set; }
    public string? SinteseQueixaInicial { get; set; }
    public string? RelatoAtendimento { get; set; }
    public string? CondutaEncaminhamento { get; set; }
    public string? CRPSupervisor { get; set; }

    public DocumentoClinicoModel DocumentoClinico { get; set; } = null!;
}
