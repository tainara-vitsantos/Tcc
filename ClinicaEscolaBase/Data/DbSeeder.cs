using Microsoft.AspNetCore.Identity;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Enums;

namespace ClinicaEscolaBase.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        // 1. Criar Roles
        string[] roles = { "Professor", "Aluno" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // 2. Criar Professor
        ApplicationUser? prof = await userManager.FindByEmailAsync("prof@fatec.com");
        if (prof == null)
        {
            prof = new ApplicationUser {
                UserName = "prof@fatec.com",
                Email = "prof@fatec.com",
                NomeCompleto = "Tainara Vitória (Supervisor)",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(prof, "Senha@123");
            await userManager.AddToRoleAsync(prof, "Professor");
        }

        // 3. Criar Aluno
        ApplicationUser? aluno = await userManager.FindByEmailAsync("aluno@fatec.com");
        if (aluno == null)
        {
            aluno = new ApplicationUser {
                UserName = "aluno@fatec.com",
                Email = "aluno@fatec.com",
                NomeCompleto = "Aluno Estagiário",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(aluno, "Senha@123");
            await userManager.AddToRoleAsync(aluno, "Aluno");
        }

        // 4. Criar Pacientes de Teste
        if (!context.Pacientes.Any())
        {
            var pacientes = new[]
            {
                new Paciente
                {
                    Id = Guid.NewGuid(),
                    NomeCompleto = "João Silva Santos",
                    DataNascimento = new DateTime(1990, 5, 15),
                    Sexo = "Masculino",
                    CPF = "123.456.789-00",
                    EnderecoLogradouro = "Rua das Flores, 123",
                    Cidade = "São Paulo",
                    Telefone = "(11) 99999-9999",
                    Ativo = true,
                    DataCriacao = DateTime.UtcNow
                },
                new Paciente
                {
                    Id = Guid.NewGuid(),
                    NomeCompleto = "Maria Oliveira Costa",
                    DataNascimento = new DateTime(1985, 8, 22),
                    Sexo = "Feminino",
                    CPF = "987.654.321-00",
                    EnderecoLogradouro = "Av. Paulista, 456",
                    Cidade = "São Paulo",
                    Telefone = "(11) 88888-8888",
                    Ativo = true,
                    DataCriacao = DateTime.UtcNow
                },
                new Paciente
                {
                    Id = Guid.NewGuid(),
                    NomeCompleto = "Pedro Alves Junior",
                    DataNascimento = new DateTime(2000, 12, 10),
                    Sexo = "Masculino",
                    CPF = "456.789.123-00",
                    EnderecoLogradouro = "Rua do Comércio, 789",
                    Cidade = "São Paulo",
                    Telefone = "(11) 77777-7777",
                    Ativo = true,
                    DataCriacao = DateTime.UtcNow
                }
            };

            context.Pacientes.AddRange(pacientes);
            await context.SaveChangesAsync();

            // 5. Criar Prontuários
            var prontuarios = new[]
            {
                new Prontuario
                {
                    PacienteId = pacientes[0].Id,
                    NumeroProntuario = "2024-001",
                    DataPrimeiraConsulta = DateTime.UtcNow.AddDays(-30),
                    SituacaoProntuario = SituacaoProntuario.Ativo,
                    Ativo = true,
                    DataCriacao = DateTime.UtcNow
                },
                new Prontuario
                {
                    PacienteId = pacientes[1].Id,
                    NumeroProntuario = "2024-002",
                    DataPrimeiraConsulta = DateTime.UtcNow.AddDays(-15),
                    SituacaoProntuario = SituacaoProntuario.Ativo,
                    Ativo = true,
                    DataCriacao = DateTime.UtcNow
                },
                new Prontuario
                {
                    PacienteId = pacientes[2].Id,
                    NumeroProntuario = "2024-003",
                    DataPrimeiraConsulta = DateTime.UtcNow.AddDays(-7),
                    SituacaoProntuario = SituacaoProntuario.Ativo,
                    Ativo = true,
                    DataCriacao = DateTime.UtcNow
                }
            };

            context.Prontuarios.AddRange(prontuarios);
            await context.SaveChangesAsync();

            // 6. Criar Vínculos Aluno-Paciente
            var vinculos = new[]
            {
                new VinculoAlunoPaciente
                {
                    AlunoId = aluno!.Id,
                    PacienteId = pacientes[0].Id,
                    StatusVinculo = StatusVinculo.Ativo,
                    PermiteLeitura = true,
                    PermiteEscrita = true,
                    LiberadoPorUsuarioId = prof!.Id,
                    DataLiberacao = DateTime.UtcNow
                },
                new VinculoAlunoPaciente
                {
                    AlunoId = aluno!.Id,
                    PacienteId = pacientes[1].Id,
                    StatusVinculo = StatusVinculo.Ativo,
                    PermiteLeitura = true,
                    PermiteEscrita = false,
                    LiberadoPorUsuarioId = prof!.Id,
                    DataLiberacao = DateTime.UtcNow
                }
            };

            context.VinculosAlunoPaciente.AddRange(vinculos);
            await context.SaveChangesAsync();

            // 7. Criar Atendimentos de Teste
            var atendimentos = new[]
            {
                new Atendimento
                {
                    PacienteId = pacientes[0].Id,
                    ProntuarioId = prontuarios[0].Id,
                    TipoAtendimento = TipoAtendimento.SessaoIndividual,
                    DataHoraInicio = DateTime.UtcNow.AddDays(-5),
                    DataHoraFim = DateTime.UtcNow.AddDays(-5).AddHours(1),
                    StatusAtendimento = StatusAtendimento.Realizado,
                    Observacoes = "Primeira sessão - paciente apresentou ansiedade social",
                    AlunoId = aluno.Id,
                    Ativo = true,
                    DataCriacao = DateTime.UtcNow
                },
                new Atendimento
                {
                    PacienteId = pacientes[1].Id,
                    ProntuarioId = prontuarios[1].Id,
                    TipoAtendimento = TipoAtendimento.Triagem,
                    DataHoraInicio = DateTime.UtcNow.AddDays(1),
                    StatusAtendimento = StatusAtendimento.Agendado,
                    Observacoes = "Avaliação inicial",
                    AlunoId = aluno.Id,
                    Ativo = true,
                    DataCriacao = DateTime.UtcNow
                }
            };

            context.Atendimentos.AddRange(atendimentos);
            await context.SaveChangesAsync();

            // 8. Criar Evoluções de Teste
            var evolucoes = new[]
            {
                new EvolucaoAtendimento
                {
                    AtendimentoId = atendimentos[0].Id,
                    TextoEvolucao = "Paciente relatou dificuldades em interações sociais. Trabalhou-se técnicas de respiração para controle da ansiedade.",
                    DataEvolucao = DateTime.UtcNow.AddDays(-5),
                    CriadoPorUsuarioId = aluno.Id,
                    Ativo = true,
                    DataCriacao = DateTime.UtcNow
                }
            };

            context.EvolucoesAtendimento.AddRange(evolucoes);
            await context.SaveChangesAsync();
        }
    }
}