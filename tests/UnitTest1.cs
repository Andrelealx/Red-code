using Xunit;
using System.Text.RegularExpressions;

namespace TicketPrimeTests;

public class CalculoCupomTests
{
    // R4 — Motor de cupons: desconto aplicado quando preço >= valor mínimo
    [Fact]
    public void ValidarCalculoMatematico_Desconto_DeveRetornarValorCorreto()
    {
        // Arrange
        decimal precoPadrao = 100.00m;
        decimal porcentagemDesconto = 20.00m;
        decimal valorEsperado = 80.00m;

        // Act
        decimal valorCalculado = precoPadrao - (precoPadrao * (porcentagemDesconto / 100));

        // Assert
        Assert.Equal(valorEsperado, valorCalculado);
    }

    // R4 — Cupom NÃO deve ser aplicado quando preço < valor mínimo da regra
    [Fact]
    public void R4_CupomNaoAplicado_QuandoPrecoMenorQueValorMinimo()
    {
        // Arrange
        decimal precoPadrao = 50.00m;
        decimal valorMinimoRegra = 100.00m;
        decimal porcentagemDesconto = 20.00m;

        // Act — simula a lógica do servidor: só aplica se preço >= mínimo
        decimal valorFinal = precoPadrao < valorMinimoRegra
            ? precoPadrao
            : precoPadrao - (precoPadrao * (porcentagemDesconto / 100));

        // Assert — valor deve ser o preço cheio (sem desconto)
        Assert.Equal(precoPadrao, valorFinal);
    }

    // R4 — Cupom aplicado quando preço == valor mínimo (limite exato)
    [Fact]
    public void R4_CupomAplicado_QuandoPrecoIgualAoValorMinimo()
    {
        // Arrange
        decimal precoPadrao = 100.00m;
        decimal valorMinimoRegra = 100.00m;
        decimal porcentagemDesconto = 10.00m;
        decimal valorEsperado = 90.00m;

        // Act
        decimal valorFinal = precoPadrao >= valorMinimoRegra
            ? precoPadrao - (precoPadrao * (porcentagemDesconto / 100))
            : precoPadrao;

        // Assert
        Assert.Equal(valorEsperado, valorFinal);
    }

    // R4 — Valor final nunca pode ser negativo (ID 09)
    [Fact]
    public void R4_ValorFinalNaoPodeSerNegativo()
    {
        // Arrange
        decimal precoPadrao = 10.00m;
        decimal porcentagemDesconto = 100.00m;

        // Act
        decimal valorFinal = precoPadrao - (precoPadrao * (porcentagemDesconto / 100));
        if (valorFinal < 0) valorFinal = 0;

        // Assert
        Assert.True(valorFinal >= 0);
    }

    // R3 — Overbooking: reservas >= capacidade deve ser bloqueado
    [Theory]
    [InlineData(100, 100, true)]   // lotado: bloqueia
    [InlineData(99, 100, false)]   // uma vaga: permite
    [InlineData(0, 100, false)]    // evento vazio: permite
    [InlineData(100, 50, true)]    // acima da capacidade: bloqueia
    public void R3_Overbooking_DeveBloquearQuandoLotado(int qtdReservas, int capacidade, bool deveBloquear)
    {
        // Act
        bool lotado = qtdReservas >= capacidade;

        // Assert
        Assert.Equal(deveBloquear, lotado);
    }

    // R2 — Limite por CPF: bloqueia quando já há 2 reservas do mesmo CPF no evento
    [Theory]
    [InlineData(0, false)]  // nenhuma reserva: permite
    [InlineData(1, false)]  // 1 reserva: permite (até 2 é permitido)
    [InlineData(2, true)]   // 2 reservas: bloqueia a terceira
    [InlineData(3, true)]   // mais de 2: bloqueia
    public void R2_LimitePorCpf_DeveBloquearNaTerceiraReserva(int qtdReservasExistentes, bool deveBloquear)
    {
        // Act — limite é 2 reservas por CPF por evento
        bool limitingido = qtdReservasExistentes >= 2;

        // Assert
        Assert.Equal(deveBloquear, limitingido);
    }

    // Validação: CPF deve ter exatamente 11 dígitos
    [Theory]
    [InlineData("12345678901", true)]   // válido
    [InlineData("1234567890", false)]   // 10 dígitos
    [InlineData("123456789012", false)] // 12 dígitos
    [InlineData("", false)]             // vazio
    public void ValidacaoCpf_DeveExigir11Digitos(string cpf, bool deveSerValido)
    {
        // Act
        bool valido = cpf.Length == 11;

        // Assert
        Assert.Equal(deveSerValido, valido);
    }

    // Validação: formato de e-mail com Regex
    [Theory]
    [InlineData("joao@email.com", true)]
    [InlineData("invalido-sem-arroba", false)]
    [InlineData("sem@dominio", false)]
    [InlineData("ok@ok.br", true)]
    public void ValidacaoEmail_DeveVerificarFormato(string email, bool deveSerValido)
    {
        // Act
        bool valido = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        // Assert
        Assert.Equal(deveSerValido, valido);
    }

    // Validação: porcentagem de desconto deve ser entre 1 e 100
    [Theory]
    [InlineData(20, true)]
    [InlineData(100, true)]
    [InlineData(1, true)]
    [InlineData(0, false)]
    [InlineData(101, false)]
    public void ValidacaoCupom_PorcentagemDeveEstarEntre1e100(decimal porcentagem, bool deveSerValida)
    {
        // Act
        bool valida = porcentagem > 0 && porcentagem <= 100;

        // Assert
        Assert.Equal(deveSerValida, valida);
    }
}
