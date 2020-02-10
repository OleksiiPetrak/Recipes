using System;
using System.Collections.Generic;
using System.Text;

namespace RecipesBook.Common.Interfaces
{
    public interface IFileAccessHelper
    {
        string GetLocalFilePath(string fileName);
    }
}
