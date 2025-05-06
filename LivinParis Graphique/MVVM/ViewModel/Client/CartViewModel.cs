using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.Model;
using LivinParis_Graphique.MVVM.View;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class CartViewModel : BaseViewModel
    {
        private CartView _cartView;
        private ObservableCollection<CartItem> _items;
        public ClientViewModel ClientViewModel { get; set; }
        public ICommand PushCartCommand { get; set; }
        public ObservableCollection<CartItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public CartViewModel(ObservableCollection<CartItem> items, ClientViewModel clientVM)
        {
            ClientViewModel = clientVM;
            _cartView = new CartView();
            this.Items = items;
            PushCartCommand = new RelayCommand(o=> PushCart());
        }

        public void PushCart()
        {
            ClientViewModel.PutCart();
        }
    }
}