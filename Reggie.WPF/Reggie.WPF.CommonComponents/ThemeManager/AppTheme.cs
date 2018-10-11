/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/4/27 9:12:36
*   描述说明：
*
* Copyright (c) 2018 yinruimin Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本人机密信息，未经本人书面同意禁止向第三方披露．　│
*│　版权所有：yinruimin　　　　　　　　　　　　      │
*└──────────────────────────────────┘
*****************************************************************/
namespace Reggie.WPF.CommonComponents
{
    using System;
    using System.Diagnostics;
    using System.Windows;

    /// <summary>
    /// Represents the background theme of the application.
    /// </summary>
    [DebuggerDisplay("apptheme={Name}, res={Resources.Source}")]
    public class AppTheme
    {
        /// <summary>
        /// The ResourceDictionary that represents this application theme.
        /// </summary>
        public ResourceDictionary Resources { get; }

        /// <summary>
        /// Gets the name of the application theme.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the AppTheme class.
        /// </summary>
        /// <param name="name">The name of the new AppTheme.</param>
        /// <param name="resourceAddress">The URI of the accent ResourceDictionary.</param>
        public AppTheme(string name, Uri resourceAddress)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (resourceAddress == null) throw new ArgumentNullException(nameof(resourceAddress));

            this.Name = name;
            this.Resources = new ResourceDictionary { Source = resourceAddress };
        }
    }
}
