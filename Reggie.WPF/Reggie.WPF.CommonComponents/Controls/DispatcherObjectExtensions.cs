/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/7/18 17:28:50
*   描述说明：
*
* Copyright (c) 2018 yinruimin Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本人机密信息，未经本人书面同意禁止向第三方披露．　│
*│　版权所有：yinruimin　　　　　　　　　　　　      │
*└──────────────────────────────────┘
*****************************************************************/
using System;
using System.Windows.Threading;
using JetBrains.Annotations;

namespace Reggie.WPF.CommonComponents.Controls
{
    public static class DispatcherObjectExtensions
    {
        public static T Invoke<T>([NotNull] this DispatcherObject dispatcherObject, [NotNull] Func<T> func)
        {
            if (dispatcherObject == null)
            {
                throw new ArgumentNullException(nameof(dispatcherObject));
            }
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            if (dispatcherObject.Dispatcher.CheckAccess())
            {
                return func();
            }
            else
            {
                return (T)dispatcherObject.Dispatcher.Invoke(new Func<T>(func));
            }
        }

        public static void Invoke([NotNull] this DispatcherObject dispatcherObject, [NotNull] Action invokeAction)
        {
            if (dispatcherObject == null)
            {
                throw new ArgumentNullException(nameof(dispatcherObject));
            }
            if (invokeAction == null)
            {
                throw new ArgumentNullException(nameof(invokeAction));
            }
            if (dispatcherObject.Dispatcher.CheckAccess())
            {
                invokeAction();
            }
            else
            {
                dispatcherObject.Dispatcher.Invoke(invokeAction);
            }
        }

        /// <summary> 
        ///   Executes the specified action asynchronously with the DispatcherPriority.Background on the thread that the Dispatcher was created on.
        /// </summary>
        /// <param name="dispatcherObject">The dispatcher object where the action runs.</param>
        /// <param name="invokeAction">An action that takes no parameters.</param>
        /// <param name="priority">The dispatcher priority.</param> 
        public static void BeginInvoke([NotNull] this DispatcherObject dispatcherObject, [NotNull] Action invokeAction, DispatcherPriority priority = DispatcherPriority.Background)
        {
            if (dispatcherObject == null)
            {
                throw new ArgumentNullException(nameof(dispatcherObject));
            }
            if (invokeAction == null)
            {
                throw new ArgumentNullException(nameof(invokeAction));
            }
            dispatcherObject.Dispatcher.BeginInvoke(priority, invokeAction);
        }
    }
}
