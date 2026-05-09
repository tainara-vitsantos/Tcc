# 🧪 GUIA DE TESTES - ConnectaMente v2.0

## ⚡ TESTES RÁPIDOS (5 minutos)

### 1. Validar Compilação
```bash
cd c:\Users\etec\Desktop\Tcc\ClinicaEscolaBase
dotnet build
```
✅ Esperado: `Compilação com êxito. 0 Aviso(s), 0 Erro(s)`

---

### 2. Rodar Aplicação
```bash
dotnet run
```
✅ Esperado: `Now listening on: https://localhost:7xxx`

---

### 3. Testar Página Inicial (Sem Login)
URL: `https://localhost:7xxx/`
✅ Esperado: Dashboard público (sem dados sensíveis)

---

### 4. Testar Redirect de Autorização
URL: `https://localhost:7xxx/Paciente/Index` (sem logar)
✅ Esperado: Redireciona para `/Identity/Account/Login`

---

## 🔐 TESTES DE SEGURANÇA (10 minutos)

### Pré-requisito: Seeding de Usuários

Adicione em `Data/DbSeeding.cs`:
```csharp
using Microsoft.AspNetCore.Identity;

public static class DbSeeding
{
    public static async Task InitializeAsync(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Criar roles
            string[] roles = { "Professor", "Aluno" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Criar usuários
            var professor = new ApplicationUser
            {
                UserName = "professor@clinic.edu",
                Email = "professor@clinic.edu",
                NomeCompleto = "Professora Ana Silva",
                EmailConfirmed = true
            };

            if (await userManager.FindByNameAsync(professor.UserName) == null)
            {
                await userManager.CreateAsync(professor, "Senha@123");
                await userManager.AddToRoleAsync(professor, "Professor");
            }

            var aluno = new ApplicationUser
            {
                UserName = "aluno@clinic.edu",
                Email = "aluno@clinic.edu",
                NomeCompleto = "Aluno João Santos",
                EmailConfirmed = true
            };

            if (await userManager.FindByNameAsync(aluno.UserName) == null)
            {
                await userManager.CreateAsync(aluno, "Senha@123");
                await userManager.AddToRoleAsync(aluno, "Aluno");
            }
        }
    }
}
```

Chame em `Program.cs` após `app.Build()`:
```csharp
await DbSeeding.InitializeAsync(app);
```

---

### Teste 1: Login como Professor
1. Acesse `/Identity/Account/Login`
2. Email: `professor@clinic.edu`
3. Senha: `Senha@123`
4. ✅ Esperado: Dashboard com estatísticas globais

---

### Teste 2: Login como Aluno
1. Acesse `/Identity/Account/Login`
2. Email: `aluno@clinic.edu`
3. Senha: `Senha@123`
4. ✅ Esperado: Dashboard com dados pessoais (0 pacientes inicialmente)

---

### Teste 3: Testar Restrição de Acesso (Aluno)
**Cenário**: Aluno tenta acessar `/Paciente/Index` sem vínculo

1. Logar como Aluno
2. Acessar `https://localhost:7xxx/Paciente/Index`
3. ✅ Esperado: Lista vazia (nenhum paciente vinculado)

---

### Teste 4: Testar Acesso Restrito (Aluno)
**Cenário**: Aluno tenta editar paciente sem permissão de escrita

1. Logar como Professor
2. Criar um Paciente (ex: "João Silva")
3. Logar como Aluno
4. Tentar acessar `/Paciente/Edit/{id}`
5. ✅ Esperado: Página de erro 403 (Forbid)

---

### Teste 5: Criar Vínculo e Testar Acesso
**Cenário**: Após vínculo, aluno pode ver paciente

1. Logar como Professor
2. Acessar `Paciente/Details/{id}`
3. (Criar VinculoAlunoPaciente com status Ativo)
4. Logar como Aluno
5. Acessar `Paciente/Index`
6. ✅ Esperado: Paciente aparece na lista

---

## 📊 TESTES DE AUDITORIA (5 minutos)

### Teste 6: Registrar Visualização
1. Logar como Professor
2. Acessar `Prontuario/Details/{id}`
3. Verificar tabela `Auditorias` no banco
   ```sql
   SELECT * FROM Auditorias 
   WHERE TipoAcao = 6 
   ORDER BY DataHora DESC
   ```
4. ✅ Esperado: Novo registro com `Visualizacao`

---

