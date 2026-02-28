using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp1
{
    public class ContaCorrente : Conta
    {
        public decimal LimiteCredito { get; set; }

        // Sobrescreve a Regra de Validação: Inclui o limite de crédito.
        public override bool TentarDebitar(decimal valor)
        {
            // A conta corrente pode debitar usando o saldo + o limite de crédito.
            return Saldo + LimiteCredito >= valor;
        }

    }
}
