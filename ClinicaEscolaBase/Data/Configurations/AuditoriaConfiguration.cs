using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Data.Configurations;

public class AuditoriaConfiguration : IEntityTypeConfiguration<AuditoriaModel>
{
    public void Configure(EntityTypeBuilder<AuditoriaModel> builder)
    {
        builder.ToTable("Auditorias");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DataCriacao);
        builder.Property(x => x.DataAtualizacao);
        builder.Property(x => x.Ativo);
        builder.Property(x => x.UsuarioId);
        builder.Property(x => x.TipoAcao).HasConversion<int>();
        builder.Property(x => x.Entidade);
        builder.Property(x => x.RegistroId);
        builder.Property(x => x.PacienteId);
        builder.Property(x => x.ProntuarioId);
        builder.Property(x => x.DataHora);
        builder.Property(x => x.IP);
        builder.Property(x => x.UserAgent);
        builder.Property(x => x.ValoresAntesJson);
        builder.Property(x => x.ValoresDepoisJson);
        builder.Property(x => x.Observacoes);

        builder.HasOne(x => x.Usuario)
            .WithMany(x => x.Auditorias)
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Paciente)
            .WithMany(x => x.Auditorias)
            .HasForeignKey(x => x.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Prontuario)
            .WithMany(x => x.Auditorias)
            .HasForeignKey(x => x.ProntuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
