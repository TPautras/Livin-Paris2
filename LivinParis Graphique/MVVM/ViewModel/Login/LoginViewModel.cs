using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.View;
using SqlConnector.DataAccess;
using SqlConnector.DataService;
using SqlConnector.Models;
using CryptingUtils;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private static readonly byte[] EncryptionKey = Crypter.GenerateKey("LivinParisSecretKey2025");
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password { get; set; } = string.Empty;

        private UserRole _selectedRole;
        public UserRole SelectedRole
        {
            get => _selectedRole;
            set { _selectedRole = value; OnPropertyChanged(); }
        }

        public UserRole[] Roles => new UserRole[] { UserRole.Client, UserRole.Cook, UserRole.Company };

        public ICommand LoginCommand { get; }
        public ICommand ForgotPasswordCommand { get; }
        public ICommand CreateNewUserCommand { get; }

        public LoginViewModel()
        {
            CreateNewUserCommand = new RelayCommand(o => ExecuteCreateNewUser());
            LoginCommand = new RelayCommand(_ => ExecuteLogin());
            ForgotPasswordCommand = new RelayCommand(_ => ExecuteForgotPassword());
        }

        private void ExecuteLogin()
        {
            Console.WriteLine("test");
            Personne? user = null;
            string password = string.Empty;
            switch (SelectedRole)
            {
                case UserRole.Client:
                    var clientDal = new ClientDataAccess();
                    var personneDalClient = new PersonneDataAccess();
                    var client = clientDal.GetByUsername(_username);
                    user = personneDalClient.GetByEmail(client.PersonneEmail);
                    password = client.ClientPassword;
                    break;
                case UserRole.Cook:
                    var cookDal = new CuisinierDataAccess();
                    var personneDalCook = new PersonneDataAccess();
                    var cook = cookDal.GetByUsername(_username);
                    user = personneDalCook.GetByEmail(cook.PersonneEmail);
                    password = cook.CuisinierPassword;
                    break;
                case UserRole.Company:
                    var companyDal = new EntrepriseDataAccess();
                    user = companyDal.GetByUsername(_username);
                    break;
            }
            Console.WriteLine(password);
            if (user != null && password == Password)
            {
                Console.WriteLine(Password);
                Console.WriteLine(password);
                if (user.PersonneIsAdmin == true)
                {
                    OpenAdminView(user);
                }
                else
                {
                    OpenRoleView(user, SelectedRole);
                }
            }
        }
        
        private void ExecuteForgotPassword()
        {
            var forgotPasswordViewModel = new ForgotPasswordViewModel();
            var newWindow = new ForgotPasswordView{DataContext = forgotPasswordViewModel};
            Application.Current.MainWindow = newWindow;
            newWindow.ShowDialog();
        }
        private void ExecuteCreateNewUser()
        {
            var createNewUserViewModel = new CreateUserViewModel();
            var newWindow = new CreateUserView(){DataContext = createNewUserViewModel};
            Application.Current.MainWindow = newWindow;
            newWindow.ShowDialog();
        }


        private void OpenAdminView(Personne user)
        {
            var adminVM = new AdminViewModel(user);
            var adminWindow = new AdminView{ DataContext = adminVM };
            adminWindow.DataContext = adminVM;
            adminWindow.Show();
            Application.Current.MainWindow?.Close();
            Application.Current.MainWindow = adminWindow;
        }

        private void OpenRoleView(Personne user, UserRole role)
        {
            Window? newWindow = null;
            switch (role)
            {
                case UserRole.Client:
                    newWindow = new ClientView { DataContext = new ClientViewModel(user, Username )};
                    break;
                case UserRole.Cook:
                    newWindow = new CookView { DataContext = new CookViewModel(user) };
                    break;
                case UserRole.Company:
                    newWindow = new CompanyView { DataContext = new CompanyViewModel(user) };
                    break;
            }

            if (newWindow != null)
            {
                newWindow.Show();
                Application.Current.MainWindow?.Close();
                Application.Current.MainWindow = newWindow;
            }
            else
            {
                MessageBox.Show("Échec de l'ouverture de la nouvelle fenêtre. Veuillez vérifier le rôle sélectionné.");
            }
        }

        private void CloseLoginWindow()
        {
            var current = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.IsActive);

            current?.Close();
        }
    }
}
