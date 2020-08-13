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
        [InlineData(2, new double[] { 500, 600, 300 })]
        [InlineData(3, new double[] { 500, 600, 300, 900 })]
        [InlineData(4, new double[] { 0, 0, 0, 900, 0 })]
        [InlineData(6, new double[] { 0, 0, 0, 0, 0, 9})]
        public void NaoPermiteNovosLancesDadoLeilaoEncerrado(int valorEsperado,
            double[] ofertas)
        {
            //Arranje
            var reservarUltimaOferta = ofertas.Length - 1;

            var leilao = new Leilao("Roupa do Gugu");
            var Joao = new Interessada("Joao", leilao);

            for (int i = 0; i < reservarUltimaOferta; i++)
            {
                leilao.RecebeLance(Joao, ofertas[i]);
            }

            leilao.TerminaPregao();

            //Act
            leilao.RecebeLance(Joao, ofertas[reservarUltimaOferta]);

            //Assert
            var valorObtido = leilao.Lances.Count();

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
