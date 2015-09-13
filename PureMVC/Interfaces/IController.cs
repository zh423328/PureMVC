using System;

namespace PureMVC.Interfaces
{
    //控制command的方法
    public interface IController
    {
        //注册
        void RegisterCommand(string notificationName, Type commandType);

        //移除
        void RemoveCommand(string notificationName);

        //执行
        void ExecuteCommand(INotification notification);

        //判断
        bool HasCommand(string notification);
    }
}
