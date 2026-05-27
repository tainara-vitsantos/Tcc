using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Configurations;

public class PlantaoPsicologicoConfiguration : IEntityTypeConfiguration<PlantaoPsicologicoModel>
{
    public void Configure(EntityTypeBuilder<PlantaoPsicologicoModel> builder)
    {
        builder.ToTable("PlantoesPsicologicos");
        builder.HasKey(x => x.DocumentoClinicoId);

        builder.HasOne(x => x.DocumentoClinico)
            .WithOne(x => x.PlantaoPsicologico)
            .HasForeignKey<PlantaoPsicologicoModel>(x => x.DocumentoClinicoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
