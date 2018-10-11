/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/4/27 9:11:45
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
    /// An object that represents the foreground color for a <see cref="AppTheme"/>.
    /// </summary>
    [DebuggerDisplay("accent={Name}, res={Resources.Source}")]
    public class Accent
    {
        /// <summary>
        /// The ResourceDictionary that represents this Accent.
        /// </summary>
        public ResourceDictionary Resources { get; set; }

        /// <summary>
        /// Gets/sets the name of the Accent.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the Accent class.
        /// </summary>
        public Accent()
        { }

        /// <summary>
        /// Initializes a new instance of the Accent class.
        /// </summary>
        /// <param name="name">The name of the new Accent.</param>
        /// <param name="resourceAddress">The URI of the accent ResourceDictionary.</param>
        public Accent(string name, Uri resourceAddress)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (resourceAddress == null) throw new ArgumentNullException(nameof(resourceAddress));

            this.Name = name;
            this.Resources = new ResourceDictionary { Source = resourceAddress };
        }
    }
}
