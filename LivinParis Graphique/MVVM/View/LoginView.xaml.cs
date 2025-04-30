using System.Windows;
using LivinParis_Graphique.MVVM.ViewModel;

namespace LivinParis_Graphique.MVVM.View
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LivinParis_Graphique.MVVM.ViewModel.LoginViewModel();
            // Liaison spéciale du PasswordBox : on peut abonner à PasswordChanged pour mettre à jour le VM
            PwdBox.PasswordChanged += (s, e) => 
            {
                if (DataContext is LoginViewModel vm)
                    vm.Password = PwdBox.Password;
            };
        }
    }
}