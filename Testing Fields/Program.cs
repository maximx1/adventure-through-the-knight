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

            Console.WriteLine("Window Height: " + windowSetting.Element("height").Value);
            Console.WriteLine("Window Width: " + windowSetting.Element("width").Value);
            Console.WriteLine("Window Full Screen: " + windowSetting.Element("fullscreen").Value);
            if (Boolean.Parse(windowSetting.Element("fullscreen").Value))
            {
                if ((setting)Enum.Parse(typeof(setting), input.Value) == setting.GAMEPAD)
                {
                    Console.WriteLine("Window pudding Height: " + input.Value);
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
