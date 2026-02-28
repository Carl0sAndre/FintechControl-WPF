using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Persistence;


namespace WpfApp1.Controller
{
    internal class GerenciadorDeContas
    {
        // Aplicação de SOLID: Injeção de Dependência para o DbContext
        private readonly FintechPooDbContext _context;

        public GerenciadorDeContas(FintechPooDbContext context)
        {
            _context = context;
        }

        // 1. MÉTODO DE GERAÇÃO DE NÚMERO (Lógica de Negócio)
        public string GerarNovoNumeroConta()
        {
            // Consulta o banco para encontrar o maior ContaId e adiciona 1.
            // Isso garante que o número da conta seja sequencial (e único, se usado como tal).

            int proximoId = _context.Contas.Any()
                ? _context.Contas.Max(c => c.ContaId) + 1:1;

            // Formata o número com 3 dígitos (ex: 1 -> "001", 10 -> "010")
            return proximoId.ToString("D3");
        }

        // 2. MÉTODO DE CRIAÇÃO (Persistência)
        // Recebe o objeto Cliente e o objeto Conta (que pode ser ContaCorrente ou Poupanca - LSP)
        public void CriarConta(Cliente cliente, Conta novaConta)
        {
            // 1. Atribui o número sequencial antes de salvar
            novaConta.Numero = GerarNovoNumeroConta();
             
            cliente.Contas.Add(novaConta);
            // 2. Adiciona o Cliente.
            // O EF Core (graças à relação Cliente 1:N Contas) adiciona a Conta junto.
            _context.Clientes.Add(cliente);

            // 3. Persiste no banco de dados
            _context.SaveChanges();
        }

        // 3. MÉTODO DE BUSCA (Utilitário)
        public Conta BuscarContaPorNumero(string numeroConta)
        {
            // Busca a conta e usa Include para carregar o objeto Cliente relacionado, se necessário.
            return _context.Contas
                           .Include(c => c.Cliente)
                           .FirstOrDefault(c => c.Numero == numeroConta);
        }

        // 4. MÉTODO DE EXCLUSÃO
        public bool ExcluirConta(string numeroConta)
        {
            var contaParaExcluir = BuscarContaPorNumero(numeroConta);

            if (contaParaExcluir == null)
            {
                return false; // Conta não encontrada
            }

            // Remove a conta. O EF Core fará a remoção do registro no banco.
            _context.Contas.Remove(contaParaExcluir);

            _context.SaveChanges();
            return true;
        }

        public void AplicarRendimentosPoupanca()
        {
            // O Controller instancia uma poupança temporária apenas para acessar a regra de negócio
            // Ou seja, ele delega a lógica SQL para a classe Poupanca, como você queria.
            var poupancaService = new Poupanca();

            // Aqui o Controller passa o _context (que só ele tem) para o Model
            poupancaService.AplicarRendimento(_context);
        }
    }
}
