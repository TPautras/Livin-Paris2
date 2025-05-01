using System;
using System.Windows.Input;
using LivinParis_Graphique.Core;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class AjouterPlatViewModel : BaseViewModel
    {
        private string _newDishName;
        public string NewDishName
        {
            get => _newDishName;
            set
            {
                _newDishName = value;
                OnPropertyChanged();
            }
        }

        private string _newDishPrice;
        public string NewDishPrice
        {
            get => _newDishPrice;
            set
            {
                _newDishPrice = value;
                OnPropertyChanged();
            }
        }

        // Commande pour ajouter le plat (implémentation factice)
        public ICommand AddDishCommand { get; }

        public AjouterPlatViewModel()
        {
            AddDishCommand = new RelayCommand(o => ExecuteAddDish());
            // Champs initialisés à vide
            _newDishName = string.Empty;
            _newDishPrice = string.Empty;
        }

        private void ExecuteAddDish()
        {
            // Action factice : vide les champs après l'ajout du plat
            Console.WriteLine(NewDishName);
            Console.WriteLine(NewDishPrice);
            NewDishName = string.Empty;
            NewDishPrice = string.Empty;
        }
    }
}