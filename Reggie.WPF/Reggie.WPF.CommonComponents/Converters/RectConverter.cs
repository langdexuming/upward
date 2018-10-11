/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/6/8 11:03:47
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
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Reggie.WPF.CommonComponents.Converters
{
    public class RectConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values?.Count() >= 2)
            {
                if (values[0] != null && values[1] != null)
                {
                    double width;
                    double height;
                    if (double.TryParse(values[0].ToString(), out width) && (double.TryParse(values[1].ToString(), out height)))
                    {
                        return new Rect(0, 0, width, height);
                    }
                }
            }
            return new Rect();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
