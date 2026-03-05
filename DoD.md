# Definition of Done (DoD) - TicketPrime

Baseado em: `requisitos.md`

## 1. DoD Funcional (Historias de Usuario)

- [ ] HU-01: `POST /api/eventos` cria evento com `nome`, `data`, `capacidade`, `precoPadrao` e retorna `HTTP 201`.
- [ ] HU-02: `POST /api/cupons` cria cupom com `codigo`, `porcentagem`, `valorMinimo` e retorna `HTTP 201`.
- [ ] HU-03: `POST /api/usuarios` cria usuario com `cpf`, `nome`, `email`; CPF duplicado retorna `HTTP 400`.
- [ ] HU-04: `POST /api/reservas` cria reserva para CPF cadastrado e evento com vaga; sem cupom usa preco cheio; com cupom valido aplica desconto quando regra de valor minimo for atendida.
- [ ] HU-05: `GET /api/reservas/{cpf}` retorna reservas do CPF com `NomeEvento` (via JOIN) e `ValorFinalPago`; sem reservas retorna `HTTP 200` com lista vazia.
- [ ] HU-06: Reserva bloqueada quando capacidade do evento foi atingida (`HTTP 400`).
- [ ] HU-07: Reserva bloqueada quando CPF ja possui 2 reservas para o mesmo evento (`HTTP 400`).

## 2. DoD de Qualidade (BDD e Validacoes)

- [ ] Requisitos registrados no formato de historias: `Como`, `Quero`, `Para`.
- [ ] Criterios BDD registrados no formato: `Dado`, `Quando`, `Entao`.
- [ ] Todos os cenarios de erro usam fail-fast (`BadRequest` ou `NotFound`) com mensagem clara.
- [ ] Regras R1, R2, R3 e R4 cobertas por testes automatizados.

## 3. DoD Tecnico - AV1 (Fundacao)

- [ ] `README.md` na raiz com instrucoes de execucao em bloco de codigo.
- [ ] Script SQL em `/db` com `CREATE TABLE` para `Usuarios`, `Eventos`, `Cupons`, `Reservas`, incluindo FKs.
- [ ] API minimal em `/src` com mapeamento de endpoints obrigatorios da AV1 (`MapGet` e `MapPost`).
- [ ] Todas as queries Dapper usam parametros (`@Param`), sem concatenacao ou interpolacao de SQL.
- [ ] Projeto de testes em `/tests` com pelo menos um teste `[Fact]` ou `[Theory]`.
- [ ] Todo teste possui `Assert` valido.

## 4. DoD Tecnico - AV2 (Arquitetura e Operacao)

- [ ] ADR em `/docs` com secoes: `## Contexto`, `## Decisao`, `## Consequencias`.
- [ ] Em `## Consequencias`, existem `Pros` e `Contras` explicitos.
- [ ] `/docs/operacao.md` contem matriz de riscos com colunas: `Risco`, `Probabilidade`, `Impacto`, `Acao`, `Gatilho`.
- [ ] `/docs/operacao.md` define metrica com: `Formula`, `Fonte de Dados`, `Frequencia`, `Acao se Violado`.
- [ ] Documento explicita `SLO` com porcentagem e janela de tempo.
- [ ] Documento explicita `Error Budget Policy`.
- [ ] Nenhum arquivo `.cs` contem credenciais em texto plano (`Password=`, `User Id=`).
- [ ] `release_checklist_final.md` existe na raiz com todos os itens finais marcados.

## 5. DoD de Regras de Negocio (Endpoints)

- [ ] R1 Integridade: em `POST /api/reservas`, valida existencia de `UsuarioCpf` e `EventoId` antes do insert.
- [ ] R2 Limite por CPF: bloqueia terceira reserva do mesmo CPF no mesmo evento.
- [ ] R3 Estoque: bloqueia reserva quando total de reservas >= `CapacidadeTotal`.
- [ ] R4 Motor de cupons: aplica desconto somente se `PrecoPadrao >= ValorMinimoRegra`.
- [ ] Consulta com JOIN: `GET /api/reservas/{cpf}` retorna nome do evento (nao apenas ID).

## 6. DoD de Entrega (Regras Gerais)

- [ ] Estrutura de pastas na raiz: `/docs`, `/db`, `/src`, `/tests`.
- [ ] Entrega feita por URL de repositorio GitHub ou GitLab.
- [ ] Grupo respeita tamanho entre 5 e 6 alunos.
- [ ] Persistencia com Dapper (sem Entity Framework e sem banco em memoria).
- [ ] Nomes de pastas, tabelas, colunas e rotas seguem exatamente o especificado nos requisitos.

## 7. Condicao de Conclusao

Um item (historia, regra, documento ou endpoint) so e considerado "Done" quando:

- [ ] Implementacao concluida.
- [ ] Criterio BDD correspondente atendido.
- [ ] Teste automatizado passando.
- [ ] Evidencia registrada (codigo, teste, documento ou resposta de endpoint).

O projeto so e considerado "Done" quando todos os itens deste arquivo estiverem marcados.
