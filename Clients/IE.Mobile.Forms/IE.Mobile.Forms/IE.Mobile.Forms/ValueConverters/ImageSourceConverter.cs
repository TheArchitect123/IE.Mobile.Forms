namespace IE.Mobile.Forms.ValueConverters
{
    using System;
    using System.Globalization;
    using Xamarin.Forms;

    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var result = (string)value;
                if (result.StartsWith("http"))
                    return ImageSource.FromUri(new Uri(result));
                else
                    return ImageSource.FromFile(result);
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// This value converter is supposed to be used only for a one way binding between the view model and the view 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
