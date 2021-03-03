using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace St.Zoo.Core
{
    /// <summary>
    /// The file reader service
    /// </summary>
    public class FileService : IFileService
    {
        /// <summary>
        /// Read a file.
        /// </summary>
        /// <param name="path">the absolute file path</param>
        /// <returns>The file lines</returns>
        public IEnumerable<string> ReadLines(string path)
        {
            if (string.IsNullOrEmpty(path)) {
                throw new ArgumentNullException(nameof(path));
            }
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            return File.ReadAllLines(path, Encoding.UTF8);
        }
    }
}
