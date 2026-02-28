using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Depositar : Transacao
    {
        private readonly Conta _contaDestino;
        private readonly decimal _valor;

        // Os dados entram aqui no construtor
        public Depositar(Conta contaDestino, decimal valor)
        {
            _contaDestino = contaDestino;
            _valor = valor;
        }

        // O método não precisa de parâmetros, pois já tem os dados salvos acima
        public bool Executar()
        {
            if (_valor <= 0) return false;
            _contaDestino.Creditar(_valor);
            return true;
        }
    }
}
