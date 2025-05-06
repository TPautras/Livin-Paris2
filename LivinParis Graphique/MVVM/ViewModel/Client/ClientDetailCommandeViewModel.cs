using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using MetroHelper;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class ClientDetailCommandeViewModel : BaseViewModel
    {
        public string Prix { get; set; }
        public string Temps { get; set; }
        public ObservableCollection<string> Plats { get; set; }
        public string ImageSource { get; set; }
        public string Cuisinier { get; set; }
        public ObservableCollection<string> Commentaires { get; set; }
        public Commande Commande { get; set; }
        public CommandesViewModel CommandesViewModel { get; set; }
        public ICommand AddReviewCommand { get; set; }
        public ClientDetailCommandeViewModel(Commande commande, CommandesViewModel commandesViewModel)
        {
            Commande = commande;
            CommandesViewModel = commandesViewModel;
            double prix = 0;
            var tmp = new CreationDataAccess().GetAll().Where(x => x.CommandeId == Commande.CommandeId).ToList();
            var tmp2 = tmp.Select(x => x.PlatId).ToList();
            List<int> platsIds = new List<int>();
            foreach (var platId in tmp2)
            {
                platsIds.Add(new PlatDataAccess().GetById(platId).RecetteId);
            }
            foreach (var platId in tmp2)
            {
                prix += Convert.ToDouble(new PlatDataAccess().GetById(platId).PlatPrix,CultureInfo.InvariantCulture);
            }
            Prix = prix.ToString()+" \u20ac";
            Temps = CalculerTemps(new PersonneDataAccess().GetByEmail(new CuisinierDataAccess()
                .GetByUsername(commande.CuisinierUsername).PersonneEmail));
            Plats = new ObservableCollection<string>(
                new RecetteDataAccess().GetAll().Where(x=> platsIds.Contains(x.RecetteId)).Select(x=>x.RecetteNom).ToList()
                );
            Cuisinier = Commande.CuisinierUsername;
            Commentaires = new ObservableCollection<string>(
                new EvaluationDataAccess().GetCommentsByCuisinierUsername(Commande.CuisinierUsername)
                );
        }
        public string CalculerTemps(Personne personneArrivee)
        {
            string res = "";
            string solutionRoot = TrouverRacineProjet("MetroHelper");
            string dataPath = Path.Combine(solutionRoot, "MetroHelper", "Data");
            try
            {
                var peuplement = new PeuplementGrandeStation();
                var grandes = peuplement.CreerGrandesStations(dataPath);

                GrandeStation grandeDepart = grandes[CommandesViewModel.ClientViewModel.CurrentUser.PersonneStationDeMetroLaPlusProche];
                GrandeStation grandeArrivee = grandes[personneArrivee.PersonneStationDeMetroLaPlusProche];

                List<Station_de_metro> chemin;
                int temps;
                int nbCorrespondances;
                (chemin, temps, nbCorrespondances) = OutilsMetroHelper.ParcoursDijkstra(grandeDepart, grandeArrivee, dataPath);
                res = temps.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Une erreur est survenue : {ex.Message}");
                Console.WriteLine("📌 Détail : " + ex.StackTrace);
            }

            return res;
        }
        public static string TrouverRacineProjet(string dossierCible)
        {
            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            while (!Directory.Exists(Path.Combine(dir.FullName, dossierCible)))
            {
                if (dir.Parent == null)
                    throw new DirectoryNotFoundException($"Impossible de trouver le dossier '{dossierCible}' dans la hiérarchie.");
                dir = dir.Parent;
            }

            return dir.FullName;
        }
    }
}