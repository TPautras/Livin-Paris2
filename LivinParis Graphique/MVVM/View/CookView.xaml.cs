using System;
using System.Windows;
using LivinParis_Graphique.MVVM.ViewModel;

namespace LivinParis_Graphique.MVVM.View
{
    public partial class CookView : Window
    {
        public CookView()
        {
            DataContext = new LivinParis_Graphique.MVVM.ViewModel.CookViewModel();
            InitializeComponent();
        }
    }
}
