using System;
using GerenciadorEstacionamento.Modelos;
using Xunit;
using Xunit.Abstractions;

namespace GerenciadorEstacionamentoTestes
{
    public class PatioTeste : IDisposable
    {
        private Veiculo veiculo;
        public ITestOutputHelper SaidaConsoleTeste;
        public PatioTeste(ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor invocado");
            veiculo = new Veiculo();
        }

        [Fact]
        public void ValidaFaturamento()
        {
            //Arrange
            var estacionamento = new Patio();
            //var veiculo = new Veiculo();
            veiculo.Proprietario = "Alysson";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "Preto";
            veiculo.Modelo = "Fusca";
            veiculo.Placa = "asd-9999";

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("Alysson", "asd-9999", "Preto", "fusca")]
        [InlineData("Alysson", "pme-8977", "Cinza", "Onix")]
        public void ValidaFaturamentoVariosVeiculos(string proprietario,
            string placa,
            string cor,
            string modelo)
        {
            //Arrange
            var estacionamento = new Patio();
            //var veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Placa = placa;

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("Alysson", "asd-9999", "Preto", "fusca")]
        public void LocalizaVeiculoNoPatio(string proprietario,
            string placa,
            string cor,
            string modelo)
        {
            //Arrange
            var estacionamento = new Patio();
            //var veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Placa = placa;

            estacionamento.RegistrarEntradaVeiculo(veiculo);

            //Act
            var consultado = estacionamento.PesquisaVeciulo(placa);

            //Assert
            Assert.Equal(placa, consultado.Placa);
        }

        [Fact]
        public void AlterarDadosVeiculo()
        {
            //Arrange
            Patio estacionamento = new Patio();
            //var veiculo = new Veiculo();
            veiculo.Proprietario = "Alysson";
            veiculo.Cor = "Preto";
            veiculo.Modelo = "Fusca";
            veiculo.Placa = "asd-9999";
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            var veiculoAlterado = new Veiculo();
            veiculoAlterado.Proprietario = "Alysson";
            veiculoAlterado.Cor = "Branco"; //alterado
            veiculoAlterado.Modelo = "Fusca";
            veiculoAlterado.Placa = "asd-9999";

            //Act
            Veiculo alterado = estacionamento.AlterarDadosVeiculo(veiculoAlterado);

            //Assert
            Assert.Equal(alterado.Cor, veiculoAlterado.Cor);
        }

        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Dispose invocado");
        }
    }
}