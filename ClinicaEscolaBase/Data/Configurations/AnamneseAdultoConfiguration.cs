using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Configurations;

public class AnamneseAdultoConfiguration : IEntityTypeConfiguration<AnamneseAdultoModel>
{
    public void Configure(EntityTypeBuilder<AnamneseAdultoModel> builder)
    {
        builder.ToTable("AnamnesesAdulto");
        builder.HasKey(x => x.DocumentoClinicoId);

        builder.Property(x => x.DocumentoClinicoId);
        builder.Property(x => x.QueixaPrincipal);
        builder.Property(x => x.QueixaSecundaria);
        builder.Property(x => x.Sintomas);
        builder.Property(x => x.InicioPatologia);
        builder.Property(x => x.FrequenciaPatologia);
        builder.Property(x => x.IntensidadePatologia);
        builder.Property(x => x.TratamentosAnteriores);
        builder.Property(x => x.Medicamentos);
        builder.Property(x => x.HistoriaInfancia);
        builder.Property(x => x.Rotina);
        builder.Property(x => x.Vicios);
        builder.Property(x => x.Hobbies);
        builder.Property(x => x.Trabalho);
        builder.Property(x => x.HistoricoFamiliarPais);
        builder.Property(x => x.HistoricoFamiliarIrmaos);
        builder.Property(x => x.HistoricoFamiliarConjuge);
        builder.Property(x => x.HistoricoFamiliarFilhos);
        builder.Property(x => x.HistoricoFamiliarLar);
        builder.Property(x => x.HistoriaPatologicaPregressa);
        builder.Property(x => x.ExameAparencia);
        builder.Property(x => x.ExameComportamento);
        builder.Property(x => x.Atitude);
        builder.Property(x => x.AtitudeEntrevistador);
        builder.Property(x => x.OrientacaoAutoIdentificatoria);
        builder.Property(x => x.OrientacaoCorporal);
        builder.Property(x => x.OrientacaoTemporal);
        builder.Property(x => x.OrientacaoEspacial);
        builder.Property(x => x.OrientacaoPatologia);
        builder.Property(x => x.ObservacoesOrientacao);
        builder.Property(x => x.Sensopercepcao);
        builder.Property(x => x.AtencaoVigilancia);
        builder.Property(x => x.AtencaoTenacidade);
        builder.Property(x => x.Memoria);
        builder.Property(x => x.Inteligencia);
        builder.Property(x => x.SensopercepcaoNormal);
        builder.Property(x => x.SensopercepcaoAlucinacao);
        builder.Property(x => x.PensamentoAcelerado);
        builder.Property(x => x.PensamentoRetardado);
        builder.Property(x => x.PensamentoFuga);
        builder.Property(x => x.PensamentoBloqueio);
        builder.Property(x => x.PensamentoProlixo);
        builder.Property(x => x.PensamentoRepeticao);
        builder.Property(x => x.PensamentoVelocidade);
        builder.Property(x => x.ConteudoObsessoes);
        builder.Property(x => x.ConteudoHipocondrias);
        builder.Property(x => x.ConteudoFobias);
        builder.Property(x => x.ConteudoDelirios);
        builder.Property(x => x.ConteudoPensamento);
        builder.Property(x => x.TendenciaSuicida);
        builder.Property(x => x.ExpansaoEu);
        builder.Property(x => x.RetracaoEu);
        builder.Property(x => x.NegacaoEu);
        builder.Property(x => x.Afetividade);
        builder.Property(x => x.Humor);
        builder.Property(x => x.ConscienciaDoencaAtual);
        builder.Property(x => x.HipoteseDiagnostica);

        builder.HasOne(x => x.DocumentoClinico)
            .WithOne()
            .HasForeignKey<AnamneseAdultoModel>(x => x.DocumentoClinicoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
