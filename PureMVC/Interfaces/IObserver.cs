using System;


namespace PureMVC.Interfaces
{
    //观察者接口
    public interface IObserver
    {
        //方法
        string NotifyMethod { get; set; }

        //对象
        object NotifyContext { get; set; }

        //通知消息给对象
        void NotifyObserver(INotification notification);

        //对比对象
        bool CompareNotifyContext(object obj);
    }
}
