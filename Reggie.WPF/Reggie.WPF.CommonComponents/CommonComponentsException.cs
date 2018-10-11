/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/4/27 9:16:42
*   描述说明：
*
* Copyright (c) 2018 yinruimin Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本人机密信息，未经本人书面同意禁止向第三方披露．　│
*│　版权所有：yinruimin　　　　　　　　　　　　      │
*└──────────────────────────────────┘
*****************************************************************/
using System;
using System.Runtime.Serialization;

namespace Reggie.WPF.CommonComponents
{
    internal class AppName
    {
        public const string CommonComponents = "Reggie.WPF.CommonComponents";
    }

    [Serializable]
    public class CommonComponentsException : Exception
    {
        public CommonComponentsException()
        {
        }

        public CommonComponentsException(string message)
            : base(message)
        {
        }

        public CommonComponentsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CommonComponentsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
