# 🏥 PlanosSaude.API

API REST desenvolvida em ASP.NET Core para gerenciamento de Planos de Saúde.

O projeto contempla operações CRUD, aplicação de regras de negócio, validações e persistência em banco relacional utilizando Entity Framework Core com PostgreSQL.

A solução foi estruturada com foco em organização, separação de responsabilidades e boas práticas do ecossistema .NET.

---

# 🚀 Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL 17
- Docker
- Docker Compose
- Swagger / OpenAPI
- xUnit (Testes unitários)

---

# 📦 Arquitetura e Organização

O projeto segue uma organização em camadas, inspirada nos princípios de Clean Architecture:

- **Controllers** → Camada de entrada HTTP
- **Data** → Contexto e persistência (EF Core)
- **DTOs** → Contratos de entrada e saída
- **Errors** → Contendo exceções customizadas
- **Extensions** → Métodos de extensão.
- **Mapping** → Conversões de Model para DTO
- **Middlewares** → Para tratamento global exceções
- **Migrations** → Arquivos de migração
- **Models** → Classes que representam do domínio do negócio
- **Services** → Regras de negócio
- **Validators** → Classes de validação 
- **Tests** → Testes unitários

Essa divisão reduz acoplamento, facilita manutenção e melhora a testabilidade.

---

# ▶️ Como Rodar o Projeto

## 🔹 Pré-requisitos

- Docker
- Docker Compose

---

## 🔹 1. Clonar o repositório

```bash
git clone https://github.com/seu-usuario/desafio-planos-de-saude-api.git
cd desafio-planos-de-saude-api
```

## 🔹 2. Levantar os contâiners

```
docker compose up --build
```

## 🔹 3. Serviços disponíveis

### API
```bash
http://localhost:5229
```

### Swagger
```bash
http://localhost:5229/swagger/index.html
```
### PostgreSQL (PgAdmin4 ou qualquer outra ferramenta)

```bash
Host: localhost
Porta: 5432
Database: planos_saude
User: desenvolvedor
Password: DotNet@2026
```

## 🔎 Exemplos de Requisições

**Planos**

- Criar plano

  - Método: `POST /api/planos`
  - Payload:

  ```json
  {
    "nome": "Plano Ouro",
    "codigo": "OURO-001",
    "custoMensal": 199.90,
    "cobertura": 2
  }
  ```

  - Resposta: `201 Created` (Location apontando para `GET /api/planos/{id}`)
  - Exemplo de corpo de resposta:

  ```json
  {
    "id": "F2E3F8E9-6FFF-4BA1-B261-2DB63279E699",
    "nome": "Plano Ouro",
    "codigo": "OURO-001",
    "custoMensal": 199.90,
    "cobertura": 2
  }
  ```

- Obter por id

  - Método: `GET /api/planos/{id}`
  - Resposta: `200 OK`

- Listar planos

  - Método: `GET /api/planos`
  - Resposta: `200 OK` com array de planos

- Atualizar plano

  - Método: `PUT /api/planos/{id}`
  - Payload: mesmo formato de `POST /api/planos`
  - Resposta: `204 No Content`

- Remover plano

  - Método: `DELETE /api/planos/{id}`
  - Resposta: `204 No Content`

---

**Beneficiários**

- Criar beneficiário

  - Método: `POST /api/beneficiarios`
  - Payload:

  ```json
  {
    "nome": "João da Silva",
    "cpf": "12345678909",
    "dataNascimento": "1985-04-10",
    "email": "joao@example.com",
    "telefone": "98724527498",
    "ativo": true
  }
  ```

  - Resposta: `201 Created`
  - Exemplo de corpo de resposta:

  ```json
  {
    "id": "B5A1CE17-06EE-4BDA-BFC9-5136090244A9",
    "nome": "João da Silva",
    "cpf": "12345678909",
    "dataNascimento": "1985-04-10",
    "email": "joao@example.com",
    "telefone": "98134624558",
    "isAtivo": true
  }
  ```

- Obter todos

  - Método: `GET /api/beneficiarios`
  - Resposta: `200 OK` com array de beneficiários

