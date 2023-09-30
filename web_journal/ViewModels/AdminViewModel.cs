using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;

namespace web_journal.ViewModels
{
    class AdminViewModel : ViewModelBase
    {
        private Page welcomePage;
        private Page studentPage;
        private Page teacherPage;
        private Page currentPage;

        public Page CurrentPage
        {
            get => currentPage;
            set { currentPage = value; OnPropertyChanged(nameof(currentPage)); }
        }
        public ICommand ToStudentPageCommand { get; }
        public ICommand ToTeacherPageCommand { get; }
        public AdminViewModel()
        {
            welcomePage = new Pages.WelcomePage();
            studentPage = new Pages.AdminStudentPage();
            teacherPage = new Pages.AdminTeacherPage();
            CurrentPage = welcomePage;
            ToStudentPageCommand = new ViewModelCommand(ExecuteToStudentPageCommand, CanExecuteToStudentPageCommand);
            ToTeacherPageCommand = new ViewModelCommand(ExecuteToTeacherPageCommand, CanExecuteToTeacherPageCommand);
        }

        private void ExecuteToTeacherPageCommand(object obj)
        {
            CurrentPage = teacherPage;
        }

        private bool CanExecuteToTeacherPageCommand(object obj)
        {
            return true;
        }

        private void ExecuteToStudentPageCommand(object obj)
        {
            CurrentPage = studentPage;
        }

        private bool CanExecuteToStudentPageCommand(object obj)
        {
            return true;
        }
    }
}
