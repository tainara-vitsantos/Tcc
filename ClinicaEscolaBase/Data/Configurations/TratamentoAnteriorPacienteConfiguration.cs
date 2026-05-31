using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Configurations;

public class TratamentoAnteriorPacienteConfiguration : IEntityTypeConfiguration<TratamentoAnteriorModel>
{
    public void Configure(EntityTypeBuilder<TratamentoAnteriorModel> builder)
    {
        builder.ToTable("TratamentosAnterioresPaciente");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.DataCriacao);
        builder.Property(x => x.DataAtualizacao);
        builder.Property(x => x.Ativo);
        builder.Property(x => x.PacienteId);
        builder.Property(x => x.TipoTratamento).HasConversion<int>();
        builder.Property(x => x.Internacao);
        builder.Property(x => x.MotivoInternacao).HasColumnType("nvarchar(max)");
        builder.Property(x => x.Observacoes).HasColumnType("nvarchar(max)");

        builder.HasOne(x => x.Paciente)
            .WithMany(x => x.TratamentosAnteriores)
            .HasForeignKey(x => x.PacienteId)
            .OnDelete(DeleteBehavior.Cascade);

       
    }
}
