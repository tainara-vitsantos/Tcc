using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Services.Interfaces;

/// <summary>
/// Contrato para o serviço de trilha de auditoria automática.
/// </summary>
public interface IAuditService
{
    /// <summary>
    /// Registra uma ação de auditoria genérica.
    /// </summary>
    Task LogAsync(
        string usuarioId,
        TipoAcaoAuditoria tipoAcao,
        string entidade,
        string registroId,
        Guid? pacienteId = null,
        int? prontuarioId = null,
        string? observacoes = null,
        object? valoresAntes = null,
        object? valoresDepois = null);

    /// <summary>
    /// Registra visualização de prontuário.
    /// </summary>
    Task LogVisualizacaoProntuarioAsync(string usuarioId, int prontuarioId, Guid pacienteId);

    /// <summary>
    /// Registra criação de documento clínico.
    /// </summary>
    Task LogCriacaoDocumentoAsync(
        string usuarioId,
        int documentoId,
        Guid pacienteId,
        int? prontuarioId,
        TipoDocumentoClinico tipo);

    /// <summary>
    /// Registra edição de documento clínico.
    /// </summary>
    Task LogEdicaoDocumentoAsync(
        string usuarioId,
        int documentoId,
        Guid pacienteId,
        int? prontuarioId,
        TipoDocumentoClinico tipo,
        object? valoresAntes = null,
        object? valoresDepois = null);

    /// <summary>
    /// Registra exclusão lógica de documento.
    /// </summary>
    Task LogExclusaoDocumentoAsync(
        string usuarioId,
        int documentoId,
        Guid pacienteId,
        int? prontuarioId,
        string motivo = "");

    /// <summary>
    /// Persiste os logs de auditoria no banco de dados.
    /// </summary>
    Task SaveAuditAsync();
}