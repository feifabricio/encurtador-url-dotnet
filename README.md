# Encurtador de URL – ASP.NET Core

Projeto de **API REST para encurtamento de URLs**, desenvolvido em **ASP.NET Core (.NET 6)**, seguindo princípios de **Clean Architecture**, com persistência em **SQL Server**.

Este projeto foi construído como um **desafio técnico**, priorizando clareza arquitetural, separação de responsabilidades e código simples, legível e extensível.

---

## Arquitetura

O projeto está organizado em camadas bem definidas:

```
EncurtadorUrl.Api             → Camada de apresentação (API)
EncurtadorUrl.Aplicacao      → Contratos e regras de aplicação
EncurtadorUrl.Dominio        → Entidades e regras de domínio
EncurtadorUrl.Infraestrutura → Persistência, EF Core e implementações
```

### Responsabilidades

* **Api**

  * Configuração do pipeline
  * Injeção de dependência
  * Exposição dos endpoints REST

* **Aplicação**

  * Interfaces (contratos)
  * Não depende de infraestrutura

* **Domínio**

  * Entidades de negócio
  * Regras centrais

* **Infraestrutura**

  * Entity Framework Core
  * SQL Server
  * Implementações de serviços

---

## 🛠 Tecnologias Utilizadas

* .NET 6
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Injeção de Dependência
* Clean Architecture

---

## Configuração do Ambiente

### Pré-requisitos

* .NET SDK 6.0+
* SQL Server (local ou remoto)
* Git

---

## Configuração do Banco de Dados

No projeto **EncurtadorUrl.Api**, configure a connection string no arquivo:

`appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EncurtadorUrl;Trusted_Connection=True;"
  }
}
```

---

## 🗄 Migrations (Entity Framework)

Criar a migration inicial:

```bash
dotnet ef migrations add Inicial \
  --project EncurtadorUrl.Infraestrutura \
  --startup-project EncurtadorUrl.Api
```

Aplicar no banco:

```bash
dotnet ef database update \
  --startup-project EncurtadorUrl.Api
```

---

## Executando a Aplicação

Na raiz do projeto:

```bash
dotnet build
dotnet run --project EncurtadorUrl.Api
```

A API estará disponível em:

```
https://localhost:5001
http://localhost:5000
```

---

## Endpoints Principais

### Criar URL encurtada

**POST** `/api/urls`

```json
{
  "urlOriginal": "https://www.exemplo.com"
}
```

---

### Obter URL original

**GET** `/{codigoCurto}`

Exemplo:

```
GET /aB9xQ1
```

---

## Concorrência

Para evitar colisões na geração de códigos curtos, o serviço utiliza **SemaphoreSlim**, garantindo que apenas uma operação crítica seja executada por vez.

Essa abordagem é simples, eficaz e suficiente para o escopo do desafio.

---

## Decisões de Projeto

* Separação clara entre contrato e implementação
* Aplicação não depende de infraestrutura
* Código em português para facilitar entendimento
* Simplicidade acima de abstrações desnecessárias

---

## Possíveis Evoluções

* Cache (Redis)
* Expiração de URLs
* Autenticação
* Testes unitários e de integração

---

## Autor

Projeto desenvolvido como desafio técnico em **ASP.NET Core**, demonstrando domínio de arquitetura, boas práticas e organização de código.

