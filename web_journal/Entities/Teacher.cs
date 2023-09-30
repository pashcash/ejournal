using System;
using System.Collections.Generic;

#nullable disable

namespace web_journal
{
    public partial class Teacher
    {
        public Teacher()
        {
            SubjectTeachers = new HashSet<SubjectTeacher>();
        }

        public Teacher(long userId, string firstName, string middleName, string lastName)
        {
            UserId = userId;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }
    }
}
