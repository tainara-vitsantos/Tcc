using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Services;

/// <summary>
/// Serviço para implementar Soft Delete (exclusão lógica).
/// Marca registros sensíveis como inativos em vez de deletar do BD,
/// preservando o histórico acadêmico e de auditoria.
/// </summary>
public class SoftDeleteService(ApplicationDbContext context) 
{

    /// <summary>
    /// Marca um DocumentoClinico como inativo (soft delete).
    /// </summary>
    public async Task SoftDeleteDocumentoAsync(int documentoId, string usuarioId, string motivo = "")
    {
        var documento = await context.DocumentosClinicos.FindAsync(documentoId);
        if (documento != null)
        {
            documento.Ativo = false;
            documento.ExcluidoLogicamente = true;
            documento.DataAtualizacao = DateTime.UtcNow;

            context.DocumentosClinicos.Update(documento);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Marca um Atendimento como inativo (soft delete).
    /// </summary>
    public async Task SoftDeleteAtendimentoAsync(int atendimentoId)
    {
        var atendimento = await context.Atendimentos.FindAsync(atendimentoId);
        if (atendimento != null)
        {
            atendimento.Ativo = false;
            atendimento.DataAtualizacao = DateTime.UtcNow;

            context.Atendimentos.Update(atendimento);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Marca uma Evolução de Atendimento como inativa.
    /// </summary>
    public async Task SoftDeleteEvolucaoAsync(int evolucaoId)
    {
        var evolucao = await context.EvolucoesAtendimento.FindAsync(evolucaoId);
        if (evolucao != null)
        {
            evolucao.Ativo = false;
            evolucao.DataAtualizacao = DateTime.UtcNow;

            context.EvolucoesAtendimento.Update(evolucao);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Restaura um registro marcado como inativo.
    /// </summary>
    public async Task RestoreDocumentoAsync(int documentoId)
    {
        var documento = await context.DocumentosClinicos.FindAsync(documentoId);
        if (documento != null)
        {
            documento.Ativo = true;
            documento.ExcluidoLogicamente = false;
            documento.DataAtualizacao = DateTime.UtcNow;

            context.DocumentosClinicos.Update(documento);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Retorna apenas registros ativos ao fazer queries.
    /// Deve ser usado como padrão em includes e consultas.
    /// </summary>
    public IQueryable<T> FilterActive<T>(DbSet<T> dbSet) where T : EntityBase
    {
        return dbSet.Where(e => e.Ativo).AsQueryable();
    }
}
