using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class PersonneDetailsViewModel : BaseViewModel
    {
        #region Properties
        
        private string _nom;
        public string Nom
        {
            get => _nom;
            set
            {
                _nom = value;
                OnPropertyChanged();
            }
        }
        
        private string _prenom;
        public string Prenom
        {
            get => _prenom;
            set
            {
                _prenom = value;
                OnPropertyChanged();
            }
        }
        
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        
        private string _telephone;
        public string Telephone
        {
            get => _telephone;
            set
            {
                _telephone = value;
                OnPropertyChanged();
            }
        }
        
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
        
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                // Ne pas appeler OnPropertyChanged() pour le mot de passe
            }
        }
        
        private bool _isAdmin;
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnPropertyChanged();
            }
        }
        
        private AddressViewModel _adresse;
        public AddressViewModel Adresse
        {
            get => _adresse;
            set
            {
                _adresse = value;
                OnPropertyChanged();
            }
        }
        
        private PreferencesViewModel _preferences;
        public PreferencesViewModel Preferences
        {
            get => _preferences;
            set
            {
                _preferences = value;
                OnPropertyChanged();
            }
        }
        
        private bool _isNewUser;
        public bool IsNewUser
        {
            get => _isNewUser;
            set
            {
                _isNewUser = value;
                OnPropertyChanged();
            }
        }
        
        private bool _isAdminMode;
        public bool IsAdminMode
        {
            get => _isAdminMode;
            set
            {
                _isAdminMode = value;
                OnPropertyChanged();
            }
        }
        
        private UserRole _selectedRole;
        public UserRole SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged();
            }
        }
        
        public List<UserRole> AvailableRoles { get; } = new List<UserRole>
        {
            UserRole.Client,
            UserRole.Cook,
            UserRole.Company
        };
        
        #endregion
        
        #region Commands
        
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        
        #endregion
        
        #region Constructors
        
        /// <summary>
        /// Constructeur pour créer un nouvel utilisateur
        /// </summary>
        public PersonneDetailsViewModel(bool isAdminMode = false)
        {
            InitializeEmpty();
            IsNewUser = true;
            IsAdminMode = isAdminMode;
            
            SaveCommand = new RelayCommand(o => SaveNewUser());
            CancelCommand = new RelayCommand(o => Close());
        }
        
        /// <summary>
        /// Constructeur pour éditer un utilisateur existant
        /// </summary>
        public PersonneDetailsViewModel(Personne personne, UserRole role, bool isAdminMode = false)
        {
            LoadFromPerson(personne, role);
            IsNewUser = false;
            IsAdminMode = isAdminMode;
            
            SaveCommand = new RelayCommand(o => UpdateUser());
            CancelCommand = new RelayCommand(o => Close());
        }
        
        #endregion
        
        #region Methods
        
        private void InitializeEmpty()
        {
            Nom = string.Empty;
            Prenom = string.Empty;
            Email = string.Empty;
            Telephone = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            IsAdmin = false;
            SelectedRole = UserRole.Client;
            
            Adresse = new AddressViewModel();
            Preferences = new PreferencesViewModel();
        }
        
        private void LoadFromPerson(Personne personne, UserRole role)
        {
            if (personne == null)
            {
                InitializeEmpty();
                return;
            }
            
            Nom = personne.PersonneNom;
            Prenom = personne.PersonnePrenom;
            Email = personne.PersonneEmail;
            Telephone = personne.PersonneTelephone;
            IsAdmin = personne.PersonneIsAdmin ?? false;
            SelectedRole = role;
            
            Adresse = new AddressViewModel
            {
                Rue = personne.PersonneNomDeLaRue,
                CodePostal = personne.PersonneCodePostale.ToString(),
                Ville = personne.PersonneVille,
            };
            
            Preferences = new PreferencesViewModel
            {
                EnableNotifications = true,
                SubscribeNewsletter = false
            };
            
            switch (role)
            {
                case UserRole.Client:
                    var client = new ClientDataAccess().GetByEmail(personne.PersonneEmail);
                    if (client != null)
                    {
                        Username = client.ClientUsername;
                        Password = client.ClientPassword; 
                    }
                    break;
                case UserRole.Cook:
                    var cuisinier = new CuisinierDataAccess().GetByEmail(personne.PersonneEmail);
                    if (cuisinier != null)
                    {
                        Username = cuisinier.CuisinierUsername;
                        Password = cuisinier.CuisinierPassword; 
                    }
                    break;
                case UserRole.Company:
                    // Traitement spécifique pour les entreprises si nécessaire
                    break;
            }
        }
        
        private void SaveNewUser()
        {
            try
            {
                var personne = new Personne
                {
                    PersonneNom = Nom,
                    PersonnePrenom = Prenom,
                    PersonneEmail = Email,
                    PersonneTelephone = Telephone,
                    PersonneNomDeLaRue = Adresse.Rue,
                    PersonneCodePostale = Convert.ToInt32(Adresse.CodePostal),
                    PersonneVille = Adresse.Ville,
                    PersonneIsAdmin = IsAdmin
                };
                
                // Enregistrer la personne
                new PersonneDataAccess().Insert(personne);
                
                // Enregistrer selon le rôle
                switch (SelectedRole)
                {
                    case UserRole.Client:
                        var client = new Client
                        {
                            ClientUsername = Username,
                            ClientPassword = Password,
                            PersonneEmail = Email
                        };
                        new ClientDataAccess().Insert(client);
                        break;
                    
                    case UserRole.Cook:
                        var cuisinier = new Cuisinier
                        {
                            CuisinierUsername = Username,
                            CuisinierPassword = Password,
                            PersonneEmail = Email
                        };
                        new CuisinierDataAccess().Insert(cuisinier);
                        break;
                    
                    case UserRole.Company:
                        // Traitement spécifique pour les entreprises si nécessaire
                        break;
                }
                
                MessageBox.Show("Utilisateur créé avec succès!", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la création de l'utilisateur: {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void UpdateUser()
        {
            try
            {
                // Mettre à jour la personne
                var personneDAL = new PersonneDataAccess();
                var personne = personneDAL.GetByEmail(Email);
                
                if (personne != null)
                {
                    personne.PersonneNom = Nom;
                    personne.PersonnePrenom = Prenom;
                    personne.PersonneTelephone = Telephone;
                    personne.PersonneNomDeLaRue = Adresse.Rue;
                    personne.PersonneCodePostale = Convert.ToInt32(Adresse.CodePostal);
                    personne.PersonneVille = Adresse.Ville;
                    personne.PersonneIsAdmin = IsAdmin;
                    
                    personneDAL.Update(personne);
                    
                    // Mettre à jour selon le rôle
                    switch (SelectedRole)
                    {
                        case UserRole.Client:
                            var clientDAL = new ClientDataAccess();
                            var client = clientDAL.GetByEmail(Email);
                            if (client != null && !string.IsNullOrEmpty(Password))
                            {
                                client.ClientPassword = Password;
                                clientDAL.Update(client);
                            }
                            break;
                        
                        case UserRole.Cook:
                            var cuisinierDAL = new CuisinierDataAccess();
                            var cuisinier = cuisinierDAL.GetByEmail(Email);
                            if (cuisinier != null && !string.IsNullOrEmpty(Password))
                            {
                                cuisinier.CuisinierPassword = Password;
                                cuisinierDAL.Update(cuisinier);
                            }
                            break;
                        
                        case UserRole.Company:
                            // Traitement spécifique pour les entreprises si nécessaire
                            break;
                    }
                    
                    MessageBox.Show("Utilisateur mis à jour avec succès!", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour de l'utilisateur: {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void Close()
        {
            // Cette méthode pourrait être remplacée par un événement que la vue écouterait
            // pour fermer la fenêtre ou naviger à une autre vue
        }
        
        #endregion
    }
    
    /// <summary>
    /// ViewModel pour l'adresse
    /// </summary>
    public class AddressViewModel : BaseViewModel
    {
        private string _rue;
        public string Rue
        {
            get => _rue;
            set
            {
                _rue = value;
                OnPropertyChanged();
            }
        }
        
        private string _codePostal;
        public string CodePostal
        {
            get => _codePostal;
            set
            {
                _codePostal = value;
                OnPropertyChanged();
            }
        }
        
        private string _ville;
        public string Ville
        {
            get => _ville;
            set
            {
                _ville = value;
                OnPropertyChanged();
            }
        }
        
        private string _pays;
        public string Pays
        {
            get => _pays;
            set
            {
                _pays = value;
                OnPropertyChanged();
            }
        }
        
        public AddressViewModel()
        {
            Rue = string.Empty;
            CodePostal = string.Empty;
            Ville = string.Empty;
            Pays = "France"; // Valeur par défaut
        }
    }
    
    /// <summary>
    /// ViewModel pour les préférences
    /// </summary>
    public class PreferencesViewModel : BaseViewModel
    {
        private bool _enableNotifications;
        public bool EnableNotifications
        {
            get => _enableNotifications;
            set
            {
                _enableNotifications = value;
                OnPropertyChanged();
            }
        }
        
        private bool _subscribeNewsletter;
        public bool SubscribeNewsletter
        {
            get => _subscribeNewsletter;
            set
            {
                _subscribeNewsletter = value;
                OnPropertyChanged();
            }
        }
        
        public PreferencesViewModel()
        {
            EnableNotifications = true;
            SubscribeNewsletter = false;
        }
    }
}