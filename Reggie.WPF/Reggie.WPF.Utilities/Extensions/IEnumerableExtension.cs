/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/8/9 13:54:11
*   描述说明：
*
* Copyright (c) 2018 yinruimin Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本人机密信息，未经本人书面同意禁止向第三方披露．　│
*│　版权所有：yinruimin　　　　　　　　　　　　      │
*└──────────────────────────────────┘
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.WPF.Utilities.Extensions
{
    public static class IEnumerableExtension
    {
        public static string ToString(this IEnumerable<string> list,char separator)
        {
            if (list?.Count() > 0)
            {
                var builder = new StringBuilder();
                foreach (var item in list)
                {
                    builder.Append(item + separator);
                }

                return builder.ToString().TrimEnd(separator);
            }

            return string.Empty;
        }
    }
}
