using System;
using System.Windows;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.View;
using SqlConnector.DataAccess;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        private ForgotPasswordView _forgotPasswordView;
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged("Email"); }
        }
        
        public ICommand SendEmailCommand { get; }
        

        public ForgotPasswordViewModel()
        {
            _forgotPasswordView = new ForgotPasswordView();
            SendEmailCommand = new RelayCommand(_ => ExecuteSendEmail());
        }

        private void ExecuteSendEmail()
        {
            Console.WriteLine("test");
            if (string.IsNullOrWhiteSpace(Email))
            {
                MessageBox.Show("Veuillez entrer votre email.");
                return;
            }

            var personneDal = new PersonneDataAccess();
            var personne = personneDal.GetByEmail(Email);
            if (personne == null)
            {
                MessageBox.Show("Aucun utilisateur trouvé avec cet email.");
                return;
            }

            var password = personneDal.GetClientPasswordByEmail(Email);
            if (password == null)
            {
                MessageBox.Show("Mot de passe introuvable.");
                return;
            }

            string Code = SqlConnector.Services.Mailer.SendPasswordReminder(Email, password);
            MessageBox.Show("Votre code de reinitialisation a été envoyé par mail.");
            Window newWindow = new MVVM.View.PasswordReminderView{DataContext = new PasswordReminderViewModel(Code, password)};
            Application.Current.MainWindow?.Close();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
        }
    }
}