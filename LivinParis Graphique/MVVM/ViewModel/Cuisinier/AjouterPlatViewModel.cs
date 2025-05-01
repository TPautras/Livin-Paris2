using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class AjouterPlatViewModel : BaseViewModel
    {
        private Personne User { get; set; }
        public List<string> AvailableDishes { get; }= new RecetteDataAccess().GetAll().Select(q => q.RecetteNom).ToList();
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

        public ICommand AddDishCommand { get; }

        public AjouterPlatViewModel(Personne personne)
        {
            User = personne;
            AddDishCommand = new RelayCommand(o => ExecuteAddDish());
            _newDishName = string.Empty;
            _newDishPrice = string.Empty;
        }

        private void ExecuteAddDish()
        {
            Console.WriteLine(User.PersonneEmail);
            Console.WriteLine(NewDishName);
            Console.WriteLine(NewDishPrice);
            NewDishName = string.Empty;
            NewDishPrice = string.Empty;
        }
    }
}