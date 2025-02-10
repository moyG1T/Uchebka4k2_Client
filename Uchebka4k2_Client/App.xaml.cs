using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Uchebka4k2_Client
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = _provider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
