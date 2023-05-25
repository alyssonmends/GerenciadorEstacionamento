using System;
using GerenciadorEstacionamento.Modelos;
using Xunit;
using Xunit.Abstractions;

namespace GerenciadorEstacionamentoTestes
{
    public class VeiculoTeste : IDisposable
    {
        private Veiculo veiculo;
        public ITestOutputHelper SaidaConsoleTeste;
        public VeiculoTeste(ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor invocado");
            veiculo = new Veiculo();
        }

        [Fact(DisplayName = "Título Teste")]
        [Trait("Funcionalidade", "Acelerar")]
        public void TestaVeiculoAcelerar()
        {
            //Arrange
            //var veiculo = new Veiculo();
            //Act
            veiculo.Acelerar(10);
            //Assert
            Assert.Equal(100, veiculo.VelocidadeAtual);
        }

        [Fact]
        [Trait("Funcionalidade", "Frear")]
        public void TesteVeiculoFrear()
        {
            //var veiculo = new Veiculo();
            veiculo.Frear(10);
            Assert.Equal(-150, veiculo.VelocidadeAtual);
        }

        [Fact(DisplayName = "Teste pulado", Skip = "Pular teste")]
        public void ValidaNomeProprietario()
        {

        }

        [Fact]
        public void DadosVeiculo()
        {
            //Arrange
            var carro = new Veiculo();
            carro.Proprietario = "Alysson";
            carro.Tipo = TipoVeiculo.Automovel;
            carro.Cor = "Branco";
            carro.Modelo = "Fusca";
            carro.Placa = "asd-9999";

            //Act
            string dados = carro.ToString();

            //Assert
            Assert.Contains("Ficha do veículo:", dados);
        }

        [Fact]
        public void TestaNomeProprietarioVeiculoComMenosDeTresCaracteres()
        {
            //Arrange
            string nomeProprietario = "Ab";

            //Assert
            Assert.Throws<System.FormatException>(
                //Act
                () => new Veiculo(nomeProprietario)
            );
        }

        [Fact]
        public void TestaMensagemDeExcecaoDoQuartoCaractereDaPlaca()
        {
            //Arrange
            string placa = "ASDF8888";

            //Act
            var mensagem = Assert.Throws<System.FormatException>(
                () => new Veiculo().Placa = placa
            );

            //Assert
            Assert.Equal("O 4° caractere deve ser um hífen", mensagem.Message);
        }

        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Dispose invocado");
        }
    }
}
