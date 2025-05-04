using System.Windows;
using System.Windows.Controls;

namespace LivinParis_Graphique.MVVM.View
{
    public partial class PersonneDetailsView : UserControl
    {
        public PersonneDetailsView()
        {
            InitializeComponent();
            
            this.Loaded += (s, e) =>
            {
                if (this.DataContext is ViewModel.PersonneDetailsViewModel viewModel)
                {
                    if (!string.IsNullOrEmpty(viewModel.Password))
                    {
                        PasswordBox.Password = viewModel.Password;
                    }
                    
                    PasswordBox.PasswordChanged += (sender, args) =>
                    {
                        viewModel.Password = PasswordBox.Password;
                    };
                }
            };
        }
    }
}