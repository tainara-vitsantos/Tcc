using ClinicaEscolaBase.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUserModel>(options)
{
    public DbSet<PacienteModel> Pacientes => Set<PacienteModel>();
     public DbSet<InfoFamiliarModel> Familiares => Set<InfoFamiliarModel>();
    public DbSet<ProntuarioModel> Prontuarios => Set<ProntuarioModel>();
    public DbSet<TratamentoAnteriorModel> TratamentosAnterioresPaciente => Set<TratamentoAnteriorModel>();
    public DbSet<AtendimentoModel> Atendimentos => Set<AtendimentoModel>();
    public DbSet<DocumentoClinicoModel> DocumentosClinicos => Set<DocumentoClinicoModel>();
    public DbSet<AnamneseAdultoModel> AnamnesesAdulto => Set<AnamneseAdultoModel>();
    public DbSet<AnamneseAdolescenteModel> AnamnesesAdolescente => Set<AnamneseAdolescenteModel>();
    public DbSet<EvolucaoAtendimentoModel> EvolucoesAtendimento => Set<EvolucaoAtendimentoModel>();
    public DbSet<TermoPsicoterapiaIndividualModel> TermosPsicoterapiaIndividual => Set<TermoPsicoterapiaIndividualModel>();
    public DbSet<TermoAutorizacaoMenorModel> TermosAutorizacaoMenor => Set<TermoAutorizacaoMenorModel>();
     public DbSet<AnexoModel> Anexos => Set<AnexoModel>();
    public DbSet<AuditoriaModel> Auditorias => Set<AuditoriaModel>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

          base.OnModelCreating(builder);       
       builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
