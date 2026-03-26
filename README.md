# DesafioFastBackend API

API REST em `.NET 10` para gestão de `colaboradores`, `workshops` e `presenças`.

Este README foi preparado para facilitar a integração com frontend (Angular).

---

## 1. Stack e arquitetura

- `.NET 10` (`ASP.NET Core Web API`)
- `Entity Framework Core` + `SQL Server`
- `JWT Bearer` para autenticação
- `Authorization Policies` por perfil (`Admin` e `Reader`)
- `Swagger` para documentação e testes
- `FluentValidation` para validações de entrada
- Middleware global para tratamento de exceções

Organização por camadas:
- `Domain`: entidades, enums, contratos e exceptions de domínio
- `Application`: use cases, DTOs e validators
- `Infrastructure`: EF Core, repositórios e serviço de token
- `API`: controllers, middleware, autenticação, CORS e Swagger

---

## 2. Endpoints principais

### Auth
- `POST /api/auth/login`

### Colaboradores
- `GET /api/colaboradores`
- `GET /api/colaboradores/{id}`
- `POST /api/colaboradores`
- `PUT /api/colaboradores/{id}`
- `DELETE /api/colaboradores/{id}`

### Workshops
- `GET /api/workshops`
- `GET /api/workshops/{id}`
- `POST /api/workshops`
- `PUT /api/workshops/{id}`
- `DELETE /api/workshops/{id}`

### Presenças
- `GET /api/presencas`
- `GET /api/presencas/{workshopId}/{colaboradorId}`
- `POST /api/presencas`
- `PUT /api/presencas/{workshopId}/{colaboradorId}`
- `DELETE /api/presencas/{workshopId}/{colaboradorId}`

---

## 3. Segurança (Auth + Authorization)

### Login
`POST /api/auth/login`

Body:
```json
{
  "username": "admin",
  "password": "admin123"
}
```

Retorno (`200`):
```json
{
  "success": true,
  "message": "Autenticação realizada com sucesso.",
  "data": {
    "accessToken": "<jwt>",
    "tokenType": "Bearer",
    "username": "admin",
    "role": "Admin"
  }
}
```

### Policies
- `ReadAccess`: `Admin` ou `Reader`
- `WriteAccess`: `Admin`

Aplicação:
- `GET`: `ReadAccess`
- `POST/PUT/DELETE`: `WriteAccess`

Configuração atual do token:
- Expiração: `180` minutos (`3` horas)

---

## 4. Padrão de resposta da API

### Sucesso
Envelope:
```json
{
  "success": true,
  "message": "...",
  "data": { }
}
```
ou `data` como lista.

### Erro (middleware)
Envelope:
```json
{
  "success": false,
  "message": "...",
  "errors": [],
  "statusCode": 400,
  "traceId": "..."
}
```

Mapeamento global de erro:
- `ValidationException` -> `400`
- `BusinessRuleException` -> `400`
- `ConflictException` -> `409`
- não tratado -> `500`

### Status usados nos controllers
- `GET` list/detalhe: `200` ou `204` (quando não há dados) + `500`
- `DELETE`: `200` ou `404` + `500`
- `POST`: `201` + (`400` validação) + (`409` em conflito de presença)
- `PUT`: `200` ou `404` + `400` + `500`

> Observação para frontend: em alguns cenários de ausência de dados, a API retorna `204 NoContent` (sem body).

---

## 5. Regra de negócio importante (Presença)

No cadastro/atualização de presença:
- `DataHoraCheckIn` deve estar entre `16:00` e `17:00` (referência de fuso: `America/Sao_Paulo`).
- Presença duplicada (`workshopId + colaboradorId`) gera conflito (`409`).

## 5.1 Observação de contrato (Workshop)

Para `POST/PUT` de workshop, o backend usa `DataRealizacao` como campo principal.

Por compatibilidade com integrações existentes, também aceita aliases no JSON:
- `dataHora`
- `dataHoraRealizacao`

Exemplo válido:
```json
{
  "nome": "Introdução a Angular",
  "descricao": "Fundamentos de componentes",
  "dataRealizacao": "2026-01-15T16:00:00"
}
```

---

## 6. CORS para Angular

A API está configurada para ler origens permitidas em:
- `Cors:AllowedOrigins`

Padrão atual:
- `http://localhost:4200`

Se o frontend usar outra porta/domínio, ajuste `AllowedOrigins`.

---

## 7. Configuração segura com User Secrets

Valores sensíveis **não** devem ficar em `appsettings` versionado.

