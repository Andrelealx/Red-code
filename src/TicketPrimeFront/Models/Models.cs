namespace TicketPrimeFront.Models;

public class Usuario
{
    public string Cpf { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class Evento
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int CapacidadeTotal { get; set; }
    public DateTime DataEvento { get; set; }
    public decimal PrecoPadrao { get; set; }
}

public class Cupom
{
    public string Codigo { get; set; } = string.Empty;
    public decimal PorcentagemDesconto { get; set; }
    public decimal ValorMinimoRegra { get; set; }
}

public class ReservaReq
{
    public string UsuarioCpf { get; set; } = string.Empty;
    public int EventoId { get; set; }
    public string? CupomUtilizado { get; set; }
}

public class ReservaConsulta
{
    public int Id { get; set; }
    public string UsuarioCpf { get; set; } = string.Empty;
    public int EventoId { get; set; }
    public string NomeEvento { get; set; } = string.Empty;
    public string? CupomUtilizado { get; set; }
    public decimal ValorFinalPago { get; set; }
    public DateTime DataReserva { get; set; }
}
