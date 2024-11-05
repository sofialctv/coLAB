```mermaid
classDiagram
    class Projeto {
      +String nome
      +Date data_inicio
      +Date data_fim  
      +Date data_prevista_fim
      +String descricao
      +Double orcamento
    }

    class Financiador {
      +String nome
      +String email
    }

    class Pesquisador {
      +String nome
      +String email
      +String telefone
      +String cpf
    }

    class Orientador
    class Bolsista

    class Bolsa {
      +Double valor
      +Date data_inicio
      +Date data_fim
      +Date data_prevista_fim
      +Boolean ativo
    }

    class ProjetoStatus {
      <<enumeration>>
      INATIVO
      ATIVO
      CANCELADO
      FINALIZADO
    }

    class ProjetoCategoria {
      <<enumeration>>
      INOVAÇÃO
      EXTENSÃO
      TECNOLOGIA
    }

    class PesquisadorTime {
      <<enumeration>>
      GESTAO_DE_PROJETOS
      RECURSOS_HUMANOS
      MARKETING
      DESENVOLVIMENTO
      ANALISTA_DE_REQUISITOS
      QUALIDADE
      DESIGN
    }

    Projeto "1" --> "1" Orientador
    Projeto "N" --> "N" Pesquisador
    Projeto "N" --> "N" Bolsista
    Projeto "1" --> "N" Financiador : Possui
    Projeto "1" --> "1" ProjetoCategoria : Contém
    Projeto "1" --> "1" ProjetoStatus : Contém

    Bolsa "1" --> "1" Bolsista : Pertence
    Bolsa "1" --> "N" BolsaCategoria : Contém

    Pesquisador "1" --> "N" PesquisadorTime : Pertence
    Pesquisador <|-- Orientador
    Pesquisador <|-- Bolsista
```

## Dicionário 🔴🔴🔴🔴🔴🔴🔴🔴🔴 FINALIZAR

### Classes:
- **Projeto:** Representa um projeto de um laboratório.
- **Financiador:** Representa o patrocinador do projeto.
- **Pesquisador:** Representa o pesquisador envolvido no projeto, com detalhes como nome, e-mail, etc.
- **Orientador e Bolsista:** Subclasses de Pesquisador, indicando funções específicas.
- **Bolsa:** Representa bolsas dadas aos pesquisadores.

## Relacionamentos:
- Um projeto tem uma associação com um ou mais Financiadores, Pesquisadores e Bolsistas.
- Bolsa é vinculada ao Bolsista e inclui o valor da bolsa e as datas.
- Projeto tem uma associação com as enums ProjetoCategoria e ProjetoStatus.