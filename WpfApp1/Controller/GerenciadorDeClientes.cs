using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Persistence;


namespace WpfApp1.Controller
{
    internal class GerenciadorDeClientes
    {
        private readonly FintechPooDbContext _context;

        public GerenciadorDeClientes(FintechPooDbContext context)
        {
            _context = context;
        }

        //Método para Buscar Cliente por CPF (Utilitário)
        public Cliente BuscarClientePorCPF(string cpf)
        {
            return _context.Clientes
                           .Include(c => c.Contas)
                           .FirstOrDefault(c => c.CPF == cpf);
        }

        public IEnumerable<object> ObterListaFormatadaDeContas()
        {
            return _context.Clientes.Include(c => c.Contas)
                .SelectMany(cliente => cliente.Contas.Select(conta => new
                {
                    conta.Numero,
                    cliente.Nome,
                    cliente.CPF,
                    conta.Saldo,
                    Tipo = conta is ContaCorrente ? "Corrente" : "Poupança"
                })).ToList();
        }
    }
}
