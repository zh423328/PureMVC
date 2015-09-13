using System;
using PureMVC.Interfaces;

namespace PureMVC.Patterns
{
    //消息发送实现
    public class Notifier : INotifier
    {
        //消息接口
        public virtual void SendNotification(string notificationName)
        {
            m_facade.SendNotification(notificationName);
        }

        public virtual void SendNotification(string notificationName, object body)
        {
            m_facade.SendNotification(notificationName,body);
        }

        public virtual void SendNotification(string notificationName, object body, string nType)
        {
            m_facade.SendNotification(notificationName, body, nType);
        }

        protected IFacade Facade
		{
			get { return m_facade; }
		}

	    
		private IFacade m_facade = PureMVC.Patterns.Facade.Instance;
    }
}
