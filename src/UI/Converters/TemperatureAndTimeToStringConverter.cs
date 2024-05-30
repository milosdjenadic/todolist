using System.Globalization;
using System.Windows.Data;

namespace Assignment.UI.Converters;
public class TemperatureAndTimeToStringConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length != 2)
        {
            return null;
        }

        var temperature = values[0] as int?;
        var time = values[1] as DateTime?;

        if (!temperature.HasValue
            || !time.HasValue)
        {
            return null;
        }

        return $"{temperature}°C at {time:t}";
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
