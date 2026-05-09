# Correção TicketPrime (Red-code) — AV1

## Resumo

| Avaliação | Nota |
| --- | --- |
| **AV1** | **10 / 10** |

---

## AV1 — Detalhamento

| Item | Critério | Resultado | Justificativa |
| --- | --- | --- | --- |
| 1 | `docs/requisitos.md` com pelo menos 3 blocos contendo `Como`, `Quero` e `Para` | ✅ 1 | `docs/requisitos.md` traz 3 histórias de usuário no formato correto: "Cadastro de Evento", "Cadastro de Usuário" e "Gestão de Cupons", cada uma com `Como <papel>, Quero <ação>, Para <objetivo>`. Exemplo: "Como Comprador, Quero me cadastrar informando meu CPF, nome e e-mail, Para que eu possa realizar reservas de ingressos no sistema." (O projeto ainda mantém um `requisitos.md` na raiz com 36 HUs adicionais, reforçando o item.) |
| 2 | `docs/requisitos.md` com pelo menos 1 cenário contendo `Dado`, `Quando` e `Então` | ✅ 1 | O mesmo arquivo traz o cenário "Cadastro de Usuário com CPF Duplicado": "Dado que o sistema já possui um usuário cadastrado... / Quando um novo cadastro é tentado com esse mesmo CPF / Então o sistema deve retornar um erro 400". |
| 3 | `README.md` com blocos de código Markdown contendo comandos de terminal (ex: `dotnet run`, `dotnet build`) | ✅ 1 | O README na raiz tem múltiplos blocos ```powershell com `dotnet restore`, `dotnet run --project src/TicketPrimeApi/TicketPrimeApi.csproj`, `dotnet run --project src/TicketPrimeFront/TicketPrimeFront.csproj --urls http://localhost:5139` e `dotnet test tests/TicketPrimeTests.csproj`. |
| 4 | Pasta `/db` com arquivo `.sql` contendo `CREATE TABLE` | ✅ 1 | `db/script.sql` cria as 4 tabelas obrigatórias (`Usuarios`, `Eventos`, `Cupons`, `Reservas`) com `PRIMARY KEY`, `IDENTITY` e `FOREIGN KEY` entre Reservas → Usuarios/Eventos/Cupons. Script idempotente com `IF OBJECT_ID IS NULL`. |
| 5 | `/src` com arquivos `.cs` contendo `app.MapGet` ou `app.MapPost` | ✅ 1 | `src/TicketPrimeApi/Program.cs` registra `app.MapPost("/api/usuarios", ...)`, `app.MapPost("/api/eventos", ...)`, `app.MapGet("/api/eventos", ...)`, `app.MapGet("/api/eventos/{id}", ...)`, `app.MapPost("/api/cupons", ...)`, `app.MapGet("/api/cupons/{codigo}", ...)`, `app.MapPost("/api/reservas", ...)` e `app.MapGet("/api/reservas/{cpf}", ...)`. |
| 6 | `/src` com retornos explícitos de `Results.BadRequest` ou `Results.NotFound` | ✅ 1 | Program.cs usa fail-fast em vários handlers. Exemplos: `Results.BadRequest("Erro: CPF deve ter exatamente 11 caracteres.")`, `Results.BadRequest("Erro: CPF já cadastrado.")`, `Results.NotFound("Evento não encontrado.")`, `Results.NotFound("Cupom não encontrado.")`, `Results.BadRequest("Erro Crítico: Overbooking...")`. |
| 7 | Uso do caractere `@` nas strings de query Dapper | ✅ 1 | Todas as queries usam parâmetros nomeados: `@Cpf`, `@Email`, `@Id`, `@Codigo`, `@EventoId`, `@UsuarioCpf`, `@ValorFinalPago`. Exemplo: `"SELECT COUNT(1) FROM Usuarios WHERE Cpf = @Cpf"`, `"INSERT INTO Reservas (UsuarioCpf, EventoId, CupomUtilizado, ValorFinalPago) VALUES (@UsuarioCpf, @EventoId, @CupomUtilizado, @ValorFinalPago)"`. |
| 8 | Não usar `+` nem interpolação `$"{ }"` em comandos `SELECT/INSERT/UPDATE/DELETE` | ✅ 1 | Buscas por `$"...SELECT/INSERT/UPDATE/DELETE"` e por concatenação com `+` em strings SQL não retornaram ocorrências. As interpolações `$"..."` no arquivo aparecem apenas em URLs de `Results.Created` (ex: `$"/api/usuarios/{user.Cpf}"`), sem tocar em SQL. |
| 9 | `/tests` com `.cs` contendo `[Fact]` ou `[Theory]` | ✅ 1 | `tests/UnitTest1.cs` tem 4 testes com `[Fact]` e 5 testes com `[Theory]` + `[InlineData]`, cobrindo regras R2, R3, R4, cálculo de cupom, validação de CPF, e-mail e porcentagem. `using Xunit;` no topo confirma o framework. |
| 10 | `Assert.` dentro dos métodos de teste | ✅ 1 | Todos os 9 métodos de teste contêm chamadas `Assert.`: `Assert.Equal`, `Assert.True`. Nenhum teste sem Assert. |

**Total AV1: 10 / 10**

---

## Justificativa da nota final

O projeto **Red-code / TicketPrime** atinge nota cheia na AV1 com destaque para:

- **Documentação de requisitos dupla**: o `docs/requisitos.md` cobre o mínimo exigido (3 HUs + 1 BDD) e o `requisitos.md` na raiz expande com 36 HUs e um DoD consolidado cobrindo AV1, AV2 e regras gerais.
- **README executável e completo**, com múltiplos blocos PowerShell para setup automático via `npm run dev`, setup manual com `dotnet restore/run/test`, e até um `setup-local.ps1` com `winget` para Windows novo.
- **Banco SQL Server versionado** em `db/script.sql`, idempotente, com as 4 tabelas obrigatórias e três FKs em `Reservas`.
- **Minimal API** cobrindo todos os endpoints obrigatórios da AV1 (usuários, eventos, cupons, reservas) com fail-fast para CPF duplicado, e-mail inválido, data passada, overbooking (R3), limite por CPF (R2) e motor de cupons (R4).
- **Dapper 100% parametrizado** (`@param`) em todos os 13+ SQLs mapeados, sem nenhum sinal de concatenação ou interpolação.
- **Suíte xUnit robusta** com 9 métodos e casos parametrizados via `[Theory]`/`[InlineData]`, cobrindo as regras de negócio R2, R3, R4 e validações auxiliares.

Todos os 10 critérios da AV1 foram atendidos sem ressalvas. **Nota final AV1: 10 / 10.**
