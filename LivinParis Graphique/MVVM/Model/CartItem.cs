using System.ComponentModel;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.Model
{
    public class CartItem : INotifyPropertyChanged
    {
        private int _quantity = 1;

        public CartItem(string recette, string prix, Cuisinier getByUsername)
        {
            RecetteName = recette;
            Prix = prix;
            _quantity = 1;
            Cuisinier = getByUsername.CuisinierUsername;
        }

        public CartItem()
        {
            
        }

        public string RecetteName { get; set; }
        public string Prix { get; set; }
        public string Cuisinier { get; set; }

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}