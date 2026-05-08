using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Controllers;

public class ProntuarioController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProntuarioController(ApplicationDbContext context)
    {
        _context = context;
    }

    // LISTAGEM GERAL
    public async Task<IActionResult> Index()
    {
        var prontuarios = await _context.Prontuarios
            .Include(x => x.Paciente)
            .AsNoTracking()
            .OrderBy(x => x.NumeroProntuario)
            .ToListAsync();

        return View(prontuarios);
    }

    // DETALHES DO PRONTUÁRIO
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var prontuario = await _context.Prontuarios
            .Include(x => x.Paciente)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        if (prontuario == null) return NotFound();

        return View(prontuario);
    }

    // GET: CREATE (Suporta criação direta via PacienteId)
    public async Task<IActionResult> Create(Guid? pacienteId)
    {
        if (pacienteId.HasValue)
        {
            var paciente = await _context.Pacientes.FindAsync(pacienteId.Value);
            if (paciente != null)
            {
                ViewBag.PacienteNome = paciente.NomeCompleto;
                var novoProntuario = new Prontuario 
                { 
                    PacienteId = pacienteId.Value,
                    DataPrimeiraConsulta = DateTime.Now,
                    SituacaoProntuario = SituacaoProntuario.Ativo
                };
                return View(novoProntuario);
            }
        }

        await PopulatePacientesDropDownList();
        return View();
    }

    // POST: CREATE
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Prontuario prontuario)
    {
        // Limpa validações de navegação para evitar erros no ModelState
        ModelState.Remove("Paciente");
        ModelState.Remove("Atendimentos");

        if (ModelState.IsValid)
        {
            try 
            {
                _context.Add(prontuario);
                await _context.SaveChangesAsync();
                
                TempData["MensagemSucesso"] = "Prontuário aberto com sucesso! Agora você já pode agendar atendimentos.";
                
                // Redireciona para a ficha do paciente para seguir o fluxo
                return RedirectToAction("Details", "Paciente", new { id = prontuario.PacienteId });
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro ao salvar o prontuário. Verifique se o número do prontuário já existe.";
            }
        }

        await PopulatePacientesDropDownList(prontuario.PacienteId);
        return View(prontuario);
    }

    // GET: EDIT
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var prontuario = await _context.Prontuarios
            .Include(p => p.Paciente)
            .FirstOrDefaultAsync(p => p.Id == id.Value);

        if (prontuario == null) return NotFound();

        ViewBag.PacienteNome = prontuario.Paciente?.NomeCompleto;
        return View(prontuario);
    }

    // POST: EDIT
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Prontuario prontuario)
    {
        if (id != prontuario.Id) return BadRequest();

        ModelState.Remove("Paciente");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(prontuario);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Prontuário atualizado com sucesso.";
                return RedirectToAction("Details", "Paciente", new { id = prontuario.PacienteId });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProntuarioExists(prontuario.Id)) return NotFound();
                throw;
            }
        }
        return View(prontuario);
    }

    // GET: DELETE
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var prontuario = await _context.Atendimentos
            .Include(x => x.Paciente)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        if (prontuario == null) return NotFound();

        return View(prontuario);
    }

    // POST: DELETE
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var prontuario = await _context.Prontuarios.FindAsync(id);
        if (prontuario != null)
        {
            var pacienteId = prontuario.PacienteId;
            _context.Prontuarios.Remove(prontuario);
            await _context.SaveChangesAsync();
            TempData["MensagemSucesso"] = "Prontuário excluído com sucesso.";
            return RedirectToAction("Details", "Paciente", new { id = pacienteId });
        }

        return RedirectToAction(nameof(Index));
    }

    // MÉTODOS AUXILIARES
    private async Task PopulatePacientesDropDownList(Guid? selectedPacienteId = null)
    {
        // Só mostra pacientes que AINDA NÃO possuem prontuário
        var pacientesQuery = _context.Pacientes
            .AsNoTracking()
            .Where(p => !_context.Prontuarios.Any(pr => pr.PacienteId == p.Id))
            .OrderBy(p => p.NomeCompleto);

        ViewData["PacienteId"] = new SelectList(await pacientesQuery.ToListAsync(), "Id", "NomeCompleto", selectedPacienteId);
    }

    private bool ProntuarioExists(int id)
    {
        return _context.Prontuarios.Any(x => x.Id == id);
    }
}