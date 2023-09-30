using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using web_journal.Repositories;

namespace web_journal.ViewModels
{
    class StudentSchedulePageViewModel : ViewModelBase
    {
        private StudentRepository studentRepository;
        private int selectedSemester;
        private List<int> semesterValues;
        private string selectedDay;
        private List<string> dayValues;
        private ObservableCollection<KeyValuePair<int, Subject>> scheduleSubjects;
        public ICommand ChangeSemDayCommand { get; }
        public int SelectedSemester
        {
            get => selectedSemester;
            set { selectedSemester = value; OnPropertyChanged(nameof(selectedSemester)); }
        }

        public List<int> SemesterValues
        {
            get => semesterValues;
            set { semesterValues = value; OnPropertyChanged(nameof(semesterValues)); }
        }

        public string SelectedDay
        {
            get => selectedDay;
            set { selectedDay = value; OnPropertyChanged(nameof(selectedDay)); }
        }
        public List<string> DayValues 
        { 
            get => dayValues;
            set { dayValues = value; OnPropertyChanged(nameof(dayValues)); } 
        }

        public ObservableCollection<KeyValuePair<int, Subject>> ScheduleSubjects
        {
            get => scheduleSubjects;
            set { scheduleSubjects = value; OnPropertyChanged(nameof(scheduleSubjects)); }
        }

        public StudentSchedulePageViewModel()
        {
            studentRepository = new StudentRepository();
            SemesterValues = new List<int> { 1, 2, 3, 4 };
            DayValues = new List<string> { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС" };
            ChangeSemDayCommand = new ViewModelCommand(ExecuteChangeSemDayCommand, CanExecuteChangeSemDayCommand);
            SelectedSemester = 1;
            selectedDay = "ПН";
            LoadScheduleSubjects();
        }

        private void ExecuteChangeSemDayCommand(object obj)
        {
            LoadScheduleSubjects();
        }

        private bool CanExecuteChangeSemDayCommand(object obj)
        {
            return true;
        }

        public void LoadScheduleSubjects()
        {
            ScheduleSubjects = new ObservableCollection<KeyValuePair<int, Subject>>();
            List<Subject> subjects = studentRepository.FindStudentScheduleSubjects(SelectedSemester, dayValues.IndexOf(SelectedDay) + 1);
            for (int i = 0; i < subjects.Count(); i++)
            {
                ScheduleSubjects.Add(new KeyValuePair<int, Subject>(i + 1, subjects[i]));
            }
        }
    }
}
