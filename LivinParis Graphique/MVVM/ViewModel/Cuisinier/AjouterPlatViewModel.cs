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
        
        private string _nombrePortions;
        
        private string _dateFab;

        private string _datePer;
        
        public string NombrePortions
        { 
            get => _nombrePortions; 
            set
            {
                _nombrePortions = value;
                OnPropertyChanged();
            } 
        }
        public string DateFab
        { 
            get => _dateFab; 
            set
            {
                _dateFab = value;
                OnPropertyChanged();
            } 
        }
        public string DatcePer
        { 
            get => _datePer; 
            set
            {
                _datePer = value;
                OnPropertyChanged();
            } 
        }
        private bool _isPlatDuJour;
        public bool IsPlatDuJour
        {
            get => _isPlatDuJour;
            set
            {
                if (_isPlatDuJour != value)
                {
                    _isPlatDuJour = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<string> AvailableDishes { get; set; }= new RecetteDataAccess().GetAll().Select(q => q.RecetteNom).ToList();
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

        private VoirPlatsViewModel _voirPlatsViewModel;

        public AjouterPlatViewModel(Personne personne, VoirPlatsViewModel voirPlatsViewModel)
        {
            AvailableDishes = new RecetteDataAccess().GetAll().Select(q => q.RecetteNom).ToList();
            User = personne;
            _voirPlatsViewModel = voirPlatsViewModel;
            AddDishCommand = new RelayCommand(o => ExecuteAddDish());
        }

        public void ReloadDishes()
        {
            AvailableDishes = new RecetteDataAccess().GetAll().Select(q => q.RecetteNom).ToList();
        }
        private void ExecuteAddDish()
        {
            Cuisinier c = new CuisinierDataAccess().GetByEmail(User.PersonneEmail);
            Console.WriteLine("Adding new dish");
            Console.WriteLine($"Nom : {NewDishName}");
            Console.WriteLine($"Prix : {NewDishPrice}");
            Console.WriteLine($"Nombre : {NombrePortions}");
            Console.WriteLine($"Date : {DateFab}");
            Console.WriteLine($"Date : {DateFab}");
            Console.WriteLine($"PDJ : {IsPlatDuJour}");
            if (c != null)
            {
                Console.WriteLine($"Utilisateur : {c.CuisinierUsername}");
            }
            
            Plat plat = new Plat
            {
                CuisinierUsername = c.CuisinierUsername,
                PlatDuJour = IsPlatDuJour,
                PlatNombrePortion = Convert.ToInt32(NombrePortions),
                PlatPrix = NewDishPrice,
                PlatDateDeFabrication = Convert.ToDateTime(_dateFab),
                PlatDateDePeremption = Convert.ToDateTime(_datePer),
                RecetteId = new RecetteDataAccess().GetByName(NewDishName).RecetteId,
                PlatId = new PlatDataAccess().GetAll().Count() + 1
            };
            
            new PlatDataAccess().Insert(plat);
            
            _voirPlatsViewModel.Refresh();
            
            NewDishName = string.Empty;
            NewDishPrice = string.Empty;
            NombrePortions = string.Empty;
            DateFab = string.Empty;
            DatcePer = string.Empty;
            IsPlatDuJour = false;
        }
    }
}