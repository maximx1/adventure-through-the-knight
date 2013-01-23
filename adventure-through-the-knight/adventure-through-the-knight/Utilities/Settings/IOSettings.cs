using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
#region Project usings
using adventure_through_the_knight.Output.Base;
using adventure_through_the_knight.Input;
using adventure_through_the_knight.Utilities.Error_Log;
#endregion

namespace adventure_through_the_knight.Utilities.Settings
{
    class IOSettings
    {
        public InputController.InputDeviceType CurrentInputType { get; set; }
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }
        public bool IsFullScreen { get; set; }
        public enum OS { Windows, Linux };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePath">The pilepath that is that the file is stored in.</param>
        public IOSettings(String filePath)
        {
            filePath = OSBasedStringConverting(filePath);
            try
            {
                if (!File.Exists(filePath))
                {// If the file doesn't exist.
                    File.Create(filePath);
                    DefaultSettings();
                    SaveSettings(filePath);
                }
                else
                {
                    XDocument doc = XDocument.Load(filePath);
                    XElement settings = XElement.Parse(doc.ToString());
                }

                
            }
            catch (IOException er)
            {
                Error_Log.Error_Log.RecordError(er.ToString());
            }
        }
        
        /// <summary>
        /// Start with default settings if no file is present.
        /// </summary>
        private void DefaultSettings()
        {
            this.CurrentInputType = InputController.InputDeviceType.KEYBOARD;
            this.WindowWidth = 800;
            this.WindowHeight = 600;
            this.IsFullScreen = false;
        }

        /// <summary>
        /// Stores the settings into a file.
        /// TODO: Use Serialize later on instead of XML.
        /// </summary>
        public void SaveSettings(String filePath)
        {
            XElement settings =
                new XElement("settings",
                    new XElement("window",
                        new XElement("height", WindowHeight),
                        new XElement("width", WindowWidth),
                        new XElement("fullscreen", IsFullScreen)),
                    new XElement("input", CurrentInputType)
                    );
            XDocument doc = new XDocument(settings);
            doc.Save(filePath);
        }

        #region Static Methods
        /// <summary>
        /// Static detector class to see which OS the system is running on.
        /// </summary>
        /// <returns>The OS that the system is running on.</returns>
        /// <remarks>
        /// Current OS' that have been tested that have running support:
        ///     Windows
        ///     Linux
        /// </remarks>
        public static OS DetectOS()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                return OS.Windows;
            else
                return OS.Linux;
        }

        /// <summary>
        /// Cleanses filepath strings based on the OS type.
        /// </summary>
        /// <param name="stringToConvert">String to check.</param>
        /// <returns>returns a filepath that has the correct directory "/"'s</returns>
        public static String OSBasedStringConverting(String stringToConvert)
        {
            if (DetectOS() == OS.Windows)
                return stringToConvert.Replace(@"/", @"\");
            else
                return stringToConvert.Replace(@"\", @"/");
        }
        #endregion
    }
}
