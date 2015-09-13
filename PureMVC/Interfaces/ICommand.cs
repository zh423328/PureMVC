using System;


namespace PureMVC.Interfaces
{
    //command命令接口
    public interface ICommand
    {
        //执行命令
        void Execute(INotification notification);
    }
}
