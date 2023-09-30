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
    class TeacherMarksPageViewModel : ViewModelBase
    {
        private StudentRepository studentRepository;
        private UserRepository userRepository;
        
        private string filterText;
        private ObservableCollection<Student> students;
        private List<string> searchValues;
        private CollectionViewSource StudentCollection;
        private string selectedFilter;
        
        private Student selectedStudent;
        private KeyValuePair<int,Subject> selectedSubject;

        private ObservableCollection<KeyValuePair<string, List<StudentMark>>> subjectMarks;
        private int selectedSemester;
        private List<int> semesterValues;

        private int selectedScheduleSemester;
        private List<int> semSchedValues;
        private string selectedDay;
        private List<string> dayValues;
        private ObservableCollection<KeyValuePair<int, Subject>> scheduleSubjects;

        private int selectedMark;
        private List<int> markValues;
        public ICommand ChangeSemesterCommand { get; }
        public ICommand ChangeSemDayCommand { get; }
        public ICommand MarkCommand { get; }


        public TeacherMarksPageViewModel()
        {
            SearchValues = new List<string> { "Логин", "Фамилия", "Класс"};
            SelectedFilter = searchValues[0];
            studentRepository = new StudentRepository();
            userRepository = new UserRepository();
            students = new ObservableCollection<Student>(studentRepository.FindStudentsByTeacher(userRepository.FindUserByLogin(Thread.CurrentPrincipal.Identity.Name).Id));
            StudentCollection = new CollectionViewSource();
            StudentCollection.Source = students;
            StudentCollection.Filter += studentsCollectionFilter;
            SelectedSemester = 1;
            SemesterValues = new List<int> { 1, 2, 3, 4 };
            SelectedScheduleSemester = 1;
            SelectedDay = "ПН";
            SelectedMark = 1;
            SemSchedValues = new List<int> { 1, 2, 3, 4 };
            DayValues = new List<string> { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС" };
            MarkValues = new List<int> { 1, 2, 3, 4, 5 };
            ChangeSemesterCommand = new ViewModelCommand(ExecuteChangeSemesterCommand, CanExecuteChangeSemesterCommand);
            ChangeSemDayCommand = new ViewModelCommand(ExecuteChangeSemDayCommand, CanExecuteChangeSemDayCommand);
            MarkCommand = new ViewModelCommand(ExecuteMarkCommand, CanExecuteMarkCommand);
        }

        public int SelectedScheduleSemester
        {
            get => selectedScheduleSemester;
            set { selectedScheduleSemester = value; OnPropertyChanged(nameof(selectedScheduleSemester)); }
        }

        public List<int> SemSchedValues
        {
            get => semSchedValues; set { semSchedValues = value; }
        }

        public string SelectedDay
        {
            get => selectedDay;
            set { selectedDay = value; OnPropertyChanged(nameof(selectedDay)); }
        }
        public List<string> DayValues
        {
            get => dayValues; set { dayValues = value; }
        }

        public ObservableCollection<KeyValuePair<int, Subject>> ScheduleSubjects
        {
            get => scheduleSubjects;
            set { scheduleSubjects = value; OnPropertyChanged(nameof(scheduleSubjects)); }
        }

        public ICollectionView SourceCollection
        {
            get { return this.StudentCollection.View; }
        }

        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                this.StudentCollection.View.Refresh();
                OnPropertyChanged(nameof(filterText));
            }
        }

        public List<string> SearchValues 
        { 
            get => searchValues; set { searchValues = value; }
        }

        public string SelectedFilter 
        { 
            get => selectedFilter;
            set { selectedFilter = value; OnPropertyChanged(nameof(selectedFilter)); }
        }

        public Student SelectedStudent
        {
            get => selectedStudent;
            set
            {
                selectedStudent = value;
                
                ChangeStudent();
                OnPropertyChanged(nameof(selectedStudent));
            }
        }
        public ObservableCollection<KeyValuePair<string, List<StudentMark>>> SubjectMarks
        {
            get => subjectMarks;
            set { subjectMarks = value; OnPropertyChanged(nameof(subjectMarks)); }
        }

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

        public KeyValuePair<int, Subject> SelectedSubject 
        {
            get => selectedSubject;
            set { selectedSubject = value; OnPropertyChanged(nameof(selectedSubject)); } 
        }

        public int SelectedMark 
        { 
            get => selectedMark;
            set { selectedMark = value; OnPropertyChanged(nameof(selectedMark)); } 
        }
        public List<int> MarkValues 
        { 
            get => markValues;
            set { markValues = value; OnPropertyChanged(nameof(markValues)); } 
        }

        private void ExecuteMarkCommand(object obj)
        {
            studentRepository.AddStudentMark(SelectedStudent.UserId, SelectedMark, studentRepository.FindStudentScheduleId(SelectedSubject.Value.Id, SelectedStudent.ClassId, DayValues.IndexOf(SelectedDay) + 1, SelectedScheduleSemester).Id);
            LoadSubjectsMarks();
        }

        private bool CanExecuteMarkCommand(object obj)
        {
            if (SourceCollection != null)
            {
                if (!SourceCollection.Contains(SelectedStudent))
                {
                    return false;
                }
            }
            if (ScheduleSubjects != null)
            {
                if (!ScheduleSubjects.Contains(SelectedSubject))
                {
                    return false;
                }
            }
            return true;
        }

        private void ExecuteChangeSemesterCommand(object obj)
        {
            LoadSubjectsMarks();
        }

        private bool CanExecuteChangeSemesterCommand(object obj)
        {
            if (SelectedStudent != null)
                return true;
            return false;
        }

        private void ExecuteChangeSemDayCommand(object obj)
        {
            LoadScheduleSubjects();
        }

        private bool CanExecuteChangeSemDayCommand(object obj)
        {
            if (SelectedStudent != null)
                return true;
            return false;
        }

        private void LoadSubjectsMarks()
        {
            Student currentStudent = selectedStudent;
            List<Subject> subjects = studentRepository.FindStudentSubjectsByClass(currentStudent.ClassId, SelectedSemester);
            SubjectMarks = new ObservableCollection<KeyValuePair<string, List<StudentMark>>> { };
            foreach (Subject subject in subjects)
            {
                SubjectMarks.Add(new KeyValuePair<string, List<StudentMark>>(subject.SubjectName, studentRepository.FindStudentMarksByStudent(subject.Id, currentStudent.ClassId, SelectedSemester)));
            }
        }

        public void LoadScheduleSubjects()
        {
            Student currentStudent = selectedStudent;
            ScheduleSubjects = new ObservableCollection<KeyValuePair<int, Subject>>();
            List<Subject> subjects = studentRepository.FindStudentSubjectsByTeacher(currentStudent.ClassId, SelectedScheduleSemester, DayValues.IndexOf(SelectedDay) + 1, userRepository.FindUserByLogin(Thread.CurrentPrincipal.Identity.Name).Id);
            for (int i = 0; i < subjects.Count(); i++)
            {
                ScheduleSubjects.Add(new KeyValuePair<int, Subject>(i + 1, subjects[i]));
            }
        }

        private void ChangeStudent()
        {
            if (SelectedStudent != null)
            {
                LoadSubjectsMarks();
                LoadScheduleSubjects();
            }
        }

        void studentsCollectionFilter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }
            Student stud = e.Item as Student;
            User user = userRepository.FindUserById(stud.UserId);
            if (SelectedFilter == "Логин" && user.Login.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else if (SelectedFilter == "Фамилия" && stud.LastName.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else if (SelectedFilter == "Класс" && stud.ClassId.ToString().ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

    }
}
