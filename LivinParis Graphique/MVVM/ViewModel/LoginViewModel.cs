using System;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.View;
using SqlConnector.DataAccess;      
using SqlConnector.DataService;
using SqlConnector.Models; 

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
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

        // Liste des rôles disponibles pour peupler une ComboBox ou des RadioButtons dans la vue
        public UserRole[] Roles => new UserRole[] { UserRole.Client, UserRole.Cook, UserRole.Company };

        // Commande de connexion 
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(_ => ExecuteLogin());
        }

        private void ExecuteLogin()
        {
            // Tentative de connexion en fonction du rôle sélectionné
            Personne? user = null;
            string password = string.Empty;
            switch (SelectedRole)
            {
                case UserRole.Client:
                    PersonneDataAccess personneDataAccess = new PersonneDataAccess();
                    var clientDal = new ClientDataAccess();
                    Client client = clientDal.GetByUsername(_username);
                    user = personneDataAccess.GetByEmail(client.PersonneEmail);
                    password = client.ClientPassword;
                    break;
                case UserRole.Cook:
                    var cookDal = new CuisinierDataAccess();
                    PersonneDataAccess personneDataAccess1 = new PersonneDataAccess();
                    Cuisinier cuisinier1 = cookDal.GetByUsername(_username);
                    user = personneDataAccess1.GetByEmail(cuisinier1.PersonneEmail);
                    password = cuisinier1.CuisinierPassword;
                    break;
                case UserRole.Company:
                    var companyDal = new EntrepriseDataAccess();
                    user = companyDal.GetByUsername(_username);
                    break;
            }

            if (user != null && password == Password)
            {
                if (user.PersonneIsAdmin != null && (bool)user.PersonneIsAdmin)
                {
                    OpenAdminView(user);
                }
                else
                {
                    OpenRoleView(user, SelectedRole);
                }
            }
            else 
            {
                // Gestion en cas d’échec de la connexion (message d'erreur, etc.)
                // Par exemple, exposer une propriété ErrorMessage liée à la vue.
                // ErrorMessage = "Identifiants invalides, veuillez réessayer.";
            }
        }

        // Ouvre la fenêtre administrateur
        private void OpenAdminView(Personne user)
        {
            var adminVM = new AdminViewModel(user);
            var adminWindow = new AdminView();          // Fenêtre Admin (voir AdminView.xaml plus bas)
            adminWindow.DataContext = adminVM;
            adminWindow.Show();

            CloseLoginWindow();
        }

        private void OpenRoleView(Personne user, UserRole role)
        {
            switch (role)
            {
                case UserRole.Client:
                    var clientVM = new ClientViewModel(user);
                    var clientWindow = new ClientView();
                    clientWindow.DataContext = clientVM;
                    clientWindow.Show();
                    break;
                case UserRole.Cook:
                    var cookVM = new CookViewModel(user);
                    var cookWindow = new CookView();
                    cookWindow.DataContext = cookVM;
                    cookWindow.Show();
                    break;
                case UserRole.Company:
                    var companyVM = new CompanyViewModel(user);
                    var companyWindow = new CompanyView();
                    companyWindow.DataContext = companyVM;
                    companyWindow.Show();
                    break;
            }
            CloseLoginWindow();
        }

        private void CloseLoginWindow()
        {
            System.Windows.Application.Current.MainWindow?.Close();
        }
    }
}
