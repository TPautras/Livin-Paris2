using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class GestionPersonnesViewModel : BaseViewModel
    {
        private ObservableCollection<Personne> _personnes;
        public ObservableCollection<Personne> Personnes
        {
            get => _personnes;
            set
            {
                _personnes = value;
                OnPropertyChanged();
            }
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
                FilterPersonnes();
            }
        }

        private Personne _selectedPersonne;
        public Personne SelectedPersonne
        {
            get => _selectedPersonne;
            set
            {
                _selectedPersonne = value;
                OnPropertyChanged();
                if (_selectedPersonne != null)
                {
                    ShowPersonneDetailsCommand.Execute(null);
                }
            }
        }

        private BaseViewModel _currentDetailView;
        public BaseViewModel CurrentDetailView
        {
            get => _currentDetailView;
            set
            {
                _currentDetailView = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand { get; }
        public ICommand ShowPersonneDetailsCommand { get; }

        private readonly PersonneDataAccess _personneDataAccess;
        private ObservableCollection<Personne> _allPersonnes;

        public GestionPersonnesViewModel()
        {
            _personneDataAccess = new PersonneDataAccess();
            RefreshCommand = new RelayCommand(o => LoadPersonnes());
            ShowPersonneDetailsCommand = new RelayCommand(o => ShowPersonneDetails());
            
            LoadPersonnes();
        }

        private void LoadPersonnes()
        {
            _allPersonnes = new ObservableCollection<Personne>(_personneDataAccess.GetAll());
            Personnes = new ObservableCollection<Personne>(_allPersonnes);
        }

        private void FilterPersonnes()
        {
            if (string.IsNullOrEmpty(SearchQuery))
            {
                Personnes = new ObservableCollection<Personne>(_allPersonnes);
                return;
            }

            var filtered = _allPersonnes.Where(p => 
                p.PersonneNom.ToLower().Contains(SearchQuery.ToLower()) || 
                p.PersonnePrenom.ToLower().Contains(SearchQuery.ToLower()) ||
                p.PersonneEmail.ToLower().Contains(SearchQuery.ToLower()));
            
            Personnes = new ObservableCollection<Personne>(filtered);
        }

        private void ShowPersonneDetails()
        {
            if (SelectedPersonne != null)
            {
                CurrentDetailView = new PersonneDetailsViewModel(SelectedPersonne, UserRole.Client, true);
            }
        }

        public void RefreshData()
        {
            LoadPersonnes();
        }
    }
}