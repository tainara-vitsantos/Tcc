# 📋 Guia de Implementação - ConnectaMente v2.0

## ✅ O que foi implementado nesta fase

### 1. **Segurança Acadêmica com Roles (CONCLUÍDO)**
- ✅ Identity configurado com dois Roles: **Professor** e **Aluno**
- ✅ Serviço `RoleInitializationService` : inicializa roles na aplicação
- ✅ Serviço `AuthorizationService`: valida acessos via VinculoAlunoPaciente
- ✅ Controllers com `[Authorize]` e lógica de permissão implementada
- ✅ Alunos veem apenas pacientes vinculados; Professores veem todos

**Controllers atualizados:**
- `PacienteController`: Listagem/detalhes/CRUD mit autorização
- `ProntuarioController`: CRUD com auditoria automática
- `AtendimentoController`: Criação com `AlunoId` preenchido automaticamente
- `HomeController`: Dashboard diferenciado por Role

---

### 2. **Trilha de Auditoria (CONCLUÍDO)**
- ✅ Serviço `AuditService`: log automático de operações
- ✅ Table `Auditoria` captura: **Quem**, **Quando**, **O quê**
- ✅ Registra: IP, UserAgent, valores antes/depois (JSON)
- ✅ Integrado em Controllers para Visualização, Criação, Edição de documentos

**Exemplo de uso:**
```csharp
await _auditService.LogVisualizacaoProntuarioAsync(usuarioId, prontuarioId, pacienteId);
await _auditService.LogCriacaoDocumentoAsync(usuarioId, documentoId, pacienteId, prontuarioId, tipo);
```

---

### 3. **Soft Delete (Exclusão Lógica) - CONCLUÍDO**
- ✅ Serviço `SoftDeleteService`: marca registros como inativos
- ✅ EntityBase com campo `Ativo` implementado
- ✅ Preserva histórico acadêmico sem deletar do BD
- ✅ Método `FilterActive()` para filtrar ativos em queries

**Exemplo de uso:**
```csharp
await _softDeleteService.SoftDeleteDocumentoAsync(documentoId, usuarioId, "Motivo");
```

---

### 4. **ViewModels de Anamnes (CONCLUÍDO)**
- ✅ `AnamneseAdultoViewModel`: 15 seções estruturadas com Data Annotations
- ✅ `AnamneseAdolescenteViewModel`: Seções personalizadas para adolescentes
- ✅ Encapsula lógica de apresentação sem poluir Models
- ✅ Validações de Required, StringLength implementadas

---

### 5. **Dashboard Diferenciado (CONCLUÍDO)**
- ✅ **Dashboard Professor**: 
  - Estatísticas globais (total pacientes, prontuários, atendimentos)
  - Próximos 10 atendimentos globais
  - Log de auditoria recente (últimas 20 ações)
  
- ✅ **Dashboard Aluno**:
  - Estatísticas pessoais (meus pacientes, meus atendimentos)
  - Agenda pessoal (próximos atendimentos)
  - Pacientes atendidos recentemente

---

## 🚀 Próximos Passos (Para o usuário implementar)

### 1. **Criar Views para Anamness**
Localização: `Views/Atendimento/` ou `Views/Documento/`

Exemplo de estrutura (usando a ViewModel):
```html
@model AnamneseAdultoViewModel

<div class="form-section">
    <h3>Queixa Clínica</h3>
    <div class="form-group">
        @Html.LabelFor(m => m.QueixaPrincipal)
        @Html.TextAreaFor(m => m.QueixaPrincipal, new { rows = 3 })
        @Html.ValidationMessageFor(m => m.QueixaPrincipal)
    </div>
</div>
```

### 2. **Implementar Upload de Anexos**
Model `Anexo` já existe. Controller padrão:
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Upload(int documentoId, IFormFile arquivo)
{
    var anexo = new Anexo { 
        DocumentoClinicoId = documentoId,
        NomeArquivo = arquivo.FileName,
        CaminhoArquivo = "uploads/" + Guid.NewGuid() + Path.GetExtension(arquivo.FileName),
        EnviadoPorUsuarioId = usuarioId
    };
    
    // Salvar arquivo físico
    using (var stream = new FileStream(anexo.CaminhoArquivo, FileMode.Create))
    {
        await arquivo.CopyToAsync(stream);
    }
    
    _context.Anexos.Add(anexo);
    await _context.SaveChangesAsync();
}
```

### 3. **Executar Migrations**
```bash
dotnet ef database update --startup-project ClinicaEscolaBase
```

### 4. **Ajustar Views Razor**
- Adicionar rol badges no navbar (Professor/Aluno)
- Mostrar dashboard condicionalmente
- Adicionar botões de ação baseados em Roles

### 5. **Criar seeding de dados de teste**
```csharp
// Program.cs - após app.Build()
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    
    var professor = new ApplicationUser { 
        UserName = "prof@example.com",
        Email = "prof@example.com",
        NomeCompleto = "Professora Silva",
        TipoUsuario = TipoUsuario.ProfessoraAdministradora
    };
    
    await userManager.CreateAsync(professor, "Senha@123");
    await userManager.AddToRoleAsync(professor, "Professor");
}
```

---

## 🔐 Segurança Implementada

| Aspecto | Implementado | Como |
|---------|--------------|------|
| **Autenticação** | ✅ | ASP.NET Core Identity |
| **Autorização por Role** | ✅ | `[Authorize(Roles = "...")] ` |
| **Acesso a Pacientes** | ✅ | `AuthorizationService.CanRead/WritePaciente()` |
| **Auditoria** | ✅ | `AuditService` com todos os logs |
| **Soft Delete** | ✅ | `SoftDeleteService.SoftDelete<T>()` |
| **CriadoPor** | ✅ | `EvolucaoAtendimento.CriadoPorUsuarioId` (agora obrigatório) |
| **Validação de Vínculo** | ✅ | `VinculoAlunoPaciente` checado em Controllers |

---

## 📊 Fluxo de Dados

```
Usuário (Professor/Aluno)
    ↓
[Authorize] -> valida login
    ↓
AuthorizationService -> valida acesso ao Paciente
    ↓
Controller -> executa operação
    ↓
AuditService -> registra ação
    ↓
SoftDeleteService -> marca inativo (se delete)
    ↓
Banco de Dados
```

---

## 🛠️ Troubleshooting

### Erro: "Migration pending"
```bash
dotnet ef database update
```

### Erro: "No roles found"
Verifique se `RoleInitializationService` foi registrado em `Program.cs` e chamado.

### Erro: "Unauthorized" nos Controllers
Garanta que [Authorize] está no Controller e que o usuário tem um Role atribuído.

---

## 📁 Estrutura Final de Serviços

```
Services/
├── RoleInitializationService.cs    → Inicializa Roles na app
├── AuthorizationService.cs          → Valida acessos por VinculoAlunoPaciente
├── AuditService.cs                  → Log automático de operações
└── SoftDeleteService.cs             → Exclusão lógica (soft delete)
```

---

## 💾 Migração Necessária

Novo campo em Usuario para rastrear criação de evoluções:
```bash
dotnet ef migrations add "AddUserToEvolucaoAtendimento"
dotnet ef database update
```

---

**Última atualização:** 8 de Maio de 2026  
**Status:** Fase 2 Completa ✅
