/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/6/15 14:13:38
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reggie.WPF.CommonComponents.Commands
{
    public class RelayCommand
    {
        public Predicate<object> CanExecuteDelegate { get; set; }
        public Action<object> ExecuteDelegate { get; set; }

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

    public class RelayCommand<T> : ICommand
    {
        public RelayCommand(Action<T> execute) : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            this._execute = execute;
            if (canExecute != null)
            {
                this._canExecute = canExecute;
            }
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            if (this._canExecute == null)
            {
                return true;
            }
#if NET4_0
#else
            if (parameter == null && typeof(T).GetTypeInfo().IsValueType)
            {
                return this._canExecute(default(T));
            }
#endif
            if (parameter == null || parameter is T)
            {
                return this._canExecute((T)((object)parameter));
            }
            return false;
        }

        public virtual void Execute(object parameter)
        {
            if (this.CanExecute(parameter) && this._execute != null)
            {
                if (parameter == null)
                {
#if NET4_0
#else
                    if (typeof(T).GetTypeInfo().IsValueType)
                    {
                        this._execute(default(T));
                        return;
                    }
#endif
                    this._execute((T)((object)parameter));
                    return;
                }
                else
                {
                    this._execute((T)((object)parameter));
                }
            }
        }

        private readonly Action<T> _execute;

        private readonly Func<T, bool> _canExecute;
    }

}
