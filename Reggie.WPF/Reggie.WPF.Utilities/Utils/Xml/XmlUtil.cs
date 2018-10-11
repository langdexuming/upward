using Reggie.WPF.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Reggie.WPF.Utilities.Utils.Xml
{
    public class XmlUtil
    {
        private const string TAG = nameof(XmlUtil);

        /// <summary>
        /// 获取XML文件的节点值
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="node">节点名称,用'/'划分层级,例如'/root/xx'</param>
        /// <returns></returns>
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
                Logger.Error(TAG, ex);
                throw ex;
            }
            return value;
        }

        /// <summary>
        /// 获取XML文件的节点值
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="node">节点名称,用'/'划分层级,例如'/root/xx'</param>
        /// <param name="attribute">属性</param>
        /// <returns></returns>
        public static List<string> GetValues(string path, string node,string attribute="")
        {
            var values = new List<string>();
            var readXml = new XmlDocument();
            try
            {
                readXml.Load(path);
                var xmlNodeList = readXml.SelectNodes(node);
                if (string.IsNullOrEmpty(attribute))
                {
                    for (int i = 0; i < xmlNodeList.Count; i++)
                    {
                        values.Add(xmlNodeList.Item(i).InnerText);
                    }
                }
                else
                {
                    for (int i = 0; i < xmlNodeList.Count; i++)
                    {
                        values.Add(xmlNodeList.Item(i).Attributes.GetNamedItem(attribute).Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, ex);
                throw ex;
            }
            return values;
        }

        /// <summary>
        /// 获取XML文件的节点值
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="node">节点名称,用'/'划分层级,例如'/root/xx'</param>
        /// <param name="innerText">节点文本</param>
        /// <returns></returns>
        public static void SetValue(string path, string node, string innerText)
        {
            var value = string.Empty;
            var readXml = new XmlDocument();
            try
            {
                readXml.Load(path);
                var singleNode = readXml.SelectSingleNode(node);
                singleNode.InnerText = innerText;
                readXml.Save(path);
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, ex);
                throw ex;
            }
        }
    }
}
