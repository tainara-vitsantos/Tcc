
using ClinicaEscolaBase.Dtos.PacienteDto;

namespace ClinicaEscolaBase.Services.Interfaces;

public interface IPacienteService
{
	Task<IEnumerable<UsuarioListaDto>> BuscarTodosPacientes();
	Task<IEnumerable<UsuarioListaDto>> BuscarPacientesPorPsicologo(string idPsicologo);
	Task<PacienteDetalhesDto> BuscarPacientePorIdPaciente(string idPaciente);
	Task<IEnumerable<TratamentoAnteriorDto>> BuscarTratamentosAnteriores(string idPaciente);
	Task<IEnumerable<ResponsavelLegalDto>> BuscarResponsaveisLegal(string idPaciente);
	Task<PacienteDetalhesDto> CadastrarPaciente(PacienteCadastroDto pacienteCadastroDto);
	Task<PacienteDetalhesDto> AtualizarPaciente(PacienteAtualizacaoDto pacienteAtualizacaoDto);
	

		
	//	Task<IEnumerable<DocsClinicoListaDto>> BuscarDocumentosClinicos(string idPaciente);
	//VinculoAlunoPacienteModel e AuditoriaModel
}