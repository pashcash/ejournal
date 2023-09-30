using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using web_journal.Repositories;

namespace web_journal.ViewModels
{
    class TeacherInfoPageViewModel : ViewModelBase
    {
        private Teacher currentTeacher;
        private TeacherRepository studentRepository;

        public Teacher CurrentTeacher
        {
            get => currentTeacher;
            set { currentTeacher = value; OnPropertyChanged(nameof(currentTeacher)); }
        }

        public TeacherInfoPageViewModel()
        {
            studentRepository = new TeacherRepository();
            CurrentTeacher = studentRepository.FindTeacherByLogin(Thread.CurrentPrincipal.Identity.Name);
        }
    }
}
