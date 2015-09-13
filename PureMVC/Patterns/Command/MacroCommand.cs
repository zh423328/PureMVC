using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Patterns
{
    //多重调用
    public class MacroCommand:Notifier,ICommand
    {
        public MacroCommand()
        {
            m_subCommands = new List<Type>();
            InitCommand();
        }

        protected virtual void InitCommand()
        {

        }

        //添加命令
        protected void AddCommand(Type commandType)
        {
            m_subCommands.Add(commandType);
        }

        //执行
        public virtual void Execute(INotification notification)
        {
            while (m_subCommands.Count > 0)
            {
                //执行
                Type typeCommand = m_subCommands[0];

                object commandInstance = Activator.CreateInstance(typeCommand);

                if (commandInstance is ICommand)
                {
                    //执行
                    ((ICommand)commandInstance).Execute(notification); 
                }
                m_subCommands.RemoveAt(0);
            }
        }
        //成员对象
        protected List<Type> m_subCommands;
    }
}
