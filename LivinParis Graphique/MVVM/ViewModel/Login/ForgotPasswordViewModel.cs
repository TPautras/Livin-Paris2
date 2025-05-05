using LivinParis_Graphique.Core;
using LivinParis_Graphique.MVVM.View;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        private ForgotPasswordView _forgotPasswordView;

        public ForgotPasswordViewModel()
        {
            _forgotPasswordView = new ForgotPasswordView();
        }
    }
}