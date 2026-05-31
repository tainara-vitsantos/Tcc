using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaEscolaBase.Migrations
{
    /// <inheritdoc />
    public partial class ValidateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Matricula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Crp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoUsuario = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnamnesesAdolescente",
                columns: table => new
                {
                    DocumentoClinicoId = table.Column<int>(type: "int", nullable: false),
                    Escolaridade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Escola = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueixaPrincipal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesdeQuando = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AtitudeMaeFrenteQueixa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AtitudePaiFrenteQueixa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AtitudeOutrosFamiliares = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoiDesejado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Linguagem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesenvolvimentoPsicomotor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alimentacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tiques = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DificuldadeEscolar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamiliarNervoso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescricaoFamiliarNervoso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamiliarProblemaMental = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViciosFamilia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalEstudo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TiposDiversao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamiliaFazVisitas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamiliaRecebeVisitas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Companheiros = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuemEscolheCompanheiros = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsavelPrincipalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnamnesesAdolescente", x => x.DocumentoClinicoId);
                });

            migrationBuilder.CreateTable(
                name: "AnamnesesAdulto",
                columns: table => new
                {
                    DocumentoClinicoId = table.Column<int>(type: "int", nullable: false),
                    QueixaPrincipal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueixaSecundaria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sintomas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InicioPatologia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrequenciaPatologia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntensidadePatologia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TratamentosAnteriores = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medicamentos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoriaInfancia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rotina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vicios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hobbies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trabalho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoricoFamiliarPais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoricoFamiliarIrmaos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoricoFamiliarConjuge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoricoFamiliarFilhos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoricoFamiliarLar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoriaPatologicaPregressa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExameAparencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExameComportamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Atitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AtitudeEntrevistador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrientacaoAutoIdentificatoria = table.Column<bool>(type: "bit", nullable: false),
                    OrientacaoCorporal = table.Column<bool>(type: "bit", nullable: false),
                    OrientacaoTemporal = table.Column<bool>(type: "bit", nullable: false),
                    OrientacaoEspacial = table.Column<bool>(type: "bit", nullable: false),
                    OrientacaoPatologia = table.Column<bool>(type: "bit", nullable: false),
                    ObservacoesOrientacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sensopercepcao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AtencaoVigilancia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AtencaoTenacidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Memoria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inteligencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SensopercepcaoNormal = table.Column<bool>(type: "bit", nullable: false),
                    SensopercepcaoAlucinacao = table.Column<bool>(type: "bit", nullable: false),
                    PensamentoAcelerado = table.Column<bool>(type: "bit", nullable: false),
                    PensamentoRetardado = table.Column<bool>(type: "bit", nullable: false),
                    PensamentoFuga = table.Column<bool>(type: "bit", nullable: false),
                    PensamentoBloqueio = table.Column<bool>(type: "bit", nullable: false),
                    PensamentoProlixo = table.Column<bool>(type: "bit", nullable: false),
                    PensamentoRepeticao = table.Column<bool>(type: "bit", nullable: false),
                    PensamentoVelocidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConteudoObsessoes = table.Column<bool>(type: "bit", nullable: false),
                    ConteudoHipocondrias = table.Column<bool>(type: "bit", nullable: false),
                    ConteudoFobias = table.Column<bool>(type: "bit", nullable: false),
                    ConteudoDelirios = table.Column<bool>(type: "bit", nullable: false),
                    ConteudoPensamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TendenciaSuicida = table.Column<bool>(type: "bit", nullable: false),
                    ExpansaoEu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RetracaoEu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NegacaoEu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Afetividade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Humor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConscienciaDoencaAtual = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HipoteseDiagnostica = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnamnesesAdulto", x => x.DocumentoClinicoId);
                });

            migrationBuilder.CreateTable(
                name: "Anexos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeOriginal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeArmazenado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extensao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TamanhoBytes = table.Column<long>(type: "bigint", nullable: false),
                    CaminhoArquivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashArquivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnviadoPorUsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataUpload = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdDocumentoClinico = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anexos_AspNetUsers_EnviadoPorUsuarioId",
                        column: x => x.EnviadoPorUsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Atendimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProntuarioId = table.Column<int>(type: "int", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    TipoAtendimento = table.Column<int>(type: "int", nullable: false),
                    DataHoraInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataHoraFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusAtendimento = table.Column<int>(type: "int", nullable: false),
                    FaltaJustificada = table.Column<bool>(type: "bit", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auditorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TipoAcao = table.Column<int>(type: "int", nullable: false),
                    Entidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistroId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: true),
                    ProntuarioId = table.Column<int>(type: "int", nullable: true),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValoresAntesJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValoresDepoisJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auditorias_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentosClinicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProntuarioId = table.Column<int>(type: "int", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    AtendimentoId = table.Column<int>(type: "int", nullable: true),
                    CriadoPorUsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TipoDocumentoClinico = table.Column<int>(type: "int", nullable: false),
                    StatusDocumento = table.Column<int>(type: "int", nullable: false),
                    Versao = table.Column<int>(type: "int", nullable: false),
                    DataDocumento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinalizadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExcluidoLogicamente = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosClinicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentosClinicos_AspNetUsers_CriadoPorUsuarioId",
                        column: x => x.CriadoPorUsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentosClinicos_Atendimentos_AtendimentoId",
                        column: x => x.AtendimentoId,
                        principalTable: "Atendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvolucoesAtendimento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentoClinicoId = table.Column<int>(type: "int", nullable: false),
                    AtendimentoId = table.Column<int>(type: "int", nullable: true),
                    CriadoPorUsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataEvolucao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TextoEvolucao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvolucoesAtendimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvolucoesAtendimento_AspNetUsers_CriadoPorUsuarioId",
                        column: x => x.CriadoPorUsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvolucoesAtendimento_Atendimentos_AtendimentoId",
                        column: x => x.AtendimentoId,
                        principalTable: "Atendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvolucoesAtendimento_DocumentosClinicos_DocumentoClinicoId",
                        column: x => x.DocumentoClinicoId,
                        principalTable: "DocumentosClinicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TermosPsicoterapiaIndividual",
                columns: table => new
                {
                    DocumentoClinicoId = table.Column<int>(type: "int", nullable: false),
                    NomeClienteNoTermo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RGCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoCivilCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfissaoCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CidadeCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuaCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefoneCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeEstagiarioNoTermo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefoneEstagiario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataAssinatura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataInicioVigencia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataFimVigencia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FrequenciaSemanal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DuracaoSessaoMinutos = table.Column<int>(type: "int", nullable: true),
                    Abordagem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegraCancelamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegraFaltas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermosPsicoterapiaIndividual", x => x.DocumentoClinicoId);
                    table.ForeignKey(
                        name: "FK_TermosPsicoterapiaIndividual_DocumentosClinicos_DocumentoClinicoId",
                        column: x => x.DocumentoClinicoId,
                        principalTable: "DocumentosClinicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfosFamiliares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parentesco = table.Column<int>(type: "int", nullable: true),
                    GrauInstrucao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profissao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoLogradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoNumero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoBairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoCidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoEstado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoCep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CondicaoConjugal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsavelPrincipal = table.Column<bool>(type: "bit", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfosFamiliares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefoneRecado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Naturalidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoNascimento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Escolaridade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profissao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoCivil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Religiao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoLogradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoNumero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoBairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoCidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoEstado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoCep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FamiliarResponsavelId = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pacientes_InfosFamiliares_FamiliarResponsavelId",
                        column: x => x.FamiliarResponsavelId,
                        principalTable: "InfosFamiliares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TermosAutorizacaoMenor",
                columns: table => new
                {
                    DocumentoClinicoId = table.Column<int>(type: "int", nullable: false),
                    InfoFamiliarId = table.Column<int>(type: "int", nullable: false),
                    RGResponsavel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPFResponsavel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeMenorNoTermo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimentoMenorNoTermo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AutorizaAtendimentoPsicologico = table.Column<bool>(type: "bit", nullable: false),
                    AutorizaColetaInformacoes = table.Column<bool>(type: "bit", nullable: false),
                    CienteDevolutivaMensal = table.Column<bool>(type: "bit", nullable: false),
                    DataAssinatura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermosAutorizacaoMenor", x => x.DocumentoClinicoId);
                    table.ForeignKey(
                        name: "FK_TermosAutorizacaoMenor_DocumentosClinicos_DocumentoClinicoId",
                        column: x => x.DocumentoClinicoId,
                        principalTable: "DocumentosClinicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TermosAutorizacaoMenor_InfosFamiliares_InfoFamiliarId",
                        column: x => x.InfoFamiliarId,
                        principalTable: "InfosFamiliares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prontuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    NumeroProntuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPrimeiraConsulta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SituacaoProntuario = table.Column<int>(type: "int", nullable: false),
                    ObservacoesGerais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prontuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prontuarios_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TratamentosAnterioresPaciente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    TipoTratamento = table.Column<int>(type: "int", nullable: false),
                    Internacao = table.Column<bool>(type: "bit", nullable: false),
                    MotivoInternacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProntuarioModelId = table.Column<int>(type: "int", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TratamentosAnterioresPaciente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TratamentosAnterioresPaciente_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TratamentosAnterioresPaciente_Prontuarios_ProntuarioModelId",
                        column: x => x.ProntuarioModelId,
                        principalTable: "Prontuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnamnesesAdolescente_ResponsavelPrincipalId",
                table: "AnamnesesAdolescente",
                column: "ResponsavelPrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexos_EnviadoPorUsuarioId",
                table: "Anexos",
                column: "EnviadoPorUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexos_IdDocumentoClinico",
                table: "Anexos",
                column: "IdDocumentoClinico");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_PacienteId",
                table: "Atendimentos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_ProntuarioId",
                table: "Atendimentos",
                column: "ProntuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Auditorias_PacienteId",
                table: "Auditorias",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Auditorias_ProntuarioId",
                table: "Auditorias",
                column: "ProntuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Auditorias_UsuarioId",
                table: "Auditorias",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosClinicos_AtendimentoId",
                table: "DocumentosClinicos",
                column: "AtendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosClinicos_CriadoPorUsuarioId",
                table: "DocumentosClinicos",
                column: "CriadoPorUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosClinicos_PacienteId",
                table: "DocumentosClinicos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosClinicos_ProntuarioId",
                table: "DocumentosClinicos",
                column: "ProntuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucoesAtendimento_AtendimentoId",
                table: "EvolucoesAtendimento",
                column: "AtendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucoesAtendimento_CriadoPorUsuarioId",
                table: "EvolucoesAtendimento",
                column: "CriadoPorUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EvolucoesAtendimento_DocumentoClinicoId",
                table: "EvolucoesAtendimento",
                column: "DocumentoClinicoId");

            migrationBuilder.CreateIndex(
                name: "IX_InfosFamiliares_PacienteId",
                table: "InfosFamiliares",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_FamiliarResponsavelId",
                table: "Pacientes",
                column: "FamiliarResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Prontuarios_PacienteId",
                table: "Prontuarios",
                column: "PacienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TermosAutorizacaoMenor_InfoFamiliarId",
                table: "TermosAutorizacaoMenor",
                column: "InfoFamiliarId");

            migrationBuilder.CreateIndex(
                name: "IX_TratamentosAnterioresPaciente_PacienteId",
                table: "TratamentosAnterioresPaciente",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_TratamentosAnterioresPaciente_ProntuarioModelId",
                table: "TratamentosAnterioresPaciente",
                column: "ProntuarioModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnamnesesAdolescente_DocumentosClinicos_DocumentoClinicoId",
                table: "AnamnesesAdolescente",
                column: "DocumentoClinicoId",
                principalTable: "DocumentosClinicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnamnesesAdolescente_InfosFamiliares_ResponsavelPrincipalId",
                table: "AnamnesesAdolescente",
                column: "ResponsavelPrincipalId",
                principalTable: "InfosFamiliares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnamnesesAdulto_DocumentosClinicos_DocumentoClinicoId",
                table: "AnamnesesAdulto",
                column: "DocumentoClinicoId",
                principalTable: "DocumentosClinicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Anexos_DocumentosClinicos_IdDocumentoClinico",
                table: "Anexos",
                column: "IdDocumentoClinico",
                principalTable: "DocumentosClinicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimentos_Pacientes_PacienteId",
                table: "Atendimentos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimentos_Prontuarios_ProntuarioId",
                table: "Atendimentos",
                column: "ProntuarioId",
                principalTable: "Prontuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Auditorias_Pacientes_PacienteId",
                table: "Auditorias",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Auditorias_Prontuarios_ProntuarioId",
                table: "Auditorias",
                column: "ProntuarioId",
                principalTable: "Prontuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentosClinicos_Pacientes_PacienteId",
                table: "DocumentosClinicos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentosClinicos_Prontuarios_ProntuarioId",
                table: "DocumentosClinicos",
                column: "ProntuarioId",
                principalTable: "Prontuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InfosFamiliares_Pacientes_PacienteId",
                table: "InfosFamiliares",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_InfosFamiliares_FamiliarResponsavelId",
                table: "Pacientes");

            migrationBuilder.DropTable(
                name: "AnamnesesAdolescente");

            migrationBuilder.DropTable(
                name: "AnamnesesAdulto");

            migrationBuilder.DropTable(
                name: "Anexos");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Auditorias");

            migrationBuilder.DropTable(
                name: "EvolucoesAtendimento");

            migrationBuilder.DropTable(
                name: "TermosAutorizacaoMenor");

            migrationBuilder.DropTable(
                name: "TermosPsicoterapiaIndividual");

            migrationBuilder.DropTable(
                name: "TratamentosAnterioresPaciente");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DocumentosClinicos");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Atendimentos");

            migrationBuilder.DropTable(
                name: "Prontuarios");

            migrationBuilder.DropTable(
                name: "InfosFamiliares");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
