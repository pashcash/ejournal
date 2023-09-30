using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace web_journal.Repositories
{
    class TeacherRepository : UserRepository
    {
        //Поиск учителя по логину
        public Teacher FindTeacherByLogin(string login)
        {
            Teacher teacher;
            User user = base.FindUserByLogin(login);
            using (var db = new wpf_journalContext())
            {
                teacher = db.Teachers.Select(p => p)
                                     .Where(p => p.UserId == user.Id)
                                     .SingleOrDefault();
            }
            return teacher;
        }

        public List<Subject> FindTeacherSubjects()
        {
            long teacherId = FindTeacherByLogin(Thread.CurrentPrincipal.Identity.Name).UserId;
            List<Subject> teacherSubjects;
            using (var db = new wpf_journalContext())
            {
                teacherSubjects = db.SubjectTeachers
                                   .Select(p => p)
                                   .Where(p => p.TeacherId == teacherId)
                                   .Join(db.Subjects, p => p.SubjectId, c => c.Id, (p, c) => new Subject { Id = c.Id, SubjectName = c.SubjectName, Description = c.Description })
                                   .ToList();
            }
            return teacherSubjects;
        }

        public List<Class> FindTeacherClasses()
        {
            List<Class> teacherClasses;
            using (var db = new wpf_journalContext())
            {
                teacherClasses = db.Classes
                                   .OrderBy(p => p.ClassNumber)
                                   .ToList();
            }
            return teacherClasses;
        }

        public void AddScheduleByTeacher(Subject selectedSubject, Class selectedClass, long currentDay, long currentSemester)
        {
            using (var db = new wpf_journalContext())
            {
                db.Schedules.Add(new Schedule(selectedSubject.Id, selectedClass.Id, currentDay, currentSemester));
                db.SaveChanges();
            }
        }

        public List<Teacher> FindAllTeachers()
        {
            List<Teacher> teachers;
            using (var db = new wpf_journalContext())
            {
                teachers = db.Teachers.ToList();
            }
            return teachers;
        }

        public void AddTeacher(string login, string password, string firstName, string middleName, string lastName)
        {
            using (var db = new wpf_journalContext())
            {
                db.Users.Add(new User(login, password, "Учитель"));
                db.SaveChanges();
                User teacherUser = FindUserByLogin(login);
                db.Teachers.Add(new Teacher(teacherUser.Id, firstName, middleName, lastName));
                db.SaveChanges();
            }        
        }

        public List<Subject> FindAllSubjects()
        {
            List<Subject> subjects;
            using (var db = new wpf_journalContext())
            {
                subjects = db.Subjects.ToList();
            }
            return subjects;
        }

        public void AddSubject(string subjectName, string subjectDescription)
        {
            using (var db = new wpf_journalContext())
            {
                db.Add(new Subject(subjectName, subjectDescription));
                db.SaveChanges();
            }
        }

        public void AddSubjectToTeacher(long teacherId, long subjectId)
        {
            using (var db = new wpf_journalContext())
            {
                db.Add(new SubjectTeacher(teacherId, subjectId));
                db.SaveChanges();
            }
        }

        public Subject FindSubjectByName(string subjectName)
        {
            Subject subject;
            using (var db = new wpf_journalContext())
            {
                subject = db.Subjects.Select(p => p)
                                     .Where(p => p.SubjectName== subjectName)
                                     .SingleOrDefault();
            }
            return subject;
        }
    }
}
