using System;
using System.Collections.Generic;

#nullable disable

namespace web_journal
{
    public partial class Subject
    {
        public Subject()
        {
            Schedules = new HashSet<Schedule>();
            SubjectTeachers = new HashSet<SubjectTeacher>();
        }

        public Subject(string subjectName, string subjectDescription)
        {
            SubjectName = subjectName;
            Description = subjectDescription;
        }

        public long Id { get; set; }
        public string SubjectName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }
    }
}
