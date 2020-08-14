using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Igor.LeilaoOnline.Core
{
    public class MaiorValor : IModalidade
    {
        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                .DefaultIfEmpty(new Lance(null, 0))
                .OrderBy(l => l.Valor)
                .FirstOrDefault();
        }
    }
}
