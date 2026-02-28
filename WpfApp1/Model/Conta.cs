using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp1
{
    public abstract class Conta
    {
        public int ContaId { get; set; }
        public string Numero { get; set; }
        public decimal Saldo { get; set; }

        // Chave Estrangeira (FK)
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public void Creditar(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(valor), "O valor deve ser positivo.");
            }
            Saldo += valor;
        }

        // Este método será usado pelas classes de transação para verificar se a Conta pode ser debitada.
        public virtual bool TentarDebitar(decimal valor)
        {
            return Saldo >= valor;
        }

        public void Debitar(decimal valor)
        {
            if (!TentarDebitar(valor))
            {
                throw new InvalidOperationException("Saldo insuficiente.");
            }
            Saldo -= valor;
        }
    }
}
