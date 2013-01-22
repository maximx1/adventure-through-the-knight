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
        public bool FullScreen { get; set; }

        public IOSettings(String filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                }

                XElement settings = XElement.Parse(File.ReadAllText(filePath));
                
            }
            catch (IOException er)
            {
                Error_Log.Error_Log.RecordError(er.ToString());
            }
        }
        
        private void DefaultSettings()
        {
            this.CurrentInputType = InputController.InputDeviceType.KEYBOARD;
            this.WindowWidth = 800;
            this.WindowHeight = 600;
            this.FullScreen = false;
        }


    }
}
