/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/8/13 15:57:14
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
using System.Windows.Media.Imaging;

namespace Reggie.WPF.CommonComponents.Converters
{
    public class UrlToImageWithCacheConverter
    {
        private static Dictionary<string, BitmapImage> _bitmapImageCache = new Dictionary<string, BitmapImage>();
    }
}
