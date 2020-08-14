using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Igor.LeilaoOnline.Core;


namespace Igor.LeilaoOnline.Testes
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            //Arranje
            var valorNegativo = -55;

            //Assert
            Assert.Throws<ArgumentException>(
                //Act
                () => new Lance(null, valorNegativo)
            );
        }
    }
}