### Teste 7: Registrar Criação
1. Logar como Professor
2. Criar novo Prontuário
3. Verificar tabela `Auditorias`
   ```sql
   SELECT * FROM Auditorias 
   WHERE TipoAcao = 3 
   ORDER BY DataHora DESC
   ```
4. ✅ Esperado: Novo registro com `Insercao`

---

### Teste 8: Verificar Valores JSON
1. Logar como Professor
2. Editar um Prontuário
3. Verificar `ValoresAntesJson` e `ValoresDepoisJson`
   ```sql
   SELECT UsuarioId, TipoAcao, ValoresAntesJson, ValoresDepoisJson
   FROM Auditorias 
   WHERE Entidade = 'Prontuario'
   ORDER BY DataHora DESC
   OFFSET 0 ROWS FETCH NEXT 1 ROW ONLY
   ```
4. ✅ Esperado: JSON com diferenças entre versões

---

## 🎯 TESTES DO DASHBOARD (5 minutos)

### Teste 9: Dashboard do Professor
1. Logar como Professor
2. Acessar HomePage (`/`)
3. ✅ Esperado: Ver cards com:
   - Total de Pacientes
   - Prontuários Ativos
   - Atendimentos Hoje
   - Próximos Atendimentos (global)
   - **Auditoria Recente** (feature exclusiva)

---

### Teste 10: Dashboard do Aluno
1. Logar como Aluno
2. Acessar HomePage (`/`)
3. ✅ Esperado: Ver cards com:
   - Meus Pacientes: 0 (ou quantidade vinculada)
   - Meus Atendimentos Agendados
   - Meus Atendimentos Realizados
   - Meus Atendimentos Hoje
   - Pacientes Atendidos Recentemente (carrossel)

---

## 🔍 TESTE DE SOFT DELETE (3 minutos)

### Teste 11: Marcar Documento como Inativo
1. Logar como Professor
2. Criar DocumentoClinico
3. Chamar `SoftDeleteService.SoftDeleteDocumentoAsync()`
4. Verificar campo `Ativo = false` no documento
5. ✅ Esperado: Documento não aparece em listagens, mas existe no BD

---

## 📋 TESTE FINAL DE INTEGRAÇÃO (10 minutos)

### Cenário Completo: Fluxo de Atendimento

```
1. Professor cria Paciente
   ✅ Auditoria registra criação
   
2. Professor cria Prontuario para Paciente
   ✅ Auditoria registra criação
   ✅ Validação de unicidade funciona
   
3. Professor cria Vínculo: Professor → Aluno+Paciente
   ✅ Status = Ativo
   ✅ PermiteLeitura = true
   ✅ PermiteEscrita = true
   
4. Aluno acessa Paciente/Details
   ✅ Autorização valida vínculo
   ✅ Auditoria registra visualização
   
5. Aluno cria Atendimento
   ✅ AlunoId preenchido automaticamente
   ✅ Auditoria registra criação
   
6. Aluno cria Evolução
   ✅ CriadoPorUsuarioId preenchido
   ✅ Auditoria registra adição
   
7. Professor revoga Vínculo
   ✅ Status = Revogado
   ✅ Auditoria registra revogação
   
8. Aluno tenta acessar Paciente
   ✅ Acesso negado (Forbid)
```

**Tempo total**: ~20 minutos

---

## 🐛 TROUBLESHOOTING

### Erro: "No roles found"
**Solução**: Garantir que `RoleInitializationService` foi chamado em `Program.cs`

### Erro: "User does not have permission"
**Solução**: Verificar se usuário está no Role correto com:
```sql
SELECT ur.* FROM AspNetUserRoles ur
WHERE ur.UserId = '{userId}'
```

### Erro: "ForeignKey violation for AlunoId"
**Solução**: Garantir que o usuário logado existe em `AspNetUsers`

### Erro: "JsonException on ValoresJson"
**Solução**: Verificar se objetos passados para Log são serializáveis

---

## ✅ CHECKLIST DE VALIDAÇÃO

- [ ] Compilação sem erros
- [ ] Aplicação roda sem crashes
- [ ] Prof consegue logar
- [ ] Aluno consegue logar
- [ ] Aluno vê apenas seus pacientes
- [ ] Aluno não pode editar paciente se não tem permissão
- [ ] Auditoria registra visualizações
- [ ] Auditoria registra criações
- [ ] Dashboard Prof tem stats globais
- [ ] Dashboard Aluno tem stats pessoais
- [ ] Soft delete preserva dados

**Resultado**: ☐ PRONTO PARA PRÓXIMA FASE

