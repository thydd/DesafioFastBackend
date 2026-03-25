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

## 8. Como executar

1. Restaurar pacotes:
```powershell
dotnet restore
```

2. Aplicar migrations (se necessário):
```powershell
dotnet ef database update --project src/DesafioFastBackend.Infrastructure --startup-project src/DesafioFastBackend.API
```

3. Rodar API:
```powershell
dotnet run --project src/DesafioFastBackend.API
```

4. Abrir Swagger:
- `https://localhost:7114/swagger` (perfil HTTPS padrão)

---

## 9. Integração recomendada no Angular

1. Fazer login em `/api/auth/login`.
2. Guardar `accessToken` (memória/session storage conforme estratégia).
3. Enviar header em chamadas protegidas:
   - `Authorization: Bearer <token>`
4. Tratar status no frontend:
   - `200/201`: fluxo normal
   - `204`: tela vazia/lista vazia
   - `400`: exibir validações
   - `401`: redirecionar para login
   - `404`: recurso não encontrado
   - `409`: conflito de regra de negócio
   - `500`: erro genérico com fallback

Sugestão: criar `HttpInterceptor` para anexar token automaticamente.

---

## 10. Observações finais

- Swagger já está com esquema `Bearer` configurado.
- O projeto usa políticas por role, facilitando evolução futura (novos perfis/permissões).
- Para produção, use senha com hash e gestão de identidade robusta (ex.: Identity + refresh token + rotação de chave).
