using System;
using System.Threading;
using System.Windows.Input;
using web_journal.Repositories;
using System.Security.Principal;

namespace web_journal.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string login;
        private string password;
        private string role;
        private string errorMessage;
        private bool isViewVisible;

        private UserRepository userRepository;

        public string Login
        {
            get => login;
            set { login = value; OnPropertyChanged(nameof(login)); }
        }
        public string Password
        {
            get => password;
            set { password = value; OnPropertyChanged(nameof(password)); }
        }
        public string Role
        {
            get => role;
            set { role = value; OnPropertyChanged(nameof(role)); }
        }
        public string ErrorMessage
        {
            get => errorMessage;
            set { errorMessage = value; OnPropertyChanged(nameof(errorMessage)); }
        }

        public bool IsViewVisible
        {
            get => isViewVisible;
            set { isViewVisible = value; OnPropertyChanged(nameof(isViewVisible)); } 
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }

        private void ExecuteLoginCommand(object obj)
        {
            if (userRepository.AuthentificateUser(login, password, role))
            {
                string[] roles = { role };
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(login), roles);
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "Некорректный логин или пароль";
            }
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            if (role != null)
                return true;
            return false;
        }
    }
}
