using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Igor.LeilaoOnline.Core;
using System.Linq;

namespace Igor.LeilaoOnline.Testes
{
    public class LeilaoRecebeLance
    {
        [Theory]
        [InlineData(3, new double[] { 500, 600, 300 })]
        [InlineData(4, new double[] { 500, 600, 300, 900 })]
        [InlineData(5, new double[] { 0, 0, 0, 900, 0 })]
        [InlineData(6, new double[] { 0, 0, 0, 0, 0, 9 })]
        public void NaoPermiteNovosLancesDadoLeilaoEncerrado(int valorEsperado,
            double[] ofertas)
        {
            //Arranje

            var leilao = new Leilao("Roupa do Gugu");
            var Joao = new Interessada("Joao", leilao);
            var Maria = new Interessada("Maria", leilao);
            var Igor = new Interessada("Igor", leilao);

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

            leilao.TerminaPregao();

            //Act
            leilao.RecebeLance(Igor, 500);

            //Assert
            var valorObtido = leilao.Lances.Count();

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void NaoPermiteLancesConsecutivosDosMesmosClientes()
        {
            //Arranje
            var leilao = new Leilao("Baixo do Junior Groovador");

            var Joao = new Interessada("João", leilao);

            leilao.IniciaPregao();
            leilao.RecebeLance(Joao, 300);

            //Act
            leilao.RecebeLance(Joao, 500);

            //Assert
            leilao.TerminaPregao();

            var resultadoEsperado = 1;
            var resultadoObtido = leilao.Lances.Count();

            Assert.Equal(resultadoEsperado, resultadoObtido);
        }
    }
}
