using System;
using System.Collections.Generic;
using System.Text;

namespace Igor.LeilaoOnline.Core
{
    public class Lance
    {
        public Interessada Cliente { get; }
        public double Valor { get; }

        public Lance(Interessada cliente, double valor)
        {
            if (valor < 0)
            {
                throw new ArgumentException("Não é possível fazer lances com valores negativos.");
            }

            Cliente = cliente;
            Valor = valor;
        }
    }
}
