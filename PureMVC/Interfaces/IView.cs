using System;
using System.Collections.Generic;

namespace PureMVC.Interfaces
{
    //视图层接口，调用观察者来操作组件
    public interface IView
    {
        #region Observer

        /// 绑定一个消息到观察者
        void RegisterObserver(string notificationName, IObserver observer);

        /// 移除消息观察者
        void RemoveObserver(string notificationName, object notifyContext);

        /// 通知观察者
        void NotifyObservers(INotification note);

        #endregion

        #region Mediator

        /// 注册视图组件到视图上
        void RegisterMediator(IMediator mediator);

        //获取
        IMediator RetrieveMediator(string mediatorName);

        //移除
        IMediator RemoveMediator(string mediatorName);

        //检查
        bool HasMediator(string mediatorName);

        #endregion
    }
}
