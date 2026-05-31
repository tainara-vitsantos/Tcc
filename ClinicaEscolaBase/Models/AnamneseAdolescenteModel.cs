
using System.ComponentModel.DataAnnotations;

namespace ClinicaEscolaBase.Models;

public class AnamneseAdolescenteModel
{
    [Key]
    public int DocumentoClinicoId { get; set; }
    public DocumentoClinicoModel DocumentoClinico { get; set; } = null!;
    public string? Escolaridade { get; set; }
    public string? Escola { get; set; }
    public string? QueixaPrincipal { get; set; }
    public string? DesdeQuando { get; set; }
    public string? AtitudeMaeFrenteQueixa { get; set; }
    public string? AtitudePaiFrenteQueixa { get; set; }
    public string? AtitudeOutrosFamiliares { get; set; }
    public string? FoiDesejado { get; set; }
    public string? Linguagem { get; set; }
    public string? DesenvolvimentoPsicomotor { get; set; }
    public string? Sono { get; set; }
    public string? Alimentacao { get; set; }
    public string? Tiques { get; set; }
    public string? DificuldadeEscolar { get; set; }
    public string? FamiliarNervoso { get; set; }
    public string? DescricaoFamiliarNervoso { get; set; }
    public string? FamiliarProblemaMental { get; set; }
    public string? ViciosFamilia { get; set; }
    public string? LocalEstudo { get; set; }
    public string? TiposDiversao { get; set; }
    public string? FamiliaFazVisitas { get; set; }
    public string? FamiliaRecebeVisitas { get; set; }
    public string? Companheiros { get; set; }
    public string? QuemEscolheCompanheiros { get; set; }

    public int ResponsavelPrincipalId { get; set; }
    public InfoFamiliarModel? ResponsavelPrincipal { get; set; }
}