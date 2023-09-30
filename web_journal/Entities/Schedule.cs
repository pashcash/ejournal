using System;
using System.Collections.Generic;

#nullable disable

namespace web_journal
{
    public partial class Schedule
    {
        public Schedule()
        {
            StudentMarks = new HashSet<StudentMark>();
        }

        public Schedule(long subjectId, long classId, long currentDay, long currentSemester)
        {
            SubjectId = subjectId;
            ClassId = classId;
            Day = currentDay;
            Semester = currentSemester;
        }

        public long Id { get; set; }
        public long SubjectId { get; set; }
        public long ClassId { get; set; }
        public long Day { get; set; }
        public long Semester { get; set; }

        public virtual Class Class { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<StudentMark> StudentMarks { get; set; }
    }
}
