using System;
using System.Collections.Generic;
using System.Text;
using Igor.LeilaoOnline.Core;
using Xunit;

namespace Igor.LeilaoOnline.Testes
{
    public class LeilaoTerminaPregao
    {

        [Theory]
        [InlineData(803.2, new double[] { 800, 801, 802, 803, 803.2 })]
        [InlineData(900.5, new double[] { 600, 900, 900.5, 100, 50 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            //Arranje
            var leilao = new Leilao("Van Gogh");

            var Joao = new Interessada("João", leilao);
            var Maria = new Interessada("Maria", leilao);

            bool vezDaMaria = false;

            leilao.IniciaPregao();

            foreach (var item in ofertas)
            {
                if (vezDaMaria)
                {
                    leilao.RecebeLance(Maria, item);
                }
                else
                {
                    leilao.RecebeLance(Joao, item);
                }
                vezDaMaria = !vezDaMaria;
            }

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //Arranje
            var leilao = new Leilao("Nokia 1100i original");

            
            //Assert
            var excecaoObtida = Assert.Throws<InvalidOperationException>(
                //Act
                () => leilao.TerminaPregao()
            );

            var excecaoEsperada = new InvalidOperationException(
                "Não é possível terminar o pregão sem ele ter iniciado.");

            Assert.Equal(excecaoEsperada.Message, excecaoObtida.Message);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLance()
        {
            //Arranje
            var leilao = new Leilao("Roupa Jackson 5");
            leilao.IniciaPregao();

            //Act
            leilao.TerminaPregao();

            // Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Theory]
        [InlineData(1200, 1200.50, new double[] { 600, 1800, 1200.50, 1201 })]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(
            double valorDestino,
            double valorEsperado,
            double[] ofertas)
        {
            //Arranje
            var leilao = new Leilao("Van Gogh", valorDestino);

            var Joao = new Interessada("João", leilao);
            var Maria = new Interessada("Maria", leilao);

            bool vezDaMaria = false;

            leilao.IniciaPregao();

            foreach (var item in ofertas)
            {
                if (vezDaMaria)
                {
                    leilao.RecebeLance(Maria, item);
                }
                else
                {
                    leilao.RecebeLance(Joao, item);
                }
                vezDaMaria = !vezDaMaria;
            }

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
