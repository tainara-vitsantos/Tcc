using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Controllers;

public class PacienteController : Controller
{
    private readonly ApplicationDbContext _context;

    public PacienteController(ApplicationDbContext context)
    {
        _context = context;
    }

    // LISTAGEM: Ordenada por nome para melhor UX
    public async Task<IActionResult> Index()
    {
        var pacientes = await _context.Pacientes
            .AsNoTracking()
            .OrderBy(x => x.NomeCompleto)
            .ToListAsync();

        return View(pacientes);
    }

    // DETALHES: Carrega Prontuário e Atendimentos (Eager Loading)
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null) return NotFound();

        var paciente = await _context.Pacientes
            .Include(x => x.Prontuario)
            .Include(x => x.Atendimentos)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        if (paciente == null) return NotFound();

        return View(paciente);
    }

    public IActionResult Create()
    {
        return View();
    }

    // CREATE: Com tratamento de erro e feedback
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Paciente paciente)
    {
        if (ModelState.IsValid)
        {
            try 
            {
                paciente.Id = Guid.NewGuid();
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                
                TempData["MensagemSucesso"] = "Paciente cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro técnico ao salvar. Verifique se o banco de dados está acessível.";
                ModelState.AddModelError("", "Não foi possível salvar o paciente.");
            }
        }
        return View(paciente);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();

        var paciente = await _context.Pacientes.FindAsync(id.Value);
        if (paciente == null) return NotFound();

        return View(paciente);
    }

    // EDIT: Proteção contra concorrência e erros de atualização
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, Paciente paciente)
    {
        if (id != paciente.Id) return BadRequest();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(paciente);
                await _context.SaveChangesAsync();
                
                TempData["MensagemSucesso"] = "Dados do paciente atualizados com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(paciente.Id)) return NotFound();
                throw;
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Ocorreu um erro ao atualizar o paciente no banco de dados.";
            }
        }
        return View(paciente);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null) return NotFound();

        var paciente = await _context.Pacientes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        if (paciente == null) return NotFound();

        return View(paciente);
    }

    // DELETE: Com feedback de exclusão
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        try 
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
                await _context.SaveChangesAsync();
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
        return _context.Pacientes.Any(x => x.Id == id);
    }
}