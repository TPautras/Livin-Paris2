using LivinParis_Graphique.Core;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class AutreViewModel : BaseViewModel
    {
        public ObservableCollection<CategorieAutre> Categories { get; set; }

        public ICommand OpenCategoryCommand { get; }

        public AutreViewModel()
        {
            Categories = new ObservableCollection<CategorieAutre>
            {
                new CategorieAutre("Exports", "Exporter les données dans différents formats."),
                new CategorieAutre("Parcours", "Gérer les parcours utilisateurs."),
                new CategorieAutre("Coloration", "Personnaliser les thèmes et couleurs.")
            };

            OpenCategoryCommand = new RelayCommand(OpenCategory);
        }

        private void OpenCategory(object parameter)
        {
            if (parameter is CategorieAutre cat)
            {
                MessageBox.Show($"Vous avez sélectionné : {cat.Titre}", "Catégorie Sélectionnée");
                // Ici tu peux gérer la navigation vers des vues spécifiques pour chaque catégorie
            }
        }
    }

    public class CategorieAutre
    {
        public string Titre { get; set; }
        public string Description { get; set; }

        public CategorieAutre(string titre, string description)
        {
            Titre = titre;
            Description = description;
        }
    }
}