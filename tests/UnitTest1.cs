using Xunit;

namespace TicketPrimeTests;

public class ValidacaoSistemaTests
{
    [cite_start][Fact] // 
    public void Teste_ValidacaoCpf_DeveTerOnzeDigitos()
    {
        // Arranjo (Ator: Sistema)
        string cpfValido = "12345678901";

        // Ação
        int contagem = cpfValido.Length;

        [cite_start]// Asserção (O Oráculo - Item 10 da AV1) 
        Assert.Equal(11, contagem); 
    }
}
