namespace IE.Utilities.Extensions
{
    using System.IO;

    public static class PhotoStreamExtensions
    {
        public static string ConvertToString(this byte[] avatar) => new StreamReader(new MemoryStream(avatar)).ReadToEnd();
    }
}
