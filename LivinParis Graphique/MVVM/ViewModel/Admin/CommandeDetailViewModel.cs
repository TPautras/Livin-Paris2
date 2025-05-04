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

        // Constructeur pour nouvelle commande
        public CommandeDetailsViewModel()
        {
            IsNewCommande = true;
            // Initialiser la date de création à aujourd'hui
            DateCreation = DateTime.Now;
            // Les autres champs restent vides (à remplir par l’utilisateur)
            ClientUsername = string.Empty;
            CuisinierUsername = string.Empty;
            EntrepriseIdString = string.Empty;
            
            SaveCommand = new RelayCommand(o => SaveCommande());
            CancelCommand = new RelayCommand(o => CloseWindow());
        }

        // Constructeur pour afficher/éditer une commande existante
        public CommandeDetailsViewModel(Commande commande)
        {
            IsNewCommande = false;
            if (commande != null) {
                // Initialiser les propriétés à partir de la commande existante
                ClientUsername = commande.ClientUsername;
                CuisinierUsername = commande.CuisinierUsername;
                EntrepriseIdString = commande.EntrepriseId.HasValue ? commande.EntrepriseId.Value.ToString() : string.Empty;
                DateCreation = commande.DateCreation;
            }
            else {
                // Si par précaution commande est null, initialiser vide
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
                    // Création d'une nouvelle Commande à partir des champs saisis
                    var nouvelleCommande = new Commande {
                        ClientUsername = ClientUsername,
                        CuisinierUsername = CuisinierUsername,
                        DateCreation = DateCreation, // normalement DateCreation = Now
                        // EntrepriseId est optionnelle
                        EntrepriseId = !string.IsNullOrEmpty(EntrepriseIdString) 
                                        ? (int?)Convert.ToInt32(EntrepriseIdString) 
                                        : null
                    };
                    // Insérer la nouvelle commande via DataAccess/Service
                    new CommandeDataAccess().Insert(nouvelleCommande);
                    MessageBox.Show("Nouvelle commande ajoutée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                } 
                else {
                    // Ici, on pourrait implémenter la mise à jour d'une commande existante si besoin.
                    // Par exemple, récupérer la commande par son ID et mettre à jour certains champs.
                    // Si aucune modification n'est à enregistrer (consultation seule), on peut simplement fermer.
                    MessageBox.Show("Consultation terminée.", "Informations", MessageBoxButton.OK, MessageBoxImage.Information);
                    // (On pourrait omettre le MessageBox pour juste fermer dans le cas consultation)
                }
                CloseWindow();
            }
            catch (Exception ex) {
                MessageBox.Show($"Erreur lors de l'enregistrement de la commande : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseWindow()
        {
            // Fermer la fenêtre de détails.
            // Ici on peut lever un événement ou utiliser une astuce pour fermer la Window.
            // Par exemple, si la vue utilise ShowDialog(), on peut simplement faire:
            // (La vue pourrait aussi s'abonner à un événement de fermeture.)
            try {
                Application.Current.Windows
                          .OfType<Window>()
                          .SingleOrDefault(w => w.DataContext == this)
                          ?.Close();
            } catch {
                /* No-op si non trouvé */
            }
        }
    }
    
}
