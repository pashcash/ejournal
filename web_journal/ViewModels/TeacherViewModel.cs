using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;

namespace web_journal.ViewModels
{
    class TeacherViewModel : ViewModelBase
    {
        private Page welcomePage;
        private Page teacherInfoPage;
        private Page teacherMarksPage;
        private Page teacherSchedulePage;
        private Page currentPage;

        public Page CurrentPage
        {
            get => currentPage;
            set { currentPage = value; OnPropertyChanged(nameof(currentPage)); }
        }
        public ICommand ToInfoPageCommand { get; }
        public ICommand ToMarksPageCommand { get; }
        public ICommand ToSchedulePageCommand { get; }
        public TeacherViewModel()
        {
            welcomePage = new Pages.WelcomePage();
            teacherInfoPage = new Pages.TeacherInfoPage();
            teacherMarksPage = new Pages.TeacherMarksPage();
            teacherSchedulePage = new Pages.TeacherSchedulePage();
            CurrentPage = welcomePage;
            ToInfoPageCommand = new ViewModelCommand(ExecuteToInfoPageCommand, CanExecuteToInfoPageCommand);
            ToMarksPageCommand = new ViewModelCommand(ExecuteToMarksPageCommand, CanExecuteToMarksPageCommand);
            ToSchedulePageCommand = new ViewModelCommand(ExecuteToSchedulePageCommand, CanExecuteToSchedulePageCommand);
        }
        private void ExecuteToInfoPageCommand(object obj)
        {
            CurrentPage = teacherInfoPage;
        }
        private bool CanExecuteToInfoPageCommand(object obj)
        {
            return true;
        }
        private void ExecuteToMarksPageCommand(object obj)
        {
            CurrentPage = teacherMarksPage;
        }
        private bool CanExecuteToMarksPageCommand(object obj)
        {
            return true;
        }
        private void ExecuteToSchedulePageCommand(object obj)
        {
            CurrentPage = teacherSchedulePage;
        }

        private bool CanExecuteToSchedulePageCommand(object obj)
        {
            return true;
        }

    }
}
