using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using web_journal.Repositories;
using System.Windows.Input;
using System.Windows.Data;
using System.ComponentModel;

namespace web_journal.ViewModels
{
    class AdminTeacherPageViewModel : ViewModelBase
    {
        private TeacherRepository teacherRepository;
        private ObservableCollection<Teacher> teachers;
        private ObservableCollection<Subject> subjects;
        private string newLogin;
        private string newPassword;
        private string newFirstName;
        private string newMiddleName;
        private string newLastName;
        private string newSubjectName;
        private string newSubjectDescription;
        private string errorTeacherMessage;
        private string errorSubjectMessage;
        private Teacher selectedTeacher;
        private Subject selectedSubject;
        public ICommand TeacherAddCommand { get; }
        public ICommand SubjectAddCommand { get; }
        public ICommand TeacherSujectAddCommand { get; }

        public ObservableCollection<Teacher> Teachers
        {
            get => teachers;
            set { teachers = value; OnPropertyChanged(nameof(teachers)); }
        }

        public string NewLogin
        {
            get => newLogin;
            set { newLogin = value; OnPropertyChanged(nameof(newLogin)); }
        }
        public string NewPassword
        {
            get => newPassword;
            set { newPassword = value; OnPropertyChanged(nameof(newPassword)); }
        }
        public string NewFirstName
        {
            get => newFirstName;
            set { newFirstName = value; OnPropertyChanged(nameof(newFirstName)); }
        }
        public string NewMiddleName
        {
            get => newMiddleName;
            set
            {
                newMiddleName = value;
                OnPropertyChanged(nameof(newMiddleName));
            }
        }
        public string NewLastName
        {
            get => newLastName;
            set { newLastName = value; OnPropertyChanged(nameof(newLastName)); }
        }

        public ObservableCollection<Subject> Subjects 
        { 
            get => subjects;
            set { subjects = value; OnPropertyChanged(nameof(subjects)); } 
        }

        public string NewSubjectName 
        { 
            get => newSubjectName;
            set { newSubjectName = value; OnPropertyChanged(nameof(newSubjectName)); } 
        }
        public string NewSubjectDescription 
        { 
            get => newSubjectDescription;
            set { newSubjectDescription = value; OnPropertyChanged(nameof(newSubjectDescription)); } 
        }

        public Teacher SelectedTeacher 
        { 
            get => selectedTeacher; 
            set { selectedTeacher = value; OnPropertyChanged(nameof(selectedTeacher)); }
        }
        public Subject SelectedSubject 
        { 
            get => selectedSubject;
            set { selectedSubject = value; OnPropertyChanged(nameof(selectedSubject)); } 
        }

        public string ErrorTeacherMessage 
        { 
            get => errorTeacherMessage;
            set { errorTeacherMessage = value; OnPropertyChanged(nameof(errorTeacherMessage)); } 
        }

        public string ErrorSubjectMessage 
        { 
            get => errorSubjectMessage;
            set { errorSubjectMessage = value; OnPropertyChanged(nameof(errorSubjectMessage)); } 
        }

        public AdminTeacherPageViewModel()
        {
            teacherRepository = new TeacherRepository();
            TeacherAddCommand = new ViewModelCommand(ExecuteTeacherAddCommand, CanExecuteTeacherAddCommand);
            SubjectAddCommand = new ViewModelCommand(ExecuteSubjectAddCommand, CanExecuteSubjectAddCommand);
            TeacherSujectAddCommand = new ViewModelCommand(ExecuteTeacherSubjectAddCommand, CanExecuteTeacherSubjectAddCommand);
            LoadTeachers();
            LoadSubjects();
        }

        private void ExecuteTeacherSubjectAddCommand(object obj)
        {
            teacherRepository.AddSubjectToTeacher(SelectedTeacher.UserId, SelectedSubject.Id);
        }

        private bool CanExecuteTeacherSubjectAddCommand(object obj)
        {
            return true;
        }

        private void ExecuteSubjectAddCommand(object obj)
        {
            if (teacherRepository.FindSubjectByName(NewSubjectName) == null)
            {
                teacherRepository.AddSubject(NewSubjectName, newSubjectDescription);
                LoadSubjects();
            }
            else
            {
                ErrorSubjectMessage = "Такой предмет существует! Введите другие данные!";
            }
        }

        private bool CanExecuteSubjectAddCommand(object obj)
        {
            return true;
        }

        private void ExecuteTeacherAddCommand(object obj)
        {
            if (teacherRepository.FindUserByLogin(NewLogin) == null)
            {
                if (newPassword != null)
                {
                    teacherRepository.AddTeacher(NewLogin, NewPassword, NewFirstName, NewMiddleName, NewLastName);
                    LoadTeachers();
                }
                else
                {
                    ErrorTeacherMessage = "Пароль пустой! Введите другие данные!";
                }
            }
            else
            {
                ErrorTeacherMessage = "Пользователь с таким логином существует! Введите другие данные!";
            }
            
        }

        private bool CanExecuteTeacherAddCommand(object obj)
        {
            return true;
        }

        private void LoadTeachers()
        {
            Teachers = new ObservableCollection<Teacher>(teacherRepository.FindAllTeachers());
        }

        private void LoadSubjects()
        {
            Subjects = new ObservableCollection<Subject>(teacherRepository.FindAllSubjects());
        }
    }
}
