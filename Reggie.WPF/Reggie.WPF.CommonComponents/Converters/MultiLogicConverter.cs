/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/8/9 16:03:03
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
using System.Threading.Tasks;
using System.Windows.Data;

namespace Reggie.WPF.CommonComponents.Converters
{
    public enum LogicOperation
    {
        Or,
        And,
    }

    public class MultiLogicConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values==null || values.Length < 2)
            {
                return false;
            }

            LogicOperation logicOperation=LogicOperation.Or;

            if (parameter != null)
            {
                Enum.TryParse<LogicOperation>(parameter.ToString(), out logicOperation);
            }

            var result = logicOperation == LogicOperation.And ? true : false;
            for (int i = 0; i < values.Length; i++)
            {
                var val = values[i] as bool?;
                if (val != null)
                {
                    switch (logicOperation)
                    {
                        case LogicOperation.Or:
                            result = result || val.Value;
                            break;
                        case LogicOperation.And:
                            result = result && val.Value;
                            break;
                        default:
                            result = result || val.Value;
                            break;
                    }
                }
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
