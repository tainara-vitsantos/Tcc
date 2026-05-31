# Diagrama ER das models

`Endereco` nao entra como entidade porque e um owned type; no banco ele vira colunas dentro de `Pacientes` e `InfosFamiliares`. `DocumentoIdentificacaoPaciente` esta comentado no codigo e nao faz parte do schema atual.

```mermaid
erDiagram
    direction LR

    AspNetUsers {
        string Id PK
        string NomeCompleto
        string Cpf "nullable"
        string Matricula "nullable"
        string Crp "nullable"
        int TipoUsuario
        bool Ativo
    }

    Pacientes {
        int Id PK
        string NomeCompleto
        int FamiliarResponsavelId FK
        DateTime DataNascimento "nullable"
        string EnderecoLogradouro "nullable"
        string EnderecoNumero "nullable"
        string EnderecoBairro "nullable"
        string EnderecoCidade "nullable"
        string EnderecoEstado "nullable"
        string EnderecoCep "nullable"
    }

    InfosFamiliares {
        int Id PK
        int PacienteId FK
        string NomeCompleto "nullable"
        bool ResponsavelPrincipal
        string EnderecoLogradouro "nullable"
        string EnderecoNumero "nullable"
        string EnderecoBairro "nullable"
        string EnderecoCidade "nullable"
        string EnderecoEstado "nullable"
        string EnderecoCep "nullable"
    }

    Prontuarios {
        int Id PK
        int PacienteId FK
        string NumeroProntuario
        DateTime DataPrimeiraConsulta "nullable"
        int SituacaoProntuario
    }

    TratamentosAnterioresPaciente {
        int Id PK
        int PacienteId FK
        int ProntuarioModelId FK "nullable"
        int TipoTratamento
        bool Internacao
        string MotivoInternacao "nullable"
        string Observacoes "nullable"
    }

    Atendimentos {
        int Id PK
        int ProntuarioId FK
        int PacienteId FK
        int TipoAtendimento
        DateTime DataHoraInicio
        DateTime DataHoraFim "nullable"
        int StatusAtendimento
        bool FaltaJustificada
        string Observacoes "nullable"
    }

    DocumentosClinicos {
        int Id PK
        int ProntuarioId FK
        int PacienteId FK
        int AtendimentoId FK "nullable"
        string CriadoPorUsuarioId FK
        int TipoDocumentoClinico
        int StatusDocumento
        int Versao
        DateTime DataDocumento
        DateTime FinalizadoEm "nullable"
        string Observacoes "nullable"
        bool ExcluidoLogicamente
    }

    AnamnesesAdulto {
        int DocumentoClinicoId PK, FK
    }

    AnamnesesAdolescente {
        int DocumentoClinicoId PK, FK
        int ResponsavelPrincipalId FK
    }

    TermosAutorizacaoMenor {
        int DocumentoClinicoId PK, FK
        int InfoFamiliarId FK
    }

    TermosPsicoterapiaIndividual {
        int DocumentoClinicoId PK, FK
    }

    EvolucoesAtendimento {
        int Id PK
        int DocumentoClinicoId FK
        int AtendimentoId FK "nullable"
        string CriadoPorUsuarioId FK
        DateTime DataEvolucao
        string TextoEvolucao
    }

    Anexos {
        int Id PK
        int IdDocumentoClinico FK
        string EnviadoPorUsuarioId FK
        DateTime DataUpload
        string NomeOriginal
        string NomeArmazenado
        string CaminhoArquivo
    }

    Auditorias {
        int Id PK
        string UsuarioId FK
        int PacienteId FK "nullable"
        int ProntuarioId FK "nullable"
        DateTime DataHora
        string Entidade
        string RegistroId
    }

    InfosFamiliares ||--o{ Pacientes : responsavel
    Pacientes ||--o{ InfosFamiliares : familiares

    Pacientes ||--|| Prontuarios : possui
    Pacientes ||--o{ TratamentosAnterioresPaciente : historico
    Prontuarios ||--o{ TratamentosAnterioresPaciente : historico

    Prontuarios ||--o{ Atendimentos : possui
    Pacientes ||--o{ Atendimentos : participa

    Prontuarios ||--o{ DocumentosClinicos : possui
    Pacientes ||--o{ DocumentosClinicos : possui
    Atendimentos ||--o{ DocumentosClinicos : vincula
    AspNetUsers ||--o{ DocumentosClinicos : cria

    DocumentosClinicos ||--o| AnamnesesAdulto : detalha
    DocumentosClinicos ||--o| AnamnesesAdolescente : detalha
    DocumentosClinicos ||--o| TermosAutorizacaoMenor : detalha
    DocumentosClinicos ||--o| TermosPsicoterapiaIndividual : detalha

    InfosFamiliares ||--o{ AnamnesesAdolescente : responsavel
    InfosFamiliares ||--o{ TermosAutorizacaoMenor : assina

    DocumentosClinicos ||--o{ EvolucoesAtendimento : baseia
    Atendimentos ||--o{ EvolucoesAtendimento : registra
    AspNetUsers ||--o{ EvolucoesAtendimento : cria

    DocumentosClinicos ||--o{ Anexos : possui
    AspNetUsers ||--o{ Anexos : envia

    AspNetUsers ||--o{ Auditorias : registra
    Pacientes ||--o{ Auditorias : audita
    Prontuarios ||--o{ Auditorias : audita
```