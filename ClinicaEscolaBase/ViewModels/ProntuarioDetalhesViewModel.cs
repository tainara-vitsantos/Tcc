using System.Collections.Generic;
using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.ViewModels;

public class ProntuarioDetalhesViewModel
{
    public PacienteModel Paciente { get; set; } = null!;
    public ProntuarioModel Prontuario { get; set; } = null!;
    public IReadOnlyCollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();
    public IReadOnlyCollection<DocumentoClinico> DocumentosClinicos { get; set; } = new List<DocumentoClinico>();
    public IReadOnlyCollection<EvolucaoAtendimentoModel> Evolucoes { get; set; } = new List<EvolucaoAtendimentoModel>();
}
