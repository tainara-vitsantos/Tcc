using ClinicaEscolaBase.Dtos;

namespace ClinicaEscolaBase.ViewModels;

public class DashboardViewModel
{
public bool IsProfessor { get; set; }
    public DashboardAlunoDto? AlunoData { get; set; }
    public DashboardProfessorDto? ProfessorData { get; set; }
}