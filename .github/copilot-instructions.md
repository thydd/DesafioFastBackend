# Copilot Instructions

## Project Guidelines
- Projeto DesafioFastBackend: implementar API REST em C# com CRUD completo para endpoints /api/workshops e /api/colaboradores. Entidades: Colaborador (int Id, string Nome) e Workshop (id, nome, data de realização, descrição). Bônus opcionais: persistência em banco relacional (MySQL/SQL Server), autenticação/autorização e documentação com Swagger.
- O projeto deve usar User Secrets para armazenar dados sensíveis (ex.: chaves JWT e credenciais), evitando manter esses valores em appsettings versionado.

## Code General Rules
- Sempre usar Async para operações de I/O e sufixo Async nos métodos assíncronos neste projeto.
- Sempre usar Records para dtos.
- Usar List como retorno apenas se for usar operações de modificação nela; se não, utilize IEnumerable como retorno para os readonly.

## UseCase General Rules
- Usar dtos para retorno e recebimento de dados.
- Sempre criar um validator para o usecase, use o fluentvalidation.
- Um usecase por ação.
- Use repositories por meio de DI.
- Ao criar uma usecase, sempre criar a interface dela primeiro.
- Registrar useCase no ApplicationModule.

## Controller General Rules
- Evitar uso de try/catch no controller; tratar fluxo sem blocos de captura no controller.

## Exceptions General Rules
- So crie uma nova exeção, se realmente for uma exeção. Retornos nulos, não são exceptions, eles ja são esperados/corriqueiros.