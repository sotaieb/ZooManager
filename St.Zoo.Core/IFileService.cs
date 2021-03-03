using System.Collections.Generic;

namespace St.Zoo.Core
{
    public interface IFileService
    {
        IEnumerable<string> ReadLines(string path);
    }
}