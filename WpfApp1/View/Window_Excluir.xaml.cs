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
    /// Interação lógica para Window_Excluir.xam
    /// </summary>
    public partial class Window_Excluir : Window
    {
        private readonly FintechPooDbContext _context;
        private readonly GerenciadorDeContas _gerenciadorContas;
        public Window_Excluir(FintechPooDbContext context)
        {
            InitializeComponent();
            _context = context;
            // A responsabilidade de exclusão está no GerenciadorDeContas
            _gerenciadorContas = new GerenciadorDeContas(context);
        }

        private void botaoExcluir_Click(object sender, RoutedEventArgs e)
        {
            string numeroConta = textBoxNumero.Text;

            // 1. Validação de Entrada
            if (string.IsNullOrWhiteSpace(numeroConta))
            {
                MessageBox.Show("O Número da Conta é obrigatório para a exclusão.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // 2. Confirmação do Usuário
                var resultado = MessageBox.Show($"Tem certeza que deseja EXCLUIR a conta {numeroConta}?",
                                                "Confirmação de Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resultado == MessageBoxResult.Yes)
                {
                    // 3. Chamada ao Controller: Executa a exclusão e persistência
                    bool sucesso = _gerenciadorContas.ExcluirConta(numeroConta);

                    // 4. Feedback ao Usuário
                    if (sucesso)
                    {
                        MessageBox.Show($"Conta {numeroConta} excluída com sucesso!",
                                        "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        textBoxNumero.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Falha na exclusão. A conta não foi encontrada ou não existe.",
                                        "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro no sistema: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
