using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_journal.Repositories;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Input;

namespace web_journal.ViewModels
{
    class StudentViewModel : ViewModelBase
    {
        private Page welcomePage;
        private Page studentInfoPage;
        private Page studentMarksPage;
        private Page studentSchedulePage;
        private Page currentPage;

        public ICommand ToInfoPageCommand { get; }
        public ICommand ToMarksPageCommand { get; }
        public ICommand ToSchedulePageCommand { get; }
        public Page CurrentPage 
        { 
            get => currentPage; 
            set { currentPage = value; OnPropertyChanged(nameof(currentPage)); } 
        }

        public StudentViewModel()
        {
            welcomePage = new Pages.WelcomePage();
            studentInfoPage = new Pages.StudentInfoPage();
            studentMarksPage = new Pages.StudentMarksPage();
            studentSchedulePage = new Pages.StudentSchedulePage();
            CurrentPage = welcomePage;
            ToInfoPageCommand = new ViewModelCommand(ExecuteToInfoPageCommand, CanExecuteToInfoPageCommand);
            ToMarksPageCommand = new ViewModelCommand(ExecuteToMarksPageCommand, CanExecuteToMarksPageCommand);
            ToSchedulePageCommand = new ViewModelCommand(ExecuteToSchedulePageCommand, CanExecuteToSchedulePageCommand);
        }

        private void ExecuteToSchedulePageCommand(object obj)
        {
            CurrentPage = studentSchedulePage;
        }

        private bool CanExecuteToSchedulePageCommand(object obj)
        {
            return true;
        }

        private void ExecuteToMarksPageCommand(object obj)
        {
            CurrentPage = studentMarksPage;
        }
        private bool CanExecuteToMarksPageCommand(object obj)
        {
            return true;
        }

        private void ExecuteToInfoPageCommand(object obj)
        {
            CurrentPage = studentInfoPage;
        }
        private bool CanExecuteToInfoPageCommand(object obj)
        {
            return true;
        }
    }
}
