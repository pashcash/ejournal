using System;
using System.Collections.Generic;

#nullable disable

namespace web_journal
{
    public partial class Class
    {
        public Class()
        {
            Schedules = new HashSet<Schedule>();
            Students = new HashSet<Student>();
        }

        public Class(long classId, long classNumber)
        {
            Id = classId;
            ClassNumber = classNumber;
        }

        public long Id { get; set; }
        public long ClassNumber { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
