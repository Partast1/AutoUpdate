using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace AutoUpdate
{
    class XmlHandler
    {
        public XDocument XmlDoc { get; set; }
        public XmlHandler(DirectoryInfo dir, int pathToRemove)
        {
            XmlDoc = CreateXml(dir, pathToRemove);
        }
        private XDocument CreateXml(DirectoryInfo dir, int pathToRemove)
        {
            return new XDocument(FillXML(dir, pathToRemove));
        }
        private XElement FillXML(DirectoryInfo dir, int pathToRemove)
        {
            XElement xmlInfo = new XElement("folder", new XAttribute("path", dir.FullName.Remove(0, pathToRemove)), new XAttribute("ID", 1));

            foreach (FileInfo file in dir.GetFiles())
                xmlInfo.Add(new XElement("file", new XAttribute("path", file.FullName.Remove(0, pathToRemove)), new XAttribute("ID", 1)));

            foreach (DirectoryInfo folder in dir.GetDirectories())
                xmlInfo.Add(FillXML(folder, pathToRemove));

            return xmlInfo;
        }
    }
}
