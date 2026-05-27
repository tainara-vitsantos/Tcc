using ClinicaEscolaBase.Configurations;
using ClinicaEscolaBase.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<PacienteModel> Pacientes => Set<PacienteModel>();
    public DbSet<ProntuarioModel> Prontuarios => Set<ProntuarioModel>();
    public DbSet<TratamentoAnteriorPacienteModel> TratamentosAnterioresPaciente => Set<TratamentoAnteriorPacienteModel>();
    public DbSet<ResponsavelLegal> ResponsaveisLegais => Set<ResponsavelLegal>();
    public DbSet<VinculoAlunoPacienteModel> VinculosAlunoPaciente => Set<VinculoAlunoPacienteModel>();
    public DbSet<Atendimento> Atendimentos => Set<Atendimento>();
    public DbSet<DocumentoClinico> DocumentosClinicos => Set<DocumentoClinico>();
    public DbSet<DocumentoIdentificacaoPaciente> DocumentosIdentificacaoPaciente => Set<DocumentoIdentificacaoPaciente>();
    public DbSet<AnamneseAdulto> AnamnesesAdulto => Set<AnamneseAdulto>();
    public DbSet<AnamneseAdolescente> AnamnesesAdolescente => Set<AnamneseAdolescente>();
    public DbSet<PlantaoPsicologicoModel> PlantaoPsicologico => Set<PlantaoPsicologicoModel>();
    public DbSet<EvolucaoAtendimentoModel> EvolucoesAtendimento => Set<EvolucaoAtendimentoModel>();
    public DbSet<TermoPsicoterapiaIndividualModel> TermosPsicoterapiaIndividual => Set<TermoPsicoterapiaIndividualModel>();
    public DbSet<TermoAutorizacaoMenorModel> TermosAutorizacaoMenor => Set<TermoAutorizacaoMenorModel>();
    public DbSet<TermoCompromissoInformatizacaoModel> TermosCompromissoInformatizacao => Set<TermoCompromissoInformatizacaoModel>();
    public DbSet<TermoResponsabilidadeEstagiarioModel> TermosResponsabilidadeEstagiario => Set<TermoResponsabilidadeEstagiarioModel>();
    public DbSet<Anexo> Anexos => Set<Anexo>();
    public DbSet<Auditoria> Auditorias => Set<Auditoria>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new PacienteConfiguration());
        builder.ApplyConfiguration(new ProntuarioConfiguration());
        builder.ApplyConfiguration(new TratamentoAnteriorPacienteConfiguration());
        builder.ApplyConfiguration(new ResponsavelLegalConfiguration());
        builder.ApplyConfiguration(new VinculoAlunoPacienteConfiguration());
        builder.ApplyConfiguration(new AtendimentoConfiguration());
        builder.ApplyConfiguration(new DocumentoClinicoConfiguration());
        builder.ApplyConfiguration(new DocumentoIdentificacaoPacienteConfiguration());
        builder.ApplyConfiguration(new AnamneseAdultoConfiguration());
        builder.ApplyConfiguration(new AnamneseAdolescenteConfiguration());
        builder.ApplyConfiguration(new PlantaoPsicologicoConfiguration());
        builder.ApplyConfiguration(new EvolucaoAtendimentoConfiguration());
        builder.ApplyConfiguration(new TermoPsicoterapiaIndividualConfiguration());
        builder.ApplyConfiguration(new TermoAutorizacaoMenorConfiguration());
        builder.ApplyConfiguration(new TermoCompromissoInformatizacaoConfiguration());
        builder.ApplyConfiguration(new TermoResponsabilidadeEstagiarioConfiguration());
        builder.ApplyConfiguration(new AnexoConfiguration());
        builder.ApplyConfiguration(new AuditoriaConfiguration());
    }
}
