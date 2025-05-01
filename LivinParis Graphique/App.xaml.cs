using System.Windows;

namespace LivinParis_Graphique
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var login = new MVVM.View.LoginView
            {
                DataContext = new MVVM.ViewModel.LoginViewModel()
            };
            login.Show();
        }
    }
}