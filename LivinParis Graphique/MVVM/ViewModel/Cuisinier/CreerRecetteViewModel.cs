using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using LivinParis_Graphique.Core;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class CreerRecetteViewModel : BaseViewModel
    {
        private string _recipeName;
        public string RecipeName
        {
            get => _recipeName;
            set
            {
                _recipeName = value;
                OnPropertyChanged();
            }
        }

        private string _ingredients;
        public string Ingredients
        {
            get => _ingredients;
            set
            {
                _ingredients = value;
                OnPropertyChanged();
            }
        }

        private string _steps;
        public string Steps
        {
            get => _steps;
            set
            {
                _steps = value;
                OnPropertyChanged();
            }
        }

        private string _type;

        public string Type
        {
            get => _type;
            set{ _type = value; OnPropertyChanged(); }
        }
        public ObservableCollection<string> AvailableTypes { get; } = new ObservableCollection<string>
        {
            "Entree",
            "Plat",
            "Dessert"
        };

        public ICommand CreateRecipeCommand { get; }
        public CookViewModel CVM { get; set; }

        public CreerRecetteViewModel(CookViewModel cookViewModel)
        {
            CreateRecipeCommand = new RelayCommand(o => ExecuteCreateRecipe());
            CVM = cookViewModel;
            _recipeName = string.Empty;
            _ingredients = string.Empty;
            _steps = string.Empty;
        }

        private void ExecuteCreateRecipe()
        {
            Recette recette = new Recette
            {
                RecetteApportNutritifs = "nuls",
                RecetteId = new RecetteDataAccess().GetAll().Count+1,
                RecetteOrigine = Steps,
                RecetteNom = RecipeName,
                RecetteRegimeAlimentaire = "Omnivore",
                RecetteTypeDePlat = Type,
                
            }; 
            new RecetteDataAccess().Insert(recette);
            CVM.AjouterPlatViewModel.ReloadDishes();
            RecipeName = string.Empty;
            Ingredients = string.Empty;
            Steps = string.Empty;
        }
    }
}