# Base inicial - Clínica Escola de Psicologia

Pacote com Models, ViewModels, Enums, DbContext e Fluent API para um projeto ASP.NET Core MVC com EF Core + Identity.

## Estrutura
- `Models`: classes persistidas e mapeadas no banco
- `ViewModels`: classes voltadas às telas e formulários MVC
- `Enums`: enums usados nas regras e no banco
- `Configurations`: mapeamentos Fluent API
- `Data/ApplicationDbContext.cs`: contexto principal

## Premissas
- Usuários usam ASP.NET Identity.
- Professora administradora tem acesso total.
- Alunos só acessam pacientes vinculados de forma ativa.
- Documentos clínicos possuem uma tabela base e tabelas especializadas.
- Toda ação relevante deve ser auditada.

## Próximo passo
1. Copiar os arquivos para a solução.
2. Registrar o `ApplicationDbContext`.
3. Criar a migration inicial.
4. Implementar autorização por perfil e vínculo ativo.
5. Criar controllers e views para cadastro do paciente, prontuário, vínculo e documentos clínicos.

## Como rodar localmente
1. Abra o terminal na pasta `ClinicaEscolaBase`.
2. Execute `dotnet restore`.
3. Execute `dotnet ef database update` para criar o banco de dados.
4. Execute `dotnet run`.
5. Acesse `https://localhost:5001` ou `http://localhost:5000`.

## Contas de teste
- Professor: `  ` / `Senha@123`
- Aluno: `aluno@fatec.com` /Senha@123` `

## Observações finais
- A aplicação usa ASP.NET Identity, EF Core e SQL Server LocalDB.
- As permissões são controladas por perfil e vínculo ativo entre aluno e paciente.
- O sistema registra auditoria de visualizações, inserções, atualizações e exclusões.
