using System.Windows;

namespace LivinParis_Graphique.MVVM.View
{
    public partial class ClientView : Window
    {
        public ClientView()
        {
            InitializeComponent();
            // Le DataContext est assigné depuis LoginViewModel lors de l'ouverture de la fenêtre.
            // On pourrait tout de même gérer ici s'il fallait initialiser sans passage de paramètre.
        }
    }
}