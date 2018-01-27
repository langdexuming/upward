using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Reggie.Blog.Utils
{
    public class ContentProcessUtil
    {
        /// <summary>
        /// 将内容缩略成概要
        /// </summary>
        /// <param name="content">内容(博客/文章类HTML格式文本)</param>
        /// <param name="maxLenth">概要最大长度</param>
        /// <returns></returns>
        public static string ConvertSummeryFrom(string content,int maxLenth)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }

            var dic = new Dictionary<string, string>();
            dic.Add("<img.*?>","");
            dic.Add("<[^>]*?>", "");
            dic.Add("  *", " ");
            dic.Add("\n?", "");

            foreach (var item in dic)
            {
                content = Regex.Replace(content, item.Key, item.Value);
            }

            if (content.Length > maxLenth)
            {
                content += "...";
            }

            return content;
        }
    }
}