### Exemplo (API project)

```powershell
dotnet user-secrets set "ConnectionStrings:Default" "Server=...;Database=...;..." --project src/DesafioFastBackend.API

dotnet user-secrets set "Jwt:Key" "SUA_CHAVE_FORTE_AQUI" --project src/DesafioFastBackend.API
dotnet user-secrets set "Jwt:ExpiresInMinutes" "180" --project src/DesafioFastBackend.API

dotnet user-secrets set "Auth:Users:0:Username" "admin" --project src/DesafioFastBackend.API
dotnet user-secrets set "Auth:Users:0:Password" "admin123" --project src/DesafioFastBackend.API
dotnet user-secrets set "Auth:Users:0:Role" "Admin" --project src/DesafioFastBackend.API

dotnet user-secrets set "Auth:Users:1:Username" "reader" --project src/DesafioFastBackend.API
dotnet user-secrets set "Auth:Users:1:Password" "reader123" --project src/DesafioFastBackend.API
dotnet user-secrets set "Auth:Users:1:Role" "Reader" --project src/DesafioFastBackend.API
```

---

## 8. Como executar (guia para avaliador)

### 8.1 Pré-requisitos

Instalar no computador:
- `.NET SDK 10`
- `SQL Server` (Express/Developer/LocalDB)
- `SQL Management studio 22`
- ferramenta `dotnet-ef`:

```powershell
dotnet tool install --global dotnet-ef
```

Validar instalação do .NET:

```powershell
dotnet --list-sdks
```

---

### 8.2 Clonar e restaurar

```powershell
git clone https://github.com/thydd/DesafioFastBackend.git
cd DesafioFastBackend
dotnet restore
```

---

### 8.3 Configurar secrets (obrigatório)

> Valores sensíveis devem ficar em User Secrets, não em `appsettings` versionado.

```powershell
dotnet user-secrets set "ConnectionStrings:Default" "Server=SEU_SERVIDOR;Database=DesafioFastDB;Trusted_Connection=True;TrustServerCertificate=True" --project src/DesafioFastBackend.API

dotnet user-secrets set "Jwt:Issuer" "DesafioFastBackend" --project src/DesafioFastBackend.API
dotnet user-secrets set "Jwt:Audience" "DesafioFastBackendUsers" --project src/DesafioFastBackend.API
dotnet user-secrets set "Jwt:Key" "SUA_CHAVE_FORTE_COM_32_OU_MAIS_CARACTERES" --project src/DesafioFastBackend.API
dotnet user-secrets set "Jwt:ExpiresInMinutes" "180" --project src/DesafioFastBackend.API

dotnet user-secrets set "Auth:Users:0:Username" "admin" --project src/DesafioFastBackend.API
dotnet user-secrets set "Auth:Users:0:Password" "admin123" --project src/DesafioFastBackend.API
dotnet user-secrets set "Auth:Users:0:Role" "Admin" --project src/DesafioFastBackend.API

dotnet user-secrets set "Auth:Users:1:Username" "reader" --project src/DesafioFastBackend.API
dotnet user-secrets set "Auth:Users:1:Password" "reader123" --project src/DesafioFastBackend.API
dotnet user-secrets set "Auth:Users:1:Role" "Reader" --project src/DesafioFastBackend.API
```

---

### 8.4 Criar/atualizar banco

```powershell
dotnet ef database update --project src/DesafioFastBackend.Infrastructure --startup-project src/DesafioFastBackend.API
```

Executar o script `scripts/seed-desafiofast.sql` no banco `DesafioFastDB`.

---

### 8.5 Rodar API

```powershell
dotnet run --project src/DesafioFastBackend.API
```

Ao subir, a API deve exibir URLs como:
- `https://localhost:7114`
- `http://localhost:5084`

Swagger:
- `https://localhost:7114/swagger`

---

### 8.6 Primeiro teste no Swagger

1. Chamar `POST /api/auth/login` com:

```json
{
  "username": "admin",
  "password": "admin123"
}
```

2. Copiar `accessToken` da resposta.
3. Clicar em `Authorize` no Swagger e informar: `Bearer {token}`.
4. Testar endpoints protegidos (`workshops`, `colaboradores`, `presencas`).

---

### 8.7 Troubleshooting rápido

- Erro de conexão com banco: revisar `ConnectionStrings:Default` e serviço do SQL Server.
- Erro de JWT key: revisar `Jwt:Key` nos secrets.
- Falha ao rodar migration: confirmar instalação do `dotnet-ef` e versão do SDK .NET.



