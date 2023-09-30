using System;
using web_journal.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;

namespace web_journal.Converters
{
    class SubjectIdConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int subjectId = int.Parse(value.ToString());
            StudentRepository studentRepository = new StudentRepository();
            return studentRepository.FindSubjectById(subjectId).SubjectName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        #endregion
    }
}
