using System.Windows.Input;
using LivinParis_Graphique.Core;

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

        public ICommand CreateRecipeCommand { get; }

        public CreerRecetteViewModel()
        {
            CreateRecipeCommand = new RelayCommand(o => ExecuteCreateRecipe());
            _recipeName = string.Empty;
            _ingredients = string.Empty;
            _steps = string.Empty;
        }

        private void ExecuteCreateRecipe()
        {
            RecipeName = string.Empty;
            Ingredients = string.Empty;
            Steps = string.Empty;
        }
    }
}