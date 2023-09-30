using System;
using System.Collections.Generic;

#nullable disable

namespace web_journal
{
    public partial class Student
    {
        public Student()
        {
            StudentMarks = new HashSet<StudentMark>();
        }

        public Student(long userId, string firstName, string middleName, string lastName, long classNumber)
        {
            UserId = userId;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            ClassId = classNumber;
        }

        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public long? ClassId { get; set; }

        public virtual Class Class { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<StudentMark> StudentMarks { get; set; }
    }
}
