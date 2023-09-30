using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace web_journal.ViewModels
{
    class WelcomePageViewModel : ViewModelBase
    {
        private string login;
        public string Login
        {
            get => login;
            set { login = value; OnPropertyChanged(nameof(login)); }
        }
        public WelcomePageViewModel()
        {
            Login = Thread.CurrentPrincipal.Identity.Name;
        }
    }
}
