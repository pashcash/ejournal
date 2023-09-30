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
    class AdminStudentPageViewModel : ViewModelBase
    {
        private StudentRepository studentRepository;
        private ObservableCollection<Student> students;
        private List<int> comboBoxClasses;
        private int currentClass;
        private string newLogin;
        private string newPassword;
        private string newFirstName;
        private string newMiddleName;
        private string newLastName;
        private string errorMessage;
        public ICommand StudentAddCommand { get; }

        public ObservableCollection<Student> Students
        {
            get => students;
            set { students = value; OnPropertyChanged(nameof(students)); }
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
            set {
                newMiddleName = value;
                OnPropertyChanged(nameof(newMiddleName));
            } 
        }
        public string NewLastName 
        { 
            get => newLastName; 
            set { newLastName = value; OnPropertyChanged(nameof(newLastName)); }
        }

        public List<int> ComboBoxClasses 
        {
            get => comboBoxClasses;
            set { comboBoxClasses = value; OnPropertyChanged(nameof(comboBoxClasses)); } 
        }

        public int CurrentClass 
        { 
            get => currentClass;
            set { currentClass = value; OnPropertyChanged(nameof(currentClass)); } 
        }

        public string ErrorMessage 
        { 
            get => errorMessage;
            set { errorMessage = value; OnPropertyChanged(nameof(errorMessage)); } 
        }

        public AdminStudentPageViewModel()
        {
            studentRepository = new StudentRepository();
            ComboBoxClasses = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            CurrentClass = 1;
            StudentAddCommand = new ViewModelCommand(ExecuteStudentAddCommand, CanExecuteStudentAddCommand);
            LoadStudents();
        }

        private void ExecuteStudentAddCommand(object obj)
        {
            if (studentRepository.FindUserByLogin(NewLogin) == null)
            {
                if (newPassword != null)
                {
                    studentRepository.AddStudent(NewLogin, NewPassword, NewFirstName, NewMiddleName, NewLastName, CurrentClass);
                    LoadStudents();
                }
                else
                {
                    ErrorMessage = "Пароль пустой! Введите другие данные!";
                }
            }
            else
            {
                ErrorMessage = "Пользователь с таким логином существует! Введите другие данные!";
            }
        }

        private bool CanExecuteStudentAddCommand(object obj)
        {
            return true;
        }

        private void LoadStudents()
        {
            Students = new ObservableCollection<Student>(studentRepository.FindAllStudents());
        }
        
    }
}
