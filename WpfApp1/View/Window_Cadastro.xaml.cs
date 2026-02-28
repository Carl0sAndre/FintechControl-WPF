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
using System.Windows.Shapes;
using WpfApp1.Controller;
using WpfApp1.Persistence;

namespace WpfApp1
{
    /// <summary>
    /// Lógica interna para Window_Cadastro.xaml
    /// </summary>
    public partial class Window_Cadastro : Window
    {
        private readonly FintechPooDbContext _context;
        private readonly GerenciadorDeContas _gerenciadorContas;
        public Window_Cadastro(FintechPooDbContext context)
        {
            InitializeComponent();
            _context = context;
            // Instancia o Controller (Aplicação de SOLID/Clean Code)
            _gerenciadorContas = new GerenciadorDeContas(context);
            // _gerenciadorClientes = new GerenciadorDeClientes(context);
        }

        private void botaoContaCorrente_Click(object sender, RoutedEventArgs e)
        {
            // 1. Validação básica de entrada de dados
            if (string.IsNullOrWhiteSpace(textBoxNome.Text) || string.IsNullOrWhiteSpace(textBoxCPF.Text))
            {
                MessageBox.Show("Nome e CPF são obrigatórios.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // 2. Cria o objeto Cliente
                var novoCliente = new Cliente
                {
                    Nome = textBoxNome.Text,
                    CPF = textBoxCPF.Text,
                    Endereco = textBoxEndereco.Text,
                };

                // 3. Cria o objeto Conta Corrente (herdado de Conta)
                var contaCorrente = new ContaCorrente
                {
                    Saldo = 0.00m, // Saldo inicial
                    LimiteCredito = 500.00m // Exemplo de limite inicial
                };

                // 4. Chamada ao Controller: Persiste Cliente e Conta
                // O Controller é responsável por gerar o número sequencial e salvar.
                _gerenciadorContas.CriarConta(novoCliente, contaCorrente);

                // 5. Sucesso e Feedback
                MessageBox.Show($"Conta Corrente criada com sucesso!\nNúmero: {contaCorrente.Numero}",
                                "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                // Limpar os campos da interface
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao criar a conta: {ex.Message}", "Erro de Persistência", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void botaoPoupanca_Click(object sender, RoutedEventArgs e)
        {
            // 1. Validação básica de entrada de dados
            if (string.IsNullOrWhiteSpace(textBoxNome.Text) || string.IsNullOrWhiteSpace(textBoxCPF.Text))
            {
                MessageBox.Show("Nome e CPF são obrigatórios.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // 2. Cria o objeto Cliente
                var novoCliente = new Cliente
                {
                    Nome = textBoxNome.Text,
                    CPF = textBoxCPF.Text,
                    Endereco = textBoxEndereco.Text,
                };

                // 3. Cria o objeto Conta Poupanca
                var contaPoupanca = new Poupanca
                {
                    Saldo = 0.00m, // Saldo inicial
                    TaxaJuros = 0.005m // Exemplo de taxa de juros (0.5% - 5 casas decimais se configurado no EF Core)
                };

                // 4. Chamada ao Controller: Persiste Cliente e Conta
                // O GerenciadorDeContas (Controller) usa o princípio de Liskov (LSP) 
                // para aceitar a Poupança, já que ela herda de Conta.
                _gerenciadorContas.CriarConta(novoCliente, contaPoupanca);

                // 5. Sucesso e Feedback
                MessageBox.Show($"Conta Poupança criada com sucesso!\nNúmero: {contaPoupanca.Numero}",
                                "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao criar a conta: {ex.Message}", "Erro de Persistência", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        // Método auxiliar (Clean Code)
        private void LimparCampos()
        {
            textBoxNome.Clear();
            textBoxBanco.Clear();
            textBoxCPF.Clear();
            textBoxEndereco.Clear();
        }

        private void botaoVoltar_Click(object sender, RoutedEventArgs e)
        {
            // 1. Cria uma nova instância da janela principal
            MainWindow mainWindow = new MainWindow(_context);

            // 2. Exibe a nova janela principal
            mainWindow.Show();

            // 3. Fecha a janela de cadastro atual
            this.Close();
        }
    }
    
}
