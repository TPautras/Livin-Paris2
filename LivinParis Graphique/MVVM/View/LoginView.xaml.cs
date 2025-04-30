using System;
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
            Console.WriteLine("Binding actif pour LoginView");
            PwdBox.PasswordChanged += (s, e) => 
            {
                if (DataContext is LoginViewModel vm)
                    vm.Password = PwdBox.Password;
            };
            Loaded += (s, e) => UsernameBox.Focus();
        }
    }
}