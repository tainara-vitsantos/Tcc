using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Data.Configurations;

public class InfoFamiliarConfiguration : IEntityTypeConfiguration<InfoFamiliarModel>
{
    public void Configure(EntityTypeBuilder<InfoFamiliarModel> builder)
    {
        builder.ToTable("InfosFamiliares");
        builder.HasKey(x => x.Id);

        builder.Property<int>("PacienteId");
        builder.Property(x => x.NomeCompleto);
        builder.Property(x => x.Parentesco).HasConversion<int?>();
        builder.Property(x => x.GrauInstrucao);
        builder.Property(x => x.Profissao);
        builder.Property(x => x.DataNascimento);
        builder.Property(x => x.RG);
        builder.Property(x => x.CPF);
        builder.Property(x => x.Telefone);
        builder.Property(x => x.Email);
        builder.Property(x => x.CondicaoConjugal);
        builder.Property(x => x.ResponsavelPrincipal);

        builder.OwnsOne(x => x.Endereco, endereco =>
        {
            endereco.Property(e => e.Logradouro).HasColumnName("EnderecoLogradouro");
            endereco.Property(e => e.Numero).HasColumnName("EnderecoNumero");
            endereco.Property(e => e.Bairro).HasColumnName("EnderecoBairro");
            endereco.Property(e => e.Cidade).HasColumnName("EnderecoCidade");
            endereco.Property(e => e.Estado).HasColumnName("EnderecoEstado");
            endereco.Property(e => e.CEP).HasColumnName("EnderecoCep");
        });

        builder.HasOne(x => x.Paciente)
            .WithMany(x => x.Familiares)
            .HasForeignKey("PacienteId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}