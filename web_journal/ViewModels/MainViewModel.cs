using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using web_journal.Views;
using System.Threading;
using System.Security.Principal;
using web_journal.Repositories;
using web_journal.Pages;

namespace web_journal.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Page currentPage;

        public Page CurrentPage { get => currentPage; set { currentPage = value; OnPropertyChanged(nameof(currentPage)); } }

        public MainViewModel()
        {
            SetCurrentPage();
        }

        private void SetCurrentPage()
        {
            if (Thread.CurrentPrincipal.IsInRole("Ученик"))
            {
                CurrentPage = new StudentPage();
            }
            else if (Thread.CurrentPrincipal.IsInRole("Учитель"))
            {
                CurrentPage = new TeacherPage();
            }
            else if (Thread.CurrentPrincipal.IsInRole("Администратор"))
            {
                CurrentPage = new AdminPage();
            }
        }
    }
}
