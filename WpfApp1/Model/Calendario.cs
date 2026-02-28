using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Calendario
    {
        public int CalendarioId { get; set; }
        public int AnoAtual { get; set; }

        // Propriedade de navegação inversa 1:1
        public Banco Banco { get; set; }
        public int BancoId { get; set; } // Chave Estrangeira

        public Calendario(int anoAtual)
        {
            AnoAtual = anoAtual;
        }

        public Calendario() { }
    }
}