- Obter por id

  - Método: `GET /api/beneficiarios/{id}`
  - Resposta: `200 OK`
 

- Atualizar beneficiário

  - Método: `PUT /api/beneficiarios/{id}`
  - Payload:

  ```json
  {
    "nome": "João da Silva",
    "email": "joao.novo@example.com",
    "telefone": "8138313929",
    "isAtivo": true
  }
  ```

  - Resposta: `204 No Content`

- Remover beneficiário

  - Método: `DELETE /api/beneficiarios/{id}`
  - Resposta: `204 No Content`

---

**Contratações**

- Listar todas

  - Método: `GET /api/contratacoes`
  - Resposta: `200 OK` com array de contratações

- Obter por id

  - Método: `GET /api/contratacoes/{id}`
  - Resposta: `200 OK`

- Contratar (criar)

  - Método: `POST /api/contratacoes`
  - Payload:

  ```json
  {
    "beneficiarioId": "B5A1CE17-06EE-4BDA-BFC9-5136090244A9",
    "planoId": "F2E3F8E9-6FFF-4BA1-B261-2DB63279E699",
    "dataInicio": "2026-03-01T00:00:00"
  }
  ```

  - Resposta: `201 Created` com o objeto da contratação:

  ```json
  {
    "id": "A90B6010-8C1B-4F41-B10B-00BBA1AAF38D",
    "beneficiarioId": "B5A1CE17-06EE-4BDA-BFC9-5136090244A9",
    "beneficiarioNome": "João da Silva",
    "planoId": "F2E3F8E9-6FFF-4BA1-B261-2DB63279E699",
    "planoNome": "Plano Ouro",
    "dataInicio": "2026-03-01T00:00:00+00:00",
    "dataFim": null,
    "ativa": true
  }
  ```

- Cancelar contratação

  - Método: `PATCH /api/contratacoes/{id}/cancelar`
  - Resposta: `204 No Content`

---

## 🧠 Decisões Técnicas

### 1️⃣ Banco de Dados

Foi utilizado **PostgreSQL 17** por sua confiabilidade, robustez, conformidade com ACID e ampla adoção no mercado corporativo.  
A integração foi realizada através do provider `Npgsql` com Entity Framework Core.

---

### 2️⃣ Dockerização

Foi utilizado Docker com multi-stage build, permitindo:

- Redução do tamanho da imagem final

- Separação entre ambiente de build e runtime

- Melhor eficiência

- Ambiente isolado e reprodutível

- A aplicação roda juntamente com o PostgreSQL via docker-compose.


3️⃣ Tratamento de Erros

- Implementado tratamento centralizado de exceções visando:

- Padronização das respostas HTTP.

- Evitar exposição de detalhes internos da aplicação

- Melhorar previsibilidade da API

- Retornar mensagens claras ao consumidor

- A API respeita boas práticas REST quanto a códigos de status.


4️⃣ Performance

- Aplicadas boas práticas de performance:

- Uso consistente de async/await

- Projeções com .Select() para evitar overfetching
  evitando carregamento desnecessário de entidades completas

- Uso da Interface IReadOnlyCollection<T> ao invés de IEnumerable<T>, nos endpoints 
  GET (quando pertinente) para garantir que os dados sejam carregados na memória de forma mais eficiente.
   
- Estrutura preparada para indexação de consultas frequentes

- Organização arquitetural favorecendo escalabilidade


🧪 Testes

O projeto contém testes unitários utilizando xUnit, focando em:

- Regras de negócio

- Validações

- Comportamento de serviços

Execução:

```bash
dotnet test
```

⏳ O que faria se tivesse mais tempo

Melhorias futuras previstas:

- Autenticação e autorização com JWT

- Paginação e filtros dinâmicos

- Cache com Redis para consultas frequentes

- Versionamento de API

- Documentação de API utilizando Swagger

- Testes de integração com WebApplicationFactory

- Pipeline CI/CD

- Logs estruturados e observabilidade (Serilog + OpenTelemetry)
