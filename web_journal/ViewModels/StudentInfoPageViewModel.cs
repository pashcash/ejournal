using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using web_journal.Repositories;

namespace web_journal.ViewModels
{
    class StudentInfoPageViewModel : ViewModelBase
    {
        private Student currentStudent;
        private StudentRepository studentRepository;

        public Student CurrentStudent
        {
            get => currentStudent;
            set { currentStudent = value; OnPropertyChanged(nameof(currentStudent)); }
        }

        public StudentInfoPageViewModel()
        {
            studentRepository = new StudentRepository();
            CurrentStudent = studentRepository.FindStudentByLogin(Thread.CurrentPrincipal.Identity.Name);
        }
    }
}
