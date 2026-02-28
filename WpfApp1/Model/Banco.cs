using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp1
{
    public class Banco
    {
        public int BancoId { get; set; }
        public string Nome { get; set; }

        // Relação 1:1 com Calendario
        public Calendario Calendario { get; set; }

        // Construtor
        public Banco(string nome)
        {
            Nome = nome;
        }

        public Banco() { }
    }
}
