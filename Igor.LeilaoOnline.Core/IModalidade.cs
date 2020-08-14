using System;
using System.Collections.Generic;
using System.Text;

namespace Igor.LeilaoOnline.Core
{
    public interface IModalidade
    {
        Lance Avalia(Leilao leilao); 
    }
}
