using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace Testing_Fields
{
    class Program
    {
        public enum setting { GAMEPAD, KEYBOARD };
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load("dirtypete.xml");
            XElement settings = XElement.Parse(doc.ToString());
            Console.WriteLine(settings.ToString());
            Console.WriteLine();

            var windowSetting = settings.Element("window");
            var input = settings.Element("input");

            var WindowValues = settings.Descendants().Where(s => s.Parent.Name == "window" && s.Parent.Parent.Name == "settings")
                               .ToDictionary(s => s.Name.ToString(), s => s.Value.ToString());

            //Console.WriteLine("Window Height: " + 
            Console.WriteLine("Window Width: " + WindowValues["width"]);
            Console.WriteLine("Window pudding Height: " + WindowValues["height"]);
            if (Boolean.Parse(WindowValues["fullscreen"]))
            {
                if ((setting)Enum.Parse(typeof(setting), input.Value) == setting.GAMEPAD)
                {
                    Console.WriteLine("Window Full Screen: " + WindowValues["fullscreen"]);
                }
            }
            //save();
        }

        static void save()
        {
            XElement settings =
                new XElement("settings",
                    new XElement("window",
                        new XElement("height", 600),
                        new XElement("width", 800),
                        new XElement("fullscreen", true)),
                    new XElement("input", setting.GAMEPAD)
                    );
            XDocument doc = new XDocument(settings);
            doc.Save("dirtypete.xml");
        }
    }
}
