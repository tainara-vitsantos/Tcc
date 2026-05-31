using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Data.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUserModel>
{
    public void Configure(EntityTypeBuilder<ApplicationUserModel> builder)
    {
        builder.Property(x => x.NomeCompleto);
        builder.Property(x => x.Cpf);
        builder.Property(x => x.Matricula);
        builder.Property(x => x.Crp);
        builder.Property(x => x.TipoUsuario).HasConversion<int>();
        builder.Property(x => x.Ativo);

        builder.HasMany(x => x.DocumentosCriados)
            .WithOne(x => x.CriadoPorUsuario)
            .HasForeignKey(x => x.CriadoPorUsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.EvolucoesCriadas)
            .WithOne(x => x.CriadoPorUsuario)
            .HasForeignKey(x => x.CriadoPorUsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.AnexosEnviados)
            .WithOne(x => x.EnviadoPorUsuario)
            .HasForeignKey(x => x.EnviadoPorUsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Auditorias)
            .WithOne(x => x.Usuario)
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
