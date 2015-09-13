using System;
using System.Collections.Generic;


namespace PureMVC.Interfaces
{
    //消息发送接口
    public interface INotifier
    {
        //消息接口
        void SendNotification(string notificationName);

        void SendNotification(string notificationName, object body);

        void SendNotification(string notificationName, object body, string nType);
    }
}
