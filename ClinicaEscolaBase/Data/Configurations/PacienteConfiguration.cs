using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Data.Configurations;

public class PacienteConfiguration : IEntityTypeConfiguration<PacienteModel>
{
   public void Configure(EntityTypeBuilder<PacienteModel> builder)
    {        
        builder.ToTable("Pacientes");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.DataCriacao);
        builder.Property(p => p.DataAtualizacao);
        builder.Property(p => p.Ativo);
        builder.Property(p => p.NomeCompleto);
        builder.Property(p => p.Telefone);
        builder.Property(p => p.TelefoneRecado);
        builder.Property(p => p.Sexo);
        builder.Property(p => p.Naturalidade);
        builder.Property(p => p.EstadoNascimento);
        builder.Property(p => p.Escolaridade);
        builder.Property(p => p.Profissao);
        builder.Property(p => p.RG);
        builder.Property(p => p.CPF);
        builder.Property(p => p.EstadoCivil);
        builder.Property(p => p.Religiao);
        builder.Property(p => p.DataNascimento);
        builder.Property(p => p.FamiliarResponsavelId);
        builder.Ignore(p => p.Idade);

        builder.OwnsOne(p => p.Endereco, endereco =>
        {
            endereco.Property(e => e.Logradouro).HasColumnName("EnderecoLogradouro");
            endereco.Property(e => e.Numero).HasColumnName("EnderecoNumero");
            endereco.Property(e => e.Bairro).HasColumnName("EnderecoBairro");
            endereco.Property(e => e.Cidade).HasColumnName("EnderecoCidade");
            endereco.Property(e => e.Estado).HasColumnName("EnderecoEstado");
            endereco.Property(e => e.CEP).HasColumnName("EnderecoCep");
        });

        builder.HasOne(p => p.FamiliarResponsavel)
            .WithMany()
            .HasForeignKey(p => p.FamiliarResponsavelId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
