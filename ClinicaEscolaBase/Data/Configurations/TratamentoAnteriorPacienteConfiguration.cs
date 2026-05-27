using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Configurations;

public class TratamentoAnteriorPacienteConfiguration : IEntityTypeConfiguration<TratamentoAnteriorPacienteModel>
{
    public void Configure(EntityTypeBuilder<TratamentoAnteriorPacienteModel> builder)
    {
        builder.ToTable("TratamentosAnterioresPaciente");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.MotivoInternacao).HasColumnType("nvarchar(max)");
        builder.Property(x => x.Observacoes).HasColumnType("nvarchar(max)");

        builder.HasOne(x => x.Paciente)
            .WithMany(x => x.TratamentosAnteriores)
            .HasForeignKey(x => x.PacienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
