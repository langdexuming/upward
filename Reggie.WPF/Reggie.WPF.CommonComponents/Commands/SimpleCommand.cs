/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/4/27 15:08:10
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
using System.Windows.Input;

namespace Reggie.WPF.CommonComponents.Commands
{
    public class SimpleCommand : ICommand
    {
        public Predicate<object> CanExecuteDelegate { get; set; }
        public Action<object> ExecuteDelegate { get; set; }

        public SimpleCommand(Action<object> executeDelegate)
        {
            ExecuteDelegate = executeDelegate;
        }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null)
                return CanExecuteDelegate(parameter);
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (ExecuteDelegate != null)
                ExecuteDelegate(parameter);
        }
    }
}
