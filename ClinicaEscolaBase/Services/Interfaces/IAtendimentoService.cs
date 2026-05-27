using ClinicaEscolaBase.Dtos.AtendimentoDto;

namespace ClinicaEscolaBase.Services.Interfaces;

public interface IAtendimentoService
{
	Task<IEnumerable<AtendimentoListaDto>> BuscarAtendimentosPorPaciente(string idPaciente);
	Task<IEnumerable<AtendimentoListaDto>> BuscarAtendimentosPorPsicologo(string idPsicologo);
	Task<AtendimentoDto> BuscarAtendimentoPorPaciente(string idPaciente);
   	Task<AtendimentoDto> BuscarAtendimentoPorPsicologo(string idPsicologo);
	Task<AtendimentoDto> EvoluirAtendimento(EvolucaoAtendimentoDto evolucaoAtendimento)

}