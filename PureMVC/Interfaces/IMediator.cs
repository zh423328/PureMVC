using System;
using System.Collections.Generic;


namespace PureMVC.Interfaces
{
    //管理视图组件
    public interface IMediator
    {
        //
        string MediatorName { get; }

        //视图组件
        object ViewComponent { get; set; }

        //视图的消息
        IList<string> ListNotificationInterests();

        /// 操作消息
        void HandleNotification(INotification notification);

        //view层注册
        void OnRegister();


        //view 层移除
        void OnRemove();
    }
}
