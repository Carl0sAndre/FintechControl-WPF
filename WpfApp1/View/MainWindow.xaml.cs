using Microsoft.EntityFrameworkCore;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FintechPooDbContext _context;
        private readonly GerenciadorDeClientes _gerenciadorClientes;
        private readonly GerenciadorDeContas _gerenciador;
        public MainWindow(FintechPooDbContext context)
        {
            InitializeComponent();
            _context = context;
            _gerenciadorClientes = new GerenciadorDeClientes(_context);
            _gerenciador = new GerenciadorDeContas(_context);
        }

        private void botaoListar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1. Chama o Controller para obter a lista (agora como IEnumerable<object>)
                var dadosParaExibir = _gerenciadorClientes.ObterListaFormatadaDeContas();

                // 2. Vincula a lista ao DataGrid
                dataGridClientes.ItemsSource = dadosParaExibir;

                if (!dadosParaExibir.Any()) // Usa LINQ para verificar se há elementos
                {
                    MessageBox.Show("Nenhum cliente ou conta encontrado no sistema.", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao carregar os dados: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void botaoCadastro_Click(object sender, RoutedEventArgs e)
        {
            // 1. Cria a nova janela, passando o contexto.
            // O _context é a instância de FintechPooDbContext que já definimos no construtor.
            var cadastroWindow = new Window_Cadastro(_context);

            // 2. Exibe a nova janela.
            cadastroWindow.Show();

            // 3. Fecha a janela atual (MainWindow).
            this.Close();
        }

        private void botaoTransacao_Click(object sender, RoutedEventArgs e)
        {
            // 1. Cria a nova janela, passando o contexto.
            // O _context é a instância de FintechPooDbContext que já definimos no construtor.
            var transacoesWindow = new Window_Transacoes(_context);

            // 2. Exibe a nova janela.
            transacoesWindow.Show();

            // 3. Fecha a janela atual (MainWindow).
            this.Close();
        }

        private void botaoExcluir_Click(object sender, RoutedEventArgs e)
        {
            // 1. Cria a nova janela, passando o contexto.
            // O _context é a instância de FintechPooDbContext que já definimos no construtor.
            var excluirWindow = new Window_Excluir(_context);

            // 2. Exibe a nova janela.
            excluirWindow.Show();

            // 3. Fecha a janela atual (MainWindow).
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // A View só fala com o Controller (_gerenciador)
                _gerenciador.AplicarRendimentosPoupanca();

                MessageBox.Show("Rendimentos aplicados com sucesso!");

                // Opcional: Se tiver uma grid na tela, atualize ela agora
                // CarregarListagem(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao aplicar rendimentos: " + ex.Message);
            }
        }
    }
}