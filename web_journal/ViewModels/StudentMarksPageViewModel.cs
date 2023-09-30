using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;
using web_journal.Repositories;
using System.Windows.Input;

namespace web_journal.ViewModels
{
    class StudentMarksPageViewModel : ViewModelBase
    {
        private ObservableCollection<KeyValuePair<string, List<StudentMark>>> subjectMarks;
        private StudentRepository studentRepository;
        private int selectedSemester;
        private List<int> semesterValues;

        public ICommand ChangeSemesterCommand { get; }

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

        public StudentMarksPageViewModel()
        {
            studentRepository = new StudentRepository();
            SemesterValues = new List<int> { 1, 2, 3, 4 };
            ChangeSemesterCommand = new ViewModelCommand(ExecuteChangeSemesterCommand, CanExecuteChangeSemesterCommand);
            SelectedSemester = 1;
            LoadSubjectsMarks();
        }

        private void ExecuteChangeSemesterCommand(object obj)
        {
            LoadSubjectsMarks();
        }

        private bool CanExecuteChangeSemesterCommand(object obj)
        {
            return true;
        }

        private void LoadSubjectsMarks()
        {
            Student currentStudent = studentRepository.FindStudentByLogin(Thread.CurrentPrincipal.Identity.Name);
            List<Subject> subjects = studentRepository.FindStudentSubjectsByClass(currentStudent.ClassId, SelectedSemester);
            SubjectMarks = new ObservableCollection<KeyValuePair<string, List<StudentMark>>> { };
            foreach (Subject subject in subjects)
            {
                SubjectMarks.Add(new KeyValuePair<string, List<StudentMark>>(subject.SubjectName, studentRepository.FindStudentMarksByStudent(subject.Id, currentStudent.ClassId, SelectedSemester)));
            }
        }

    }
}
