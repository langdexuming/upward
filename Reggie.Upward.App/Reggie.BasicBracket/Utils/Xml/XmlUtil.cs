using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Reggie.BasicBracket.Utils.Xml
{
    public class XmlUtil
    {
        public static string GetValue(string path, string node)
        {
            var value = string.Empty;
            var readXml = new XmlDocument();
            try
            {
                readXml.Load(path);
                //var selectSingleNode = readXml.SelectSingleNode("AppConfig");
                //var singleNode = selectSingleNode?.SelectSingleNode("LogLevel");
                var singleNode = readXml.SelectSingleNode(node);
                if (singleNode != null)
                    value = singleNode.InnerText;
            }
            catch (Exception ex)
            {
                // ignored
            }
            return value;
        }
    }
}
