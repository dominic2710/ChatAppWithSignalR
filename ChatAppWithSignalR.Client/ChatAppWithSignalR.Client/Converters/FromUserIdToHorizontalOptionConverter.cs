using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppWithSignalR.Client.Converters
{
    public class FromUserIdToHorizontalOptionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0) return LayoutOptions.Start;

            if (values[0] == null || values[1] == null) return LayoutOptions.Start;

            if (values[0].ToString() == values[1].ToString()) return LayoutOptions.Start;

            return LayoutOptions.End;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
