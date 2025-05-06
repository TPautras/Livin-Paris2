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
        public ICommand AddItemCommand { get; set; }
        public ICommand RemoveItemCommand { get; set; }
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
            PushCartCommand = new RelayCommand(o => PushCart());
            AddItemCommand = new RelayCommand(AddItemToCart);
            RemoveItemCommand = new RelayCommand(RemoveItemFromCart);

        }

        public void PushCart()
        {
            ClientViewModel.PutCart();
        }

        public void RemoveItemFromCart(object o)
        {
            if (o is CartItem item)
            {
                var existing = Items.FirstOrDefault(i => i.RecetteName == item.RecetteName);
                if (existing != null)
                {
                    if (existing.Quantity > 1)
                        existing.Quantity--;
                    else
                        Items.Remove(existing);

                    OnPropertyChanged(nameof(Items)); // Facultatif si Quantity est observable
                }
            }
        }
        public void AddItemToCart(object o)
        {
            Console.WriteLine("isWorking");
            if (o is CartItem item)
            {
                var existing = Items.FirstOrDefault(i => i.RecetteName == item.RecetteName);
                if (existing != null)
                {
                    existing.Quantity++;
                    OnPropertyChanged(nameof(Items)); 
                }
                else
                {
                    Items.Add(new CartItem
                    {
                        RecetteName = item.RecetteName,
                        Prix = item.Prix,
                        Cuisinier = item.Cuisinier,
                        Quantity = 1
                    });
                }
            }
        }
    }
}