using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using static System.Reflection.MethodBase;

namespace HanoiApp
{
    class ConfigGetter
    {
        private XmlDocument _doc;
        private XmlElement _node;
        static string CONFIG_FILE_PATH = System.IO.Directory.GetCurrentDirectory() + "\\config.xml";

        public ConfigGetter()
        {
            _doc = new XmlDocument();
            _doc.Load(CONFIG_FILE_PATH);
            _node = _doc.DocumentElement;
        }

        public int GetIntdata(string nodeRoute)
        {
            int intData = Int32.Parse(_node.SelectSingleNode(nodeRoute).InnerText);
            return intData;
        }
    }
}
