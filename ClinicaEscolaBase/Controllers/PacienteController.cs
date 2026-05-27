using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Services.Interfaces;
using ClinicaEscolaBase.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Controllers;

[Authorize]
public class PacienteController(
    ApplicationDbContext context,
    IAuthService authorizationService,
    UserManager<ApplicationUser> userManager) : Controller
{

    // LISTAGEM: Ordenada por nome para melhor UX
    public async Task<IActionResult> Index()
    {
        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        // Alunos veem apenas seus pacientes vinculados; Professores veem todos
        var acessibleIds = await authorizationService.GetAcessiblePacienteIdsAsync(usuarioId);

        var pacientes = await context.Pacientes
            .Where(p => acessibleIds.Contains(p.Id))
            .AsNoTracking()
            .OrderBy(x => x.NomeCompleto)
            .ToListAsync();

        return View(pacientes);
    }

    // DETALHES: Carrega Prontuário e Atendimentos (Eager Loading)
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null) return NotFound();

        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        // Validar autorização
        if (!await authorizationService.CanReadPacienteAsync(usuarioId, id.Value))
            return Forbid();

        var paciente = await context.Pacientes
            .Include(x => x.Prontuario)
            .Include(x => x.Atendimentos)
            .Include(x => x.DocumentosClinicos)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        if (paciente == null) return NotFound();

        return View(paciente);
    }

    [Authorize(Roles = "Professor")]
    public IActionResult Create()
    {
        return View(new PacienteFormViewModel());
    }

    // CREATE: Com tratamento de erro e feedback (Apenas Professores)
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Professor")]
    public async Task<IActionResult> Create(PacienteFormViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try 
            {
                var paciente = new PacienteModel
                {
                    Id = Guid.NewGuid(),
                    DataCriacao = DateTime.UtcNow,
                    Ativo = true,
                    NomeCompleto = viewModel.NomeCompleto,
                    DataNascimento = viewModel.DataNascimento,
                    Idade = viewModel.Idade,
                    Sexo = viewModel.Sexo,
                    Naturalidade = viewModel.Naturalidade,
                    EstadoNascimento = viewModel.EstadoNascimento,
                    Escolaridade = viewModel.Escolaridade,
                    Profissao = viewModel.Profissao,
                    RG = viewModel.RG,
                    CPF = viewModel.CPF,
                    EstadoCivil = viewModel.EstadoCivil,
                    Religiao = viewModel.Religiao,
                    EnderecoLogradouro = viewModel.EnderecoLogradouro,
                    EnderecoNumero = viewModel.EnderecoNumero,
                    Bairro = viewModel.Bairro,
                    Cidade = viewModel.Cidade,
                    Estado = viewModel.Estado,
                    CEP = viewModel.CEP,
                    Telefone = viewModel.Telefone,
                    TelefoneRecado = viewModel.TelefoneRecado,
                    NomePai = viewModel.NomePai,
                    NomeMae = viewModel.NomeMae,
                    NomeResponsavel = viewModel.NomeResponsavel,
                    GrauParentescoResponsavel = viewModel.GrauParentescoResponsavel,
                    TratamentoPsicologico = viewModel.TratamentoPsicologico,
                    TratamentoNeurologico = viewModel.TratamentoNeurologico,
                    TratamentoPsiquiatrico = viewModel.TratamentoPsiquiatrico,
                    TratamentoCardiologico = viewModel.TratamentoCardiologico,
                    Internacao = viewModel.Internacao,
                    MotivoInternacao = viewModel.MotivoInternacao,
                    Observacoes = viewModel.Observacoes
                };

                var prontuario = new ProntuarioModel
                {
                    PacienteId = paciente.Id,
                    NumeroProntuario = await GenerateNumeroProntuarioAsync(),
                    DataPrimeiraConsulta = DateTime.UtcNow,
                    SituacaoProntuario = SituacaoProntuarioEnum.Ativo
                };

                paciente.Prontuario = prontuario;

                context.Add(paciente);
                await context.SaveChangesAsync();
                
                TempData["MensagemSucesso"] = "Paciente cadastrado com sucesso e prontuário criado automaticamente!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro técnico ao salvar. Verifique se o banco de dados está acessível.";
                ModelState.AddModelError("", "Não foi possível salvar o paciente.");
            }
        }
        return View(viewModel);
    }

    private async Task<string> GenerateNumeroProntuarioAsync()
    {
        var count = await context.Prontuarios.CountAsync();
        return $"{DateTime.UtcNow:yyyy}-{count + 1:000}";
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();

        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        // Validar autorização de escrita
        if (!await authorizationService.CanWritePacienteAsync(usuarioId, id.Value))
            return Forbid();

        var paciente = await context.Pacientes.FindAsync(id.Value);
        if (paciente == null) return NotFound();

        var viewModel = new PacienteFormViewModel
        {
            Id = paciente.Id,
            NomeCompleto = paciente.NomeCompleto,
            DataNascimento = paciente.DataNascimento,
            Sexo = paciente.Sexo,
            Naturalidade = paciente.Naturalidade,
            EstadoNascimento = paciente.EstadoNascimento,
            Escolaridade = paciente.Escolaridade,
            Profissao = paciente.Profissao,
            RG = paciente.RG,
            CPF = paciente.CPF,
            EstadoCivil = paciente.EstadoCivil,
            Religiao = paciente.Religiao,
            EnderecoLogradouro = paciente.EnderecoLogradouro,
            EnderecoNumero = paciente.EnderecoNumero,
            Bairro = paciente.Bairro,
            Cidade = paciente.Cidade,
            CEP = paciente.CEP,
            Telefone = paciente.Telefone,
            TelefoneRecado = paciente.TelefoneRecado,
            NomePai = paciente.NomePai,
            NomeMae = paciente.NomeMae,
            NomeResponsavel = paciente.NomeResponsavel,
            GrauParentescoResponsavel = paciente.GrauParentescoResponsavel,
            Observacoes = paciente.Observacoes
        };

        return View(viewModel);
    }

    // EDIT: Proteção contra concorrência e erros de atualização
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, PacienteFormViewModel viewModel)
    {
        if (id != viewModel.Id) return BadRequest();

        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        // Validar autorização de escrita
        if (!await authorizationService.CanWritePacienteAsync(usuarioId, id))
            return Forbid();

        if (ModelState.IsValid)
        {
            try
            {
                var paciente = await context.Pacientes.FindAsync(id);
                if (paciente == null) return NotFound();

                paciente.NomeCompleto = viewModel.NomeCompleto;
                paciente.DataNascimento = viewModel.DataNascimento;
                paciente.Idade = viewModel.Idade;
                paciente.Sexo = viewModel.Sexo;
                paciente.Naturalidade = viewModel.Naturalidade;
                paciente.EstadoNascimento = viewModel.EstadoNascimento;
                paciente.Escolaridade = viewModel.Escolaridade;
                paciente.Profissao = viewModel.Profissao;
                paciente.RG = viewModel.RG;
                paciente.CPF = viewModel.CPF;
                paciente.EstadoCivil = viewModel.EstadoCivil;
                paciente.Religiao = viewModel.Religiao;
                paciente.EnderecoLogradouro = viewModel.EnderecoLogradouro;
                paciente.EnderecoNumero = viewModel.EnderecoNumero;
                paciente.Bairro = viewModel.Bairro;
                paciente.Cidade = viewModel.Cidade;
                paciente.Estado = viewModel.Estado;
                paciente.CEP = viewModel.CEP;
                paciente.Telefone = viewModel.Telefone;
                paciente.TelefoneRecado = viewModel.TelefoneRecado;
                paciente.NomePai = viewModel.NomePai;
                paciente.NomeMae = viewModel.NomeMae;
                paciente.NomeResponsavel = viewModel.NomeResponsavel;
                paciente.GrauParentescoResponsavel = viewModel.GrauParentescoResponsavel;
                paciente.TratamentoPsicologico = viewModel.TratamentoPsicologico;
                paciente.TratamentoNeurologico = viewModel.TratamentoNeurologico;
                paciente.TratamentoPsiquiatrico = viewModel.TratamentoPsiquiatrico;
                paciente.TratamentoCardiologico = viewModel.TratamentoCardiologico;
                paciente.Internacao = viewModel.Internacao;
                paciente.MotivoInternacao = viewModel.MotivoInternacao;
                paciente.Observacoes = viewModel.Observacoes;
                paciente.DataAtualizacao = DateTime.UtcNow;

                context.Update(paciente);
                await context.SaveChangesAsync();
                
                TempData["MensagemSucesso"] = "Dados do paciente atualizados com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(id)) return NotFound();
                throw;
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Ocorreu um erro ao atualizar o paciente no banco de dados.";
            }
        }
        return View(viewModel);
    }

    [Authorize(Roles = "Professor")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null) return NotFound();

        var paciente = await context.Pacientes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        if (paciente == null) return NotFound();

        return View(paciente);
    }

    // DELETE: Com feedback de exclusão (Apenas Professores)
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Professor")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        try 
        {
            var paciente = await context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                context.Pacientes.Remove(paciente);
                await context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Paciente removido com sucesso.";
            }
        }
        catch (Exception)
        {
            TempData["MensagemErro"] = "Não é possível excluir este paciente pois ele possui registros vinculados.";
        }

        return RedirectToAction(nameof(Index));
    }

    private bool PacienteExists(Guid id)
    {
        return context.Pacientes.Any(x => x.Id == id);
    }
}