using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using adventure_through_the_knight.Utilities.File_System;

namespace adventure_through_the_knight.Utilities.Error_Log
{
    /// <summary>
    /// Error Loging class: will record errors that crop up.
    /// </summary>
    class Error_Log
    {
		//private static List<string> ErrorLogList = new List<string>();
		private static readonly string Filepath = @"error.log";

		/// <summary>
		/// Adds to the error queue.
		/// </summary>
		/// <param name='errorMessage'>
		/// The error message.
		/// </param>
		public static void RecordError (string errorMessage)
		{
//			if (File_Check.CheckFileForInUse (Filepath))
//			{
//				ErrorLogList.Add (errorMessage);
//			}

			//Tested this and with the speed that it runs, it does not pose a threat yet. Monitor.
			try
			{
				StreamWriter outfile = new StreamWriter (Filepath, true);
                outfile.WriteLine("--------------------------------------------------------------------------");
                outfile.WriteLine("                             Error Summary                                ");
                outfile.WriteLine("--------------------------------------------------------------------------");
				outfile.WriteLine (errorMessage);
				outfile.Close ();
			}
			catch
			{
				throw;
			}
		}
    }
}
