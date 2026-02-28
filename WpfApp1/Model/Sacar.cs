using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp1
{
    public class Sacar : Transacao
    {
        // 1. Variáveis privadas para guardar os dados
        private readonly Conta _contaOrigem;
        private readonly decimal _valor;

        // 2. Construtor: Recebe a conta e o valor AQUI
        public Sacar(Conta contaOrigem, decimal valor)
        {
            _contaOrigem = contaOrigem;
            _valor = valor;
        }

        // 3. Método Executar: Não pede nada, só processa o que já tem
        public bool Executar()
        {
            if (_valor <= 0) return false;

            // Usa as variáveis privadas (_contaOrigem e _valor)
            if (_contaOrigem.TentarDebitar(_valor))
            {
                _contaOrigem.Debitar(_valor);
                return true;
            }
            return false;
        }
    }
}