using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace adventure_through_the_knight.Utilities.File_System
{
    /// <summary>
    /// Helper class can house basic file operations.
    /// </summary>
    class File_Check
    {
        /// <summary>
        /// Determines if a specific file is in use.
        /// </summary>
        /// <param name="filepath">The filepath of the file.</param>
        /// <returns>True if file is in use, False if not.</returns>
        public static bool CheckFileForInUse(String filepath)
        {
            StreamReader FILE;

            try
            {
                FILE = new StreamReader(filepath);
            }
            catch (IOException er)
            {
                return true;
            }

            FILE.Close();
            return false;
        }
    }
}
