using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Controller;
using WpfApp1.Persistence;

namespace WpfApp1
{
    /// <summary>
    /// Interação lógica para Window_Transacoes.xam
    /// </summary>
    public partial class Window_Transacoes : Window
    {
        private readonly FintechPooDbContext _context;

        // Controller que lida com a lógica de negócio e persistência
        private readonly GerenciadorDeTransacoes _gerenciadorTransacoes;
        public Window_Transacoes(FintechPooDbContext context)
        {
            InitializeComponent();
            _context = context;
            _gerenciadorTransacoes = new GerenciadorDeTransacoes(context);
        }

        private void botaoDepositar_Click(object sender, RoutedEventArgs e)
        {
            // A conta de destino será a Conta A para o depósito
            string numeroConta = textBoxNumeroContaA.Text;
            string valorTexto = textBoxValor.Text;
            decimal valor;

            // 1. Validação de Entrada
            if (string.IsNullOrWhiteSpace(numeroConta) || !decimal.TryParse(valorTexto, out valor) || valor <= 0)
            {
                MessageBox.Show("Preencha o Número da Conta A e um Valor válido para depósito.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // 2. Chamada ao Controller: Executa a lógica de negócio e persistência
                bool sucesso = _gerenciadorTransacoes.ProcessarDeposito(numeroConta, valor);

                // 3. Feedback ao Usuário
                if (sucesso)
                {
                    MessageBox.Show($"Depósito de R$ {valor:F2} realizado com sucesso na conta {numeroConta}!",
                                    "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimparCampos();
                }
                else
                {
                    MessageBox.Show("Falha ao processar o depósito. A conta não foi encontrada.",
                                    "Erro de Transação", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro no sistema: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void botaoSacar_Click(object sender, RoutedEventArgs e)
        {
            // A conta de origem será a Conta A para o saque
            string numeroConta = textBoxNumeroContaA.Text;
            string valorTexto = textBoxValor.Text;
            decimal valor;

            // 1. Validação de Entrada
            if (string.IsNullOrWhiteSpace(numeroConta) || !decimal.TryParse(valorTexto, out valor) || valor <= 0)
            {
                MessageBox.Show("Preencha o Número da Conta A e um Valor válido para saque.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // 2. Chamada ao Controller: Executa a lógica de negócio (validando saldo/limite na camada Model)
                bool sucesso = _gerenciadorTransacoes.ProcessarSaque(numeroConta, valor);

                // 3. Feedback ao Usuário
                if (sucesso)
                {
                    MessageBox.Show($"Saque de R$ {valor:F2} realizado com sucesso da conta {numeroConta}.",
                                    "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimparCampos();
                }
                else
                {
                    // O Controller retorna false se a conta não for encontrada OU se o saldo/limite for insuficiente
                    MessageBox.Show("Falha ao processar o saque. Conta não encontrada ou saldo/limite insuficiente.",
                                    "Erro de Transação", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro no sistema: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void botaoTransferir_Click(object sender, RoutedEventArgs e)
        {
            string contaOrigemNum = textBoxNumeroContaA.Text;
            string contaDestinoNum = textBoxNumeroContaB.Text;
            string valorTexto = textBoxValor.Text;
            decimal valor;

            // 1. Validação de Entrada
            if (string.IsNullOrWhiteSpace(contaOrigemNum) ||
                string.IsNullOrWhiteSpace(contaDestinoNum) ||
                !decimal.TryParse(valorTexto, out valor) ||
                valor <= 0)
            {
                MessageBox.Show("Preencha as Contas A, B e um Valor válido para transferência.",
                                "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validação extra: Não permitir transferência para a mesma conta
            if (contaOrigemNum.Equals(contaDestinoNum))
            {
                MessageBox.Show("Não é possível transferir para a mesma conta.",
                                "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // 2. Chamada ao Controller: Executa a lógica (busca, valida saldo, debita, credita e salva)
                bool sucesso = _gerenciadorTransacoes.ProcessarTransferencia(contaOrigemNum, contaDestinoNum, valor);

                // 3. Feedback ao Usuário
                if (sucesso)
                {
                    MessageBox.Show($"Transferência de R$ {valor:F2} de {contaOrigemNum} para {contaDestinoNum} realizada com sucesso!",
                                    "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimparCampos();
                }
                else
                {
                    // O Controller retorna false se uma das contas não for encontrada OU se o saldo/limite for insuficiente na origem.
                    MessageBox.Show("Falha ao processar a transferência. Verifique se ambas as contas existem e se a conta de origem tem saldo/limite suficiente.",
                                    "Erro de Transação", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Captura exceções mais específicas (ex: erro de banco de dados)
                MessageBox.Show($"Ocorreu um erro no sistema: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LimparCampos()
        {
            textBoxNumeroContaA.Clear();
            textBoxNumeroContaB.Clear(); // Limpa a Conta B também
            textBoxValor.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 1. Cria uma nova instância da janela principal.
            MainWindow mainWindow = new MainWindow(_context);

            // 2. Exibe a nova janela principal.
            mainWindow.Show();

            // 3. Fecha a janela de transações atual.
            this.Close();
        }
    }
}
