/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/5/30 9:34:14
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Reggie.WPF.CommonComponents.Converters
{
    public class PathToImageSourceConverter : IValueConverter
    {
        public string ImageDirectory { get; set; } = AppDomain.CurrentDomain.BaseDirectory;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;
            BitmapImage result = null;
            if (!string.IsNullOrEmpty(path))
            {
                //var param = parameter as string;
                ////是否是Resource资源
                //var isResource = false;
                //if (!string.IsNullOrEmpty(param))
                //{
                //    isResource = param.Equals("Resource",StringComparison.OrdinalIgnoreCase);
                //}
                try
                {
                    if (!string.IsNullOrEmpty(ImageDirectory))
                    {
                        path = Path.Combine(ImageDirectory, path);
                        result = new BitmapImage(new Uri(path));
                    }
                    else
                    {
                        result = new BitmapImage(new Uri(path,UriKind.Relative));
                    }
                }
                catch
                {
                    //Ignore
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
