using RecipesBook.Common.Interfaces;
using System.IO;
using Xamarin.Essentials;

namespace RecipesBook.Common.Helpers
{
    public class FileAccessHelper : IFileAccessHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            return Path.Combine(FileSystem.AppDataDirectory, fileName);
        }
    }
}
