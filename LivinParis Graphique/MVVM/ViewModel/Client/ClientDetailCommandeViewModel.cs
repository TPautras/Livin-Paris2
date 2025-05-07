using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Graphs;
using LivinParis_Console;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.View;
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
        private ImageSource _imageSource;
        public ImageSource ImageSource {
            get => _imageSource;
            set { _imageSource = value; OnPropertyChanged(); }
        }

        public string Cuisinier { get; set; }
        public ObservableCollection<string> Commentaires { get; set; }
        public Commande Commande { get; set; }
        public CommandesViewModel CommandesViewModel { get; set; }
        public ICommand AddReviewCommand { get; set; }
        private Uri _imageUri;
        public Uri ImageUri {
            get => _imageUri;
            set { _imageUri = value; OnPropertyChanged(nameof(ImageUri)); }
        }
        public ClientDetailCommandeViewModel(Commande commande, CommandesViewModel commandesViewModel)
        {
            AddReviewCommand = new RelayCommand(o => AddReviewExecute());
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
            LoadImage(new PersonneDataAccess().GetByEmail(new CuisinierDataAccess()
                .GetByUsername(commande.CuisinierUsername).PersonneEmail));
        }

        public void AddReviewExecute()
        {
            ReviewView rv = new ReviewView { DataContext = new ReviewViewModel(this) };
            rv.ShowDialog();
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
        public void LoadImage(Personne personneArrivee)
        {
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

                var createur = new CreateGraphMetro();
                var graphe = createur.ChargerReseauDepuisFichiers(dataPath);

                string imagePath = Path.Combine(solutionRoot, "chemin.png");
                AffichageMetro.AfficherChemin(graphe, chemin, grandes, false);
                
                if (File.Exists(imagePath))
                {
                    var bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.UriSource = new Uri(imagePath, UriKind.Absolute);
                    bmp.EndInit();

                    ImageSource = bmp;
                }
                else
                {
                    MessageBox.Show($"Image introuvable : {imagePath}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Une erreur est survenue : {ex.Message}");
            }
        }
    }
}