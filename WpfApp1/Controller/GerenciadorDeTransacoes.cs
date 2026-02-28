using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Persistence;

namespace WpfApp1.Controller
{
    internal class GerenciadorDeTransacoes
    {
        private readonly FintechPooDbContext _context;

        public GerenciadorDeTransacoes(FintechPooDbContext context)
        {
            _context = context;
        }

        // Método auxiliar para buscar a conta
        public Conta BuscarContaPorNumero(string numeroConta)
        {
            return _context.Contas.FirstOrDefault(c => c.Numero == numeroConta);
        }

        public bool ProcessarDeposito(string numeroConta, decimal valor)
        {
            var conta = BuscarContaPorNumero(numeroConta);
            if (conta == null) return false;

            Transacao deposito = new Depositar(conta, valor);

            // Executa sem parâmetros (Polimorfismo)
            if (deposito.Executar())
            {
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ProcessarSaque(string numeroConta, decimal valor)
        {
            var conta = BuscarContaPorNumero(numeroConta);
            if (conta == null) return false;

            Transacao saque = new Sacar(conta, valor);

            // 2. Chama o Executar sem parâmetros
            if (saque.Executar())
            {
                _context.SaveChanges(); // Persiste a mudança
                return true;
            }
            return false;
        }

        public bool ProcessarTransferencia(string contaOrigemNum, string contaDestinoNum, decimal valor)
        {
            var origem = BuscarContaPorNumero(contaOrigemNum);
            var destino = BuscarContaPorNumero(contaDestinoNum);

            if (origem == null || destino == null) return false;

            Transacao transferencia = new Transferir(origem, destino, valor);

            if (transferencia.Executar())
            {
                _context.SaveChanges(); // Salva as alterações nas duas contas
                return true;
            }
            return false;
        }
    }
}
