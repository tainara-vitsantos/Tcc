using ClinicaEscolaBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEscolaBase.Configurations;

public class TermoPsicoterapiaIndividualConfiguration : IEntityTypeConfiguration<TermoPsicoterapiaIndividualModel>
{
    public void Configure(EntityTypeBuilder<TermoPsicoterapiaIndividualModel> builder)
    {
        builder.ToTable("TermosPsicoterapiaIndividual");
        builder.HasKey(x => x.DocumentoClinicoId);

        builder.HasOne(x => x.DocumentoClinico)
            .WithOne(x => x.TermoPsicoterapiaIndividual)
            .HasForeignKey<TermoPsicoterapiaIndividualModel>(x => x.DocumentoClinicoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
