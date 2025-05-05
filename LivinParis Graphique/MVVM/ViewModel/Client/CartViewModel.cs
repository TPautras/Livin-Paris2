using System;
using System.Collections.ObjectModel;
using System.Linq;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.Model;
using LivinParis_Graphique.MVVM.View;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class CartViewModel : BaseViewModel
    {
        private CartView _cartView;
        private ObservableCollection<CartItem> _items;

        public ObservableCollection<CartItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public CartViewModel(ObservableCollection<CartItem> items)
        {
            _cartView = new CartView();
            this.Items = items;
        }
    }
}