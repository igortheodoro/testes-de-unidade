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
        private Interessada UltimoCliente { get; set; }
        private IModalidade Avaliador { get; set; }

        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; private set; }

        public Leilao(string peca, IModalidade modalidade)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
            Avaliador = modalidade;
        }

        private bool LanceAceito(Interessada cliente, double valor)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento)
                && (cliente != UltimoCliente);
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (LanceAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                UltimoCliente = cliente;
            }
        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
            {
                throw new InvalidOperationException(
                    "Não é possível terminar o pregão sem ele ter iniciado.");
            }

            if (ValorDestino > 0)
            {
                //Modalidade oferta superior mais próxima
                Ganhador = _lances
                    .DefaultIfEmpty(new Lance(null, 0))
                    .Where(l => l.Valor > ValorDestino)
                    .OrderBy(l => l.Valor)
                    .FirstOrDefault();
            }
            else
            {
                //Modalidade maior valor
                Ganhador = _lances
                    .DefaultIfEmpty(new Lance(null, 0))
                    .OrderBy(l => l.Valor)
                    .LastOrDefault();
            }

            Estado = EstadoLeilao.LeilaoFinalizado;
        }
    }
}
