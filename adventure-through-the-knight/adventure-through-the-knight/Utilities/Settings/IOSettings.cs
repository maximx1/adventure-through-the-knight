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
    public class IOSettings
    {
        public InputController.InputDeviceType CurrentInputType { get; set; }
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }
        public bool IsFullScreen { get; set; }
        public enum OS { Windows, Linux };
        public String FilePath { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePath">The pilepath that is that the file is stored in.</param>
        public IOSettings(String filePath)
        {
            FilePath = OSBasedStringConverting(filePath);
            try
            {
                if (!Directory.Exists("local"))
                {
                    Directory.CreateDirectory("local");
                }
                if (!File.Exists(filePath))
                {// If the file doesn't exist.
                    File.Create(filePath).Close();
                    DefaultSettings();
                    SaveSettings();
                }
                else
                {
                    LoadSettings();
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
        private void SaveSettings()
        {
            XElement settings =
                new XElement("settings",
                    new XElement("window",
                        new XElement("height", this.WindowHeight),
                        new XElement("width", this.WindowWidth),
                        new XElement("fullscreen", this.IsFullScreen)),
                    new XElement("input", this.CurrentInputType)
                    );
            XDocument doc = new XDocument(settings);
            try
            {
                doc.Save(this.FilePath);
            }
            catch (Exception er)
            {
                Error_Log.Error_Log.RecordError(er.ToString());
            }
            
        }

        /// <summary>
        /// Load the settings from the filePath XML file.
        /// </summary>
        /// <param name="filePath">The file path to the settings .xml files.</param>
        public void LoadSettings()
        {
            //Load all of the settings.
            XElement settings = XElement.Parse(XDocument.Load(this.FilePath).ToString());
            
            //Create a dictionary of all the settings parsed from the XML.

            var windowValues = settings.Descendants()
                               .Where(s => s.HasElements == false)
                               .ToDictionary(s => s.Name.ToString(), s => s.Value.ToString());

            //var windowValues = settings.Descendants()
            //                   .Where(s => s.Parent.Name == "window" && s.Parent.Parent.Name == "settings")
            //                   .ToDictionary(s => s.Name.ToString(), s => s.Value.ToString());
            //windowValues.Add(settings.Descendants()
            //                 .Where(s => s.Parent.Name == "settings" && s.Name == "input").Select(s=>s.Name.ToString()).FirstOrDefault(),
            //                 settings.Descendants()
            //                 .Where(s => s.Parent.Name == "settings" && s.Name == "input").Select(s => s.Value).FirstOrDefault());

            try
            {
                //Load to the *this
                this.WindowHeight = Int32.Parse(windowValues["height"]);
                this.WindowWidth = Int32.Parse(windowValues["width"]);
                this.IsFullScreen = Boolean.Parse(windowValues["fullscreen"]);
                this.CurrentInputType = (InputController.InputDeviceType)Enum.Parse(typeof(InputController.InputDeviceType), windowValues["input"]);
            }
            catch (Exception er)
            {
                Error_Log.Error_Log.RecordError("There was an issue parsing the xml: " + er.ToString());
                DefaultSettings();
                SaveSettings();
            }
        }

        /// <summary>
        /// Changes the setting for the input controller.
        /// </summary>
        public void ChangeInputType()
        {
            if (this.CurrentInputType == InputController.InputDeviceType.GAMEPAD)
                this.CurrentInputType = InputController.InputDeviceType.KEYBOARD;
            else
                this.CurrentInputType = InputController.InputDeviceType.GAMEPAD;
            SaveSettings();
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
