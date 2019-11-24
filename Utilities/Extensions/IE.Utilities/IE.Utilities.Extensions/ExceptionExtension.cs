namespace IE.Utilities.Extensions
{
    using System;
    using Plugin.LocalNotification;

    public static class ExceptionExtension
    {
        public static void HandleException(this Exception ex, ref string message, ref string stacktrace)
        {
            message = ex.Message;
            stacktrace = ex.StackTrace;

            if (string.IsNullOrEmpty(message))
                message = "An unknown error has occurred";

            NotificationCenter.Current.Show(GenerateNotification(message, stacktrace));
        }

        private static NotificationRequest GenerateNotification(string message, string stacktrace)
        {
            return new NotificationRequest
            {
                NotificationId = (new Random()).Next(99, 200), //New Id is generated to prevent previous notifications from being deleted
                Title = message,
                Description = stacktrace,
                NotifyTime = DateTime.Now.AddSeconds(10)
            };
        }
    }
}
