using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp1
{
    public class Transferir : Transacao
    {
        private readonly Conta _origem;
        private readonly Conta _destino;
        private readonly decimal _valor;

        // Recebe as 3 informações necessárias no construtor
        public Transferir(Conta origem, Conta destino, decimal valor)
        {
            _origem = origem;
            _destino = destino;
            _valor = valor;
        }

        public bool Executar()
        {
            if (_valor <= 0) return false;

            if (_origem.TentarDebitar(_valor))
            {
                _origem.Debitar(_valor);
                _destino.Creditar(_valor);
                return true;
            }
            return false;
        }
    }
}
