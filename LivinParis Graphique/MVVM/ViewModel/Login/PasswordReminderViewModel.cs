using LivinParis_Graphique.Core;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class PasswordReminderViewModel : BaseViewModel
    {
        public string Password { get; set; }
        private string _passwordConfirmation = "";
        private string _inputCode = "";
        public string Code {get; set;}

        public string InputCode
        {
            get { return _inputCode; }
            set
            {
                if (value == Code)
                {
                    PasswordConfirmation = "Votre mot de passe est : "+Password;
                }
                _inputCode = value;
                OnPropertyChanged();
            }
        }

        public string PasswordConfirmation
        {
            get { return _passwordConfirmation; }
            set
            {
                _passwordConfirmation = value;
                OnPropertyChanged();
            }
        }

        public PasswordReminderViewModel(string code, string password)
        {
            Code = code;
            Password = password;
        }
    }
}