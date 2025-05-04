using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class CommandeDetailsViewModel : BaseViewModel
    {
        public string ClientUsername { get; set; }
        public string CuisinierUsername { get; set; }
        public string EntrepriseIdString { get; set; }
        public DateTime DateCreation { get; set; }

        public bool IsNewCommande { get; private set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public CommandeDetailsViewModel()
        {
            IsNewCommande = true;
            DateCreation = DateTime.Now;
            ClientUsername = string.Empty;
            CuisinierUsername = string.Empty;
            EntrepriseIdString = string.Empty;
            
            SaveCommand = new RelayCommand(o => SaveCommande());
            CancelCommand = new RelayCommand(o => CloseWindow());
        }

        public CommandeDetailsViewModel(Commande commande)
        {
            IsNewCommande = false;
            if (commande != null) {
                
                ClientUsername = commande.ClientUsername;
                CuisinierUsername = commande.CuisinierUsername;
                EntrepriseIdString = commande.EntrepriseId.HasValue ? commande.EntrepriseId.Value.ToString() : string.Empty;
                DateCreation = commande.DateCreation;
            }
            else {
                ClientUsername = CuisinierUsername = EntrepriseIdString = string.Empty;
                DateCreation = DateTime.Now;
            }

            SaveCommand = new RelayCommand(o => SaveCommande());
            CancelCommand = new RelayCommand(o => CloseWindow());
        }

        private void SaveCommande()
        {
            try {
                if (IsNewCommande) {
                    var nouvelleCommande = new Commande {
                        ClientUsername = ClientUsername,
                        CuisinierUsername = CuisinierUsername,
                        DateCreation = DateCreation, 
                        EntrepriseId = !string.IsNullOrEmpty(EntrepriseIdString) 
                                        ? (int?)Convert.ToInt32(EntrepriseIdString) 
                                        : null
                    };
                    new CommandeDataAccess().Insert(nouvelleCommande);
                    MessageBox.Show("Nouvelle commande ajoutée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                } 
                else {
                    MessageBox.Show("Consultation terminée.", "Informations", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                }
                CloseWindow();
            }
            catch (Exception ex) {
                MessageBox.Show($"Erreur lors de l'enregistrement de la commande : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseWindow()
        {
            try {
                Application.Current.Windows
                          .OfType<Window>()
                          .SingleOrDefault(w => w.DataContext == this)
                          ?.Close();
            } catch {
            }
        }
    }
    
}
