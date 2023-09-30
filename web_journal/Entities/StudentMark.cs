using System;
using System.Collections.Generic;

#nullable disable

namespace web_journal
{
    public partial class StudentMark
    {
        public StudentMark()
        {

        }
        public StudentMark(long studentId, long markNumber, long scheduleId)
        {
            StudentId = studentId;
            MarkNumber = markNumber;
            ScheduleId = scheduleId;
        }

        public long Id { get; set; }
        public long StudentId { get; set; }
        public long MarkNumber { get; set; }
        public long ScheduleId { get; set; }

        public virtual Schedule Schedule { get; set; }
        public virtual Student Student { get; set; }
    }
}
