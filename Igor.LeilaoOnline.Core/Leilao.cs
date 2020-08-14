using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Igor.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoAntesDoPregao,
        LeilaoEmAndamento,
        LeilaoFinalizado
    }
    public class Leilao
    {
        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; private set; }
        private Interessada UltimoCliente { get; set; }

        public Leilao(string peca)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (Estado == EstadoLeilao.LeilaoEmAndamento)
            {
                if (cliente != UltimoCliente)
                {
                    _lances.Add(new Lance(cliente, valor));
                    UltimoCliente = cliente;
                }
            }
        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            Ganhador = _lances
                .DefaultIfEmpty(new Lance(null,0))
                .OrderBy(l => l.Valor)
                .LastOrDefault();
            Estado = EstadoLeilao.LeilaoFinalizado;
        }
    }
}
