using System;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Patterns
{
    //命令,父类要调用必须重写
    public class SimpleCommand : Notifier,ICommand 
    {
        //执行
        public virtual void Execute(INotification noftication)
        {

        }
    }
}
