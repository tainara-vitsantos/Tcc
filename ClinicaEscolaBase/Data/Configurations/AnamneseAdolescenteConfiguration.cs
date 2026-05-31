using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Data.Configurations;

public class AnamneseAdolescenteConfiguration : IEntityTypeConfiguration<AnamneseAdolescenteModel>
{
    public void Configure(EntityTypeBuilder<AnamneseAdolescenteModel> builder)
    {
        builder.ToTable("AnamnesesAdolescente");
        builder.HasKey(x => x.DocumentoClinicoId);

        builder.Property(x => x.DocumentoClinicoId);
        builder.Property(x => x.Escolaridade);
        builder.Property(x => x.Escola);
        builder.Property(x => x.QueixaPrincipal);
        builder.Property(x => x.DesdeQuando);
        builder.Property(x => x.AtitudeMaeFrenteQueixa);
        builder.Property(x => x.AtitudePaiFrenteQueixa);
        builder.Property(x => x.AtitudeOutrosFamiliares);
        builder.Property(x => x.FoiDesejado);
        builder.Property(x => x.Linguagem);
        builder.Property(x => x.DesenvolvimentoPsicomotor);
        builder.Property(x => x.Sono);
        builder.Property(x => x.Alimentacao);
        builder.Property(x => x.Tiques);
        builder.Property(x => x.DificuldadeEscolar);
        builder.Property(x => x.FamiliarNervoso);
        builder.Property(x => x.DescricaoFamiliarNervoso);
        builder.Property(x => x.FamiliarProblemaMental);
        builder.Property(x => x.ViciosFamilia);
        builder.Property(x => x.LocalEstudo);
        builder.Property(x => x.TiposDiversao);
        builder.Property(x => x.FamiliaFazVisitas);
        builder.Property(x => x.FamiliaRecebeVisitas);
        builder.Property(x => x.Companheiros);
        builder.Property(x => x.QuemEscolheCompanheiros);
        builder.Property(x => x.ResponsavelPrincipalId);

        builder.HasOne(x => x.DocumentoClinico)
            .WithOne()
            .HasForeignKey<AnamneseAdolescenteModel>(x => x.DocumentoClinicoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.ResponsavelPrincipal)
            .WithMany()
            .HasForeignKey(x => x.ResponsavelPrincipalId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
