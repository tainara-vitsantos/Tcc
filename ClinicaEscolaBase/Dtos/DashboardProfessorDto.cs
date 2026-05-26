using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Dtos;

public class DashboardProfessorDto
{
public int TotalPacientes { get; set; }
    public int TotalProntuariosAtivos { get; set; }
    public int AtendimentosHoje { get; set; }
    public int AtendimentosRealizados { get; set; }
    public List<Atendimento> ProximosAtendimentos { get; set; } = [];
    public List<Auditoria> AuditoriasRecentes { get; set; } = [];
}