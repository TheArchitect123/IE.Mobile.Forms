namespace IE.Mobile.Forms.ValueConverters
{
    using IE.Utilities.Common;
    using System;
    using System.Globalization;
    using Xamarin.Forms;

    public class DirectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Value can never be null since validation is run by the view model
            return ConvertDirectionToString((PackmanDirection)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string ConvertDirectionToString(PackmanDirection obj)
        {
            switch (obj)
            {
                case PackmanDirection.North:
                    return "North";
                case PackmanDirection.South:
                    return "South";
                case PackmanDirection.East:
                    return "East";
                case PackmanDirection.West:
                    return "West";
            }

            return string.Empty;
        }
    }
}
