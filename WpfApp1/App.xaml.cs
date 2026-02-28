using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Windows;
using WpfApp1.Persistence;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var options = new DbContextOptionsBuilder<FintechPooDbContext>().Options;

            var context = new FintechPooDbContext(options);

            var mainWindow = new MainWindow(context);
            mainWindow.Show();
        }
    }

}
