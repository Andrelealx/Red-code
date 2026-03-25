using Dapper;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// String de conexão para o seu SQL Server local
const string connectionString = @"Server=localhost\SQLEXPRESS;Database=TicketPrime;Trusted_Connection=True;TrustServerCertificate=True;";

// ---------------------------------------------------------
// ROTA PING: Só para você ver a API rodando no navegador!
// ---------------------------------------------------------
app.MapGet("/", () => "API TicketPrime rodando como um Tanque de Guerra!");

// ---------------------------------------------------------
// ENDPOINTS OBRIGATÓRIOS DA AV1
// ---------------------------------------------------------

app.MapPost("/api/eventos", async (Evento evento) => {
    using var db = new SqlConnection(connectionString);
    var sql = "INSERT INTO Eventos (Nome, CapacidadeTotal, DataEvento, PrecoPadrao) VALUES (@Nome, @CapacidadeTotal, @DataEvento, @PrecoPadrao)";
    await db.ExecuteAsync(sql, evento); 
    return Results.Created($"/api/eventos", evento);
});

app.MapGet("/api/eventos", async () => {
    using var db = new SqlConnection(connectionString);
    var eventos = await db.QueryAsync<Evento>("SELECT * FROM Eventos");
    return Results.Ok(eventos);
});

app.MapPost("/api/usuarios", async (Usuario usuario) => {
    using var db = new SqlConnection(connectionString);
    
    // Fail-Fast: Regra de CPF único
    var existe = await db.QueryFirstOrDefaultAsync<string>("SELECT Cpf FROM Usuarios WHERE Cpf = @Cpf", new { usuario.Cpf });
    if (existe != null) return Results.BadRequest("Erro: CPF já cadastrado.");

    var sql = "INSERT INTO Usuarios (Cpf, Nome, Email) VALUES (@Cpf, @Nome, @Email)";
    await db.ExecuteAsync(sql, usuario);
    return Results.Created($"/api/usuarios/{usuario.Cpf}", usuario);
});

app.MapPost("/api/cupons", async (Cupom cupom) => {
    using var db = new SqlConnection(connectionString);
    var sql = "INSERT INTO Cupons (codigo, PorcentagemDesconto, valorMinimoregra) VALUES (@codigo, @PorcentagemDesconto, @valorMinimoregra)";
    await db.ExecuteAsync(sql, cupom);
    return Results.Created($"/api/cupons/{cupom.codigo}", cupom);
});

app.Run();

// ---------------------------------------------------------
// MODELOS DE DADOS
// ---------------------------------------------------------
public record Usuario(string Cpf, string Nome, string Email);
public record Evento(string Nome, int CapacidadeTotal, DateTime DataEvento, decimal PrecoPadrao);
public record Cupom(string codigo, decimal PorcentagemDesconto, decimal valorMinimoregra);