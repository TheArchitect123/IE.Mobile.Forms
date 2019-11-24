namespace IE.Utilities.Extensions
{
    using System.IO;
    public static class FileExtensions
    {
        public static bool FilePathExists(this string filePath) => File.Exists(filePath);
        public static void CreateFileIfNotExists(this string filePath)
        {
            if (!File.Exists(filePath))
                File.Create(filePath);
            else
            {
                if (File.ReadAllBytes(filePath) != null && File.ReadAllBytes(filePath).Length != 0)
                {
                    File.Delete(filePath);
                    File.Create(filePath);
                }
            }
        }
    }
}
