using System;
using System.Collections.Generic;


namespace PureMVC.Interfaces
{
    public interface IFacade : INotifier
    {
#region Proxy
        //注册
        void RegisterProxy(IProxy proxy);
        //获取
        IProxy RetrieveProxy(string proxyName);
        //移除
        IProxy RemoveProxy(string proxyName);
        //检测
        bool HasProxy(string proxyName);
#endregion

#region COMMAND
        void RegisterCommand(string notificationName, Type commandType);
        void RemoveCommand(string notificationName);
        bool HasCommand(string notificationName);
#endregion

#region Mediator
        void RegisterMediator(IMediator mediator);
        IMediator RetrieveMediator(string mediatorName);
        IMediator RemoveMediator(string mediatorName);
        bool HasMediator(string mediatorName);
#endregion

#region Observer
        void NotifyObservers(INotification note);
#endregion
    }
}
