namespace IE.Utilities.Extensions
{
    using System;

    public static class DateFormatExtensions
    {
        public static string Date(this DateTime date) => date.ToString("m");

        public static string Time(this DateTime time) => time.ToString("hh:mm tt");

    }
}
