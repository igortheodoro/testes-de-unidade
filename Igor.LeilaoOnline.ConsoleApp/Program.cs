using System;
using Igor.LeilaoOnline.Core;

namespace Igor.LeilaoOnline.ConsoleApp
{
    class Program
    {
        private static void Verifica(double valorObtido, double valorEsperado)
        {
            Console.WriteLine(valorObtido == valorEsperado);
        }
        private static void LeilaoVariosLances()
        {
            //Arranje
            var leilao = new Leilao("Van Gogh");

            var Joao = new Interessada("João", leilao);
            var Maria = new Interessada("Maria", leilao);
            var Marcos = new Interessada("Marcos", leilao);

            leilao.RecebeLance(Joao, 600);
            leilao.RecebeLance(Maria, 900);
            leilao.RecebeLance(Marcos, 901);
            leilao.RecebeLance(Joao, 100);
            leilao.RecebeLance(Joao, 50);

            //Act
            leilao.TerminaPregao();

            //Assert
            Verifica(leilao.Ganhador.Valor, 901);
        }

        private static void LeilaoApenasUmLance()
        {
            //Arranje
            var leilao = new Leilao("Van Gogh");

            var Joao = new Interessada("João", leilao);

            leilao.RecebeLance(Joao, 600);

            //Act
            leilao.TerminaPregao();

            //Assert

            Verifica(leilao.Ganhador.Valor, 600);
        }

        static void Main()
        {
            LeilaoVariosLances();
            LeilaoApenasUmLance();
        }
    }
}
