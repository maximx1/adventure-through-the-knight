using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace adventure_through_the_knight.Utilities.File_System
{
    static class File_Check
    {

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
