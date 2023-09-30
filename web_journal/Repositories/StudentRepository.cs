using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace web_journal.Repositories
{
    class StudentRepository : UserRepository
    {
        //Поиск студента по логину
        public Student FindStudentByLogin(string login)
        {
            Student student;
            User user = base.FindUserByLogin(login);
            using (var db = new wpf_journalContext())
            {
                student = db.Students
                            .Select(p => p)
                            .Where(p => p.UserId == user.Id)
                            .SingleOrDefault();
            }
            return student;
        }

        public Subject FindSubjectById(int subjectId)
        {
            Subject subject;
            using (var db = new wpf_journalContext())
            {
                subject = db.Subjects
                            .Select(p => p)
                            .Where(p => p.Id == subjectId)
                            .SingleOrDefault();
            }
            return subject;
        }

        //Поиск всех предметов по классу
        public List<Subject> FindStudentSubjectsByClass(long? classId, long semester)
        {
            List<Subject> subjects;
            using (var db = new wpf_journalContext())
            {
                subjects = db.Schedules
                             .Select(p => p)
                             .Where(p => p.ClassId == classId && p.Semester == semester)
                             .Join(db.Subjects, p => p.SubjectId, c => c.Id, (p, c) => new Subject { Id = c.Id, SubjectName = c.SubjectName, Description = c.Description })
                             .ToList()
                             .GroupBy(p => p.Id)
                             .Select(p => p.First())
                             .OrderBy(p => p.Id)
                             .ToList();
            }
            return subjects;
        }

        //Поиск оценок по студенту
        public List<StudentMark> FindStudentMarksByStudent(long? subjectId, long? studentId, long semesterNumber)
        {
            List<StudentMark> marks;
            using (var db = new wpf_journalContext())
            {
                marks = db.Schedules
                          .Select(p => p)
                          .Where(p => p.Semester == semesterNumber && p.SubjectId == subjectId)
                          .Join(db.StudentMarks, p => p.Id, c => c.ScheduleId, (p, c) => new StudentMark { Id = c.Id, StudentId = c.StudentId, MarkNumber = c.MarkNumber, ScheduleId = c.ScheduleId })
                          .Where(p => p.StudentId == studentId)
                          .ToList();
            }
            return marks;
        }

        //По id поиск номера класса, для отображения classId в виде classNumber
        public int FindStudentClassNumber(long classId)
        {
            Class studentClass;
            using (var db = new wpf_journalContext())
            {
                studentClass = db.Classes
                                 .Select(p => p)
                                 .Where(p => p.Id == classId)
                                 .SingleOrDefault();
            }
            return (int)studentClass.ClassNumber;
        }

        //Поиск предметов по классу, которые есть в расписании
        public List<Subject> FindStudentScheduleSubjects(long currentSemester, long currentDay)
        {
            Student currentStudent = FindStudentByLogin(Thread.CurrentPrincipal.Identity.Name);
            List<Subject> scheduleSubjects;
            using (var db = new wpf_journalContext())
            {
                scheduleSubjects = db.Schedules
                                     .Select(p => p)
                                     .Where(p => p.ClassId == currentStudent.ClassId && p.Semester == currentSemester && p.Day == currentDay)
                                     .Join(db.Subjects, p => p.SubjectId, c => c.Id, (p, c) => new Subject { Id = c.Id, SubjectName = c.SubjectName, Description = c.Description })
                                     .Select(p => p)
                                     .ToList();
            }
            return scheduleSubjects;
        }

        //Поиск учеников, у которых ведет предметы учитель с указанным id
        public List<Student> FindStudentsByTeacher(long teacherId)
        {
            List<Student> students;
            using (var db = new wpf_journalContext())
            {
                students = db.Teachers
                             .Select(p => p)
                             .Where(p => p.UserId == teacherId)
                             .Join(db.SubjectTeachers, p => p.UserId, c => c.TeacherId, (p, c) => new SubjectTeacher { SubjectId = c.SubjectId, TeacherId = c.TeacherId })
                             .Select(p => p)
                             .Join(db.Schedules, p => p.SubjectId, c => c.SubjectId, (p, c) => new Schedule { SubjectId = c.SubjectId, ClassId = c.ClassId, Day = c.Day, Semester = c.Semester })
                             .Select(p => p)
                             .Join(db.Students, p => p.ClassId, c => c.ClassId, (p, c) => new Student { UserId = c.UserId, FirstName = c.FirstName, MiddleName = c.MiddleName, LastName = c.LastName, ClassId = c.ClassId })
                             .ToList()
                             .GroupBy(p => p.UserId)
                             .Select(p => p.First())
                             .ToList();
            }
            return students;
        }

        //Поиск предметов по классу указанного студента, которые ведет указанный учитель по расписанию
        public List<Subject> FindStudentSubjectsByTeacher(long? classId, long semester, long day, long teacherId)
        {
            List<Subject> subjects;
            using (var db = new wpf_journalContext())
            {
                subjects = db.Schedules
                             .Select(p => p)
                             .Where(p => p.ClassId == classId && p.Semester == semester && p.Day == day)
                             .Join(db.SubjectTeachers, p=>p.SubjectId, c=>c.SubjectId, (p,c)=>new SubjectTeacher{ SubjectId=c.SubjectId, TeacherId=c.TeacherId })
                             .Where(p=>p.TeacherId==teacherId)
                             .Join(db.Subjects, p => p.SubjectId, c => c.Id, (p, c) => new Subject { Id = c.Id, SubjectName = c.SubjectName, Description = c.Description })
                             .ToList()
                             .GroupBy(p => p.Id)
                             .Select(p => p.First())
                             .OrderBy(p => p.Id)
                             .ToList();
            }
            return subjects;
        }

        //Поиск записи расписания с указанными данными
        public Schedule FindStudentScheduleId(long subjectId, long? classId, long day, long semester)
        {
            Schedule schedule;
            using (var db = new wpf_journalContext())
            {
                schedule = db.Schedules
                             .Select(p => p)
                             .Where(p => p.SubjectId == subjectId && p.ClassId == classId && p.Day == day && p.Semester == semester)
                             .SingleOrDefault();
            }
            return schedule;
        }

        //Дописать добавление в базу оценок по студенту
        public void AddStudentMark(long studentId, long markNumber, long scheduleId)
        {
            using (var db = new wpf_journalContext())
            {
                db.StudentMarks.Add(new StudentMark(studentId, markNumber, scheduleId));
                db.SaveChanges();
            }
        }

        public List<Schedule> FindTeacherSchedulesByTeacher(long currentSemester, long currentDay)
        {
            TeacherRepository teacherRepository = new TeacherRepository();
            long teacherId = teacherRepository.FindTeacherByLogin(Thread.CurrentPrincipal.Identity.Name).UserId;
            List<Schedule> teacherSchedules;
            using (var db = new wpf_journalContext())
            {
                teacherSchedules = db.SubjectTeachers
                                     .Select(p => p)
                                     .Where(p => p.TeacherId == teacherId)
                                     .Join(db.Schedules, p => p.SubjectId, c => c.SubjectId, (p, c) => new Schedule { Id = c.Id, SubjectId = c.SubjectId, ClassId = c.ClassId, Day = c.Day, Semester = c.Semester })
                                     .Where(p=>p.Semester == currentSemester && p.Day == currentDay)
                                     .OrderBy(p=>p.ClassId)
                                     .ToList();
            }
            return teacherSchedules;
        }

        public List<Student> FindAllStudents()
        {
            List<Student> students;
            using (var db = new wpf_journalContext())
            {
                students = db.Students.OrderBy(p=>p.ClassId).ToList();
            }
            return students;
        }

        //Поиск класса с заданным id
        public bool FindClass(long classNumber)
        {
            bool isFind = false;
            using (var db = new wpf_journalContext())
            {
                isFind = db.Classes.Select(p => p).Where(p => p.ClassNumber == classNumber).Any();
            }
            return isFind;
        }

        public void AddStudent(string login, string password, string firstName, string middleName, string lastName, long classNumber)
        {
            using (var db = new wpf_journalContext())
            {
                db.Users.Add(new User(login, password, "Ученик"));
                db.SaveChanges();
                User studentUser = FindUserByLogin(login);
                if (!FindClass(classNumber))
                {
                    db.Classes.Add(new Class(classNumber, classNumber));
                    db.SaveChanges();
                }
                db.Students.Add(new Student(studentUser.Id, firstName, middleName, lastName, classNumber));
                db.SaveChanges();
            }
        }
    }
}
