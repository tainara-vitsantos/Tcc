using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Controllers;

[Authorize(Roles = "Professor")]
public class AuditoriaController : Controller
{
    private readonly ApplicationDbContext _context;

    public AuditoriaController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Auditoria
    public async Task<IActionResult> Index(int? pageNumber, string? usuario, string? entidade, TipoAcaoAuditoria? acao)
    {
        var query = _context.Auditorias
            .Include(a => a.Usuario)
            .AsQueryable();

        // Filtros
        if (!string.IsNullOrEmpty(usuario))
        {
            query = query.Where(a => a.Usuario.NomeCompleto.Contains(usuario));
        }

        if (!string.IsNullOrEmpty(entidade))
        {
            query = query.Where(a => a.Entidade.Contains(entidade));
        }

        if (acao.HasValue)
        {
            query = query.Where(a => a.TipoAcao == acao.Value);
        }

        // Ordenar por data decrescente
        query = query.OrderByDescending(a => a.DataHora);

        int pageSize = 20;
        var paginatedList = await PaginatedList<Auditoria>.CreateAsync(query, pageNumber ?? 1, pageSize);

        // ViewData para filtros
        ViewData["Usuario"] = usuario;
        ViewData["Entidade"] = entidade;
        ViewData["Acao"] = acao;

        return View(paginatedList);
    }

    // GET: Auditoria/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var auditoria = await _context.Auditorias
            .Include(a => a.Usuario)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (auditoria == null)
        {
            return NotFound();
        }

        return View(auditoria);
    }
}

// Classe auxiliar para paginação
public class PaginatedList<T> : List<T>
{
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}