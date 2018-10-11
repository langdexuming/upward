/****************************************************************
*   作者：xuxi 5372
*   创建时间：2018/8/2 17:12:27
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
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Reggie.WPF.CommonComponents.Converters
{
    public class PathToUriConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;
            if (!string.IsNullOrEmpty(path))
            {              
                try
                {
                    if (!string.IsNullOrEmpty(AppDomain.CurrentDomain.BaseDirectory))
                    {
                        path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
                    }
                    else
                    {
                    }
                }
                catch
                {
                    //Ignore
                }
            }
            return new Uri(path);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
