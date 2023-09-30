using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Collections.ObjectModel;
using web_journal.Repositories;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_journal.ViewModels
{
    class TeacherSchedulePageViewModel : ViewModelBase
    {
        private StudentRepository studentRepository;
        private TeacherRepository teacherRepository;
        private int currentSemester;
        private List<int> comboBoxSemValues;
        private string currentDay;
        private List<string> comboBoxDayValues;
        private ObservableCollection<Schedule> scheduleSubjects;
        private ObservableCollection<Subject> teacherSubjects;
        private Subject selectedSubject;
        private ObservableCollection<Class> teacherClasses;
        private Class selectedClass;
        public ICommand ChangeSemDayCommand { get; }
        public ICommand ScheduleCommand { get; }
        public int CurrentSemester
        {
            get => currentSemester;
            set { currentSemester = value; OnPropertyChanged(nameof(currentSemester)); }
        }

        public List<int> ComboBoxSemValues
        {
            get => comboBoxSemValues;
            set { comboBoxSemValues = value; OnPropertyChanged(nameof(comboBoxSemValues)); }
        }

        public string CurrentDay
        {
            get => currentDay;
            set { currentDay = value; OnPropertyChanged(nameof(currentDay)); }
        }
        public List<string> ComboBoxDayValues
        {
            get => comboBoxDayValues;
            set { comboBoxDayValues = value; OnPropertyChanged(nameof(comboBoxDayValues)); }
        }

        public ObservableCollection<Schedule> ScheduleSubjects
        {
            get => scheduleSubjects;
            set { scheduleSubjects = value; OnPropertyChanged(nameof(scheduleSubjects)); }
        }

        public ObservableCollection<Subject> TeacherSubjects 
        { 
            get => teacherSubjects;
            set { teacherSubjects = value; OnPropertyChanged(nameof(teacherSubjects)); } 
        }

        public Subject SelectedSubject 
        { 
            get => selectedSubject;
            set { selectedSubject = value; OnPropertyChanged(nameof(selectedSubject)); } 
        }

        public ObservableCollection<Class> TeacherClasses 
        { 
            get => teacherClasses;
            set { teacherClasses = value; OnPropertyChanged(nameof(teacherClasses)); } 
        }
        public Class SelectedClass 
        { 
            get => selectedClass;
            set { selectedClass = value; OnPropertyChanged(nameof(selectedClass)); } 
        }

        public TeacherSchedulePageViewModel()
        {
            studentRepository = new StudentRepository();
            teacherRepository = new TeacherRepository();
            ComboBoxSemValues = new List<int> { 1, 2, 3, 4 };
            ComboBoxDayValues = new List<string> { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС" };
            ChangeSemDayCommand = new ViewModelCommand(ExecuteChangeSemDayCommand, CanExecuteChangeSemDayCommand);
            ScheduleCommand = new ViewModelCommand(ExecuteScheduleCommand, CanExecuteScheduleCommand);
            CurrentSemester = 1;
            CurrentDay = "ПН";
            LoadScheduleSubjectsData();
            LoadTeacherSubjects();
            LoadTeacherClasses();
        }

        private void ExecuteScheduleCommand(object obj)
        {
            teacherRepository.AddScheduleByTeacher(SelectedSubject, SelectedClass, ComboBoxDayValues.IndexOf(CurrentDay) + 1, CurrentSemester);
            LoadScheduleSubjectsData();
        }

        private bool CanExecuteScheduleCommand(object obj)
        {
            if (!TeacherSubjects.Contains(SelectedSubject) || !TeacherClasses.Contains(SelectedClass))
                return false;
            return true;
        }

        private void ExecuteChangeSemDayCommand(object obj)
        {
            LoadScheduleSubjectsData();
        }

        private bool CanExecuteChangeSemDayCommand(object obj)
        {
            return true;
        }

        public void LoadScheduleSubjectsData()
        {
            ScheduleSubjects = new ObservableCollection<Schedule>(studentRepository.FindTeacherSchedulesByTeacher(CurrentSemester, ComboBoxDayValues.IndexOf(CurrentDay) + 1));
        }

        private void LoadTeacherSubjects()
        {
            TeacherSubjects = new ObservableCollection<Subject>(teacherRepository.FindTeacherSubjects());
        }
        private void LoadTeacherClasses()
        {
            TeacherClasses = new ObservableCollection<Class>(teacherRepository.FindTeacherClasses());
        }
    }
}
