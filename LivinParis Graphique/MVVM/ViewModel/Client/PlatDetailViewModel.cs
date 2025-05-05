using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.Model;
using LivinParis_Graphique.MVVM.View;
using MetroHelper;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class PlatDetailViewModel : BaseViewModel
    {
        private PlatDetailView _platDetailView;
        public string Cuisinier { get; set; }
        public string Prix { get; set; }
        public string Recette { get; set; }
        public ObservableCollection<CommentaireViewDisplay> Commentaires { get; set; }
        public string Origine { get; set; }
        public string Regime { get; set; }
        public string Apports { get; set; }
        public string Type {get;set;}
        public string Temps {get;set;}
        public Personne User { get; set; }

        public PlatDetailViewModel(PlatToExplore platToExplore, Personne currentUser)
        {
            Cuisinier = platToExplore.Cuisinier;
            User = currentUser;
            Prix = platToExplore.Prix;
            Recette = platToExplore.Recette;
            Origine = platToExplore.RecetteEntiere.RecetteOrigine;
            Regime = platToExplore.RecetteEntiere.RecetteRegimeAlimentaire;
            Apports = platToExplore.RecetteEntiere.RecetteApportNutritifs;
            Type = platToExplore.RecetteEntiere.RecetteTypeDePlat;
            Cuisinier cooker = new CuisinierDataAccess().GetByUsername(Cuisinier);
            Personne personne = new PersonneDataAccess().GetByEmail(cooker.PersonneEmail);
            Temps = CalculerTemps(personne);
            _platDetailView = new PlatDetailView();
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

                GrandeStation grandeDepart = grandes[User.PersonneStationDeMetroLaPlusProche];
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