using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Services.Interfaces;
using System.Text.Json;

namespace ClinicaEscolaBase.Services;

/// <summary>
/// Serviço para implementar a trilha de auditoria automática.
/// Registra: Quem fez, Quando fez e Qual registro foi afetado.
/// </summary>
public class AuditService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IAuditService
{

    /// <summary>
    /// Registra uma ação de auditoria.
    /// </summary>
    public Task LogAsync(
        string usuarioId,
        TipoAcaoAuditoriaEnum tipoAcao,
        string entidade,
        string registroId,
        Guid? pacienteId = null,
        int? prontuarioId = null,
        string? observacoes = null,
        object? valoresAntes = null,
        object? valoresDepois = null)
    {
        var httpContext = httpContextAccessor.HttpContext;
        var ip = httpContext?.Connection?.RemoteIpAddress?.ToString();
        var userAgent = httpContext?.Request?.Headers["User-Agent"].ToString();

        var auditoria = new Auditoria
        {
            UsuarioId = usuarioId,
            TipoAcao = tipoAcao,
            Entidade = entidade,
            RegistroId = registroId,
            PacienteId = pacienteId,
            ProntuarioId = prontuarioId,
            DataHora = DateTime.UtcNow,
            IP = ip,
            UserAgent = userAgent,
            ValoresAntesJson = valoresAntes != null ? JsonSerializer.Serialize(valoresAntes) : null,
            ValoresDepoisJson = valoresDepois != null ? JsonSerializer.Serialize(valoresDepois) : null,
            Observacoes = observacoes
        };

        context.Auditorias.Add(auditoria);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Registra visualização de prontuário.
    /// </summary>
    public async Task LogVisualizacaoProntuarioAsync(string usuarioId, int prontuarioId, Guid pacienteId)
    {
        await LogAsync(
            usuarioId,
            TipoAcaoAuditoriaEnum.Visualizacao,
            nameof(ProntuarioModel),
            prontuarioId.ToString(),
            pacienteId,
            prontuarioId,
            "Prontuário visualizado");
    }

    /// <summary>
    /// Registra criação de documento clínico.
    /// </summary>
    public async Task LogCriacaoDocumentoAsync(
        string usuarioId,
        int documentoId,
        Guid pacienteId,
        int? prontuarioId,
        TipoDocumentoClinicoEnum tipo)
    {
        await LogAsync(
            usuarioId,
            TipoAcaoAuditoriaEnum.Insercao,
            nameof(DocumentoClinico),
            documentoId.ToString(),
            pacienteId,
            prontuarioId,
            $"Documento clínico criado: {tipo}");
    }

    /// <summary>
    /// Registra edição de documento clínico.
    /// </summary>
    public async Task LogEdicaoDocumentoAsync(
        string usuarioId,
        int documentoId,
        Guid pacienteId,
        int? prontuarioId,
        TipoDocumentoClinicoEnum tipo,
        object? valoresAntes = null,
        object? valoresDepois = null)
    {
        await LogAsync(
            usuarioId,
            TipoAcaoAuditoriaEnum.Atualizacao,
            nameof(DocumentoClinico),
            documentoId.ToString(),
            pacienteId,
            prontuarioId,
            $"Documento clínico editado: {tipo}",
            valoresAntes,
            valoresDepois);
    }

    /// <summary>
    /// Registra exclusão lógica de documento.
    /// </summary>
    public async Task LogExclusaoDocumentoAsync(
        string usuarioId,
        int documentoId,
        Guid pacienteId,
        int? prontuarioId,
        string motivo = "")
    {
        await LogAsync(
            usuarioId,
            TipoAcaoAuditoriaEnum.ExclusaoLogica,
            nameof(DocumentoClinico),
            documentoId.ToString(),
            pacienteId,
            prontuarioId,
            $"Documento clínico marcado como inativo. Motivo: {motivo}");
    }

    /// <summary>
    /// Persiste os logs de auditoria no banco de dados.
    /// Deve ser chamado após o SaveChangesAsync do DbContext.
    /// </summary>
    public async Task SaveAuditAsync()
    {
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log em arquivo ou sistema externo se necessário
            Console.WriteLine($"Erro ao salvar auditoria: {ex.Message}");
        }
    }
}
