using System;
using System.Collections.Generic;

#nullable disable

namespace web_journal
{
    public partial class SubjectTeacher
    {
        public SubjectTeacher()
        {

        }
        public SubjectTeacher(long teacherId, long subjectId)
        {
            TeacherId = teacherId;
            SubjectId = subjectId;
        }

        public long SubjectId { get; set; }
        public long TeacherId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
