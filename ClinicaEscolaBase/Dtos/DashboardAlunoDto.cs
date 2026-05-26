using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Dtos;

public class DashboardAlunoDto
{
public int MeusPacientes { get; set; }
    public int MeusAtendimentosAgendados { get; set; }
    public int MeusAtendimentosRealizados { get; set; }
    public int MeusAtendimentosHoje { get; set; }
    public List<Atendimento> MeusProximosAtendimentos { get; set; } = [];
    public List<Atendimento> MeusPacientesRecentes { get; set; } = [];
}