using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Core
{
    //单例视图层管理
    public class View:IView
    {
        public View()
        {
            m_mediatorMap = new Dictionary<string, IMediator>();
            m_observerMap = new Dictionary<string, IList<IObserver>>();
            InitView();
        }

        static View()
        {

        }

        protected virtual void InitView()
        {

        }

        public static IView Instance
        {
            get
            {
                lock (m_staticSyncLocker)
                {
                    if (m_instance == null)
                    {
                        m_instance = new View();
                    }
                }

                return m_instance;
            }
        }

        //父类实现

        //注册观察者
        public virtual void RegisterObserver(string notificationName, IObserver observer)
        {
            lock (m_syncLocker)
            {
                if (!m_observerMap.ContainsKey(notificationName))
                {
                    m_observerMap[notificationName] = new List<IObserver>();
                }

                m_observerMap[notificationName].Add(observer);
            }
        }

        /// 移除消息观察者
        public virtual void RemoveObserver(string notificationName, object notifyContext)
        {
            lock (m_syncLocker)
            {
                if (m_observerMap.ContainsKey(notificationName))
                {
                    IList<IObserver> observers = m_observerMap[notificationName];

                    for (int i = 0; i < observers.Count;++i )
                    {
                        IObserver server = observers[i];
                        if (server.CompareNotifyContext(notifyContext))
                        {
                            observers.RemoveAt(i);
                            break;
                        }
                    }

                    if (observers.Count == 0)
                    {
                        m_observerMap.Remove(notificationName);
                    }
                }
            }
        }

        /// 通知所有观察者
        public virtual void NotifyObservers(INotification note)
        {
            IList<IObserver> observers = null;

            lock(m_syncLocker)
            {
                if (m_observerMap.ContainsKey(note.Name))
                {
                    observers = m_observerMap[note.Name];
                }
            }

            if (observers != null)
            {
                for (int i = 0; i < observers.Count; ++i )
                {
                    IObserver server = observers[i];
                    if (server != null)
                    {
                        server.NotifyObserver(note);//执行操作
                    }
                }
            }
        }

        //注册视图组件
        public virtual void RegisterMediator(IMediator mediator)
        {
            lock(m_syncLocker)
            {
                if (m_mediatorMap.ContainsKey(mediator.MediatorName))
                    return;

                m_mediatorMap[mediator.MediatorName] = mediator;

                //获取观察者上所有的消息
                IList<string> interests = mediator.ListNotificationInterests();
                if (interests.Count > 0)
                {
                    IObserver server = new Observer("handleNotification",mediator);
                    for (int i = 0; i < interests.Count; ++i )
                    {
                        //notification列表
                        RegisterObserver(interests[i], server);
                    }
                }
            }
            mediator.OnRegister();
        }

        //获取
        public virtual  IMediator RetrieveMediator(string mediatorName)
        {
            lock (m_syncLocker)
            {
                if (!m_mediatorMap.ContainsKey(mediatorName))
                    return null;

                return m_mediatorMap[mediatorName];
            }
        }

        //删除
        public virtual IMediator RemoveMediator(string mediatorName)
        {
            IMediator mediator = null;
            lock (m_syncLocker)
            {
                // Retrieve the named mediator
                if (!m_mediatorMap.ContainsKey(mediatorName)) return null;
                mediator = (IMediator)m_mediatorMap[mediatorName];
                // for every notification this mediator is interested in...
                IList<string> interests = mediator.ListNotificationInterests();

                for (int i = 0; i < interests.Count; i++)
                {
                    // remove the observer linking the mediator 
                    // to the notification interest
                    RemoveObserver(interests[i], mediator); //删除相应的注册事件
                }
                // remove the mediator from the map		
                m_mediatorMap.Remove(mediatorName);
            }

            // alert the mediator that it has been removed
            if (mediator != null) 
                mediator.OnRemove();
            return mediator;
        }

        //有
        public virtual bool HasMediator(string mediatorName)
        {
            lock (m_syncLocker)
            {
                return m_mediatorMap.ContainsKey(mediatorName);
            }
        }

        //观察者
        protected IDictionary<string, IMediator> m_mediatorMap;
        protected IDictionary<string, IList<IObserver>> m_observerMap;

        protected static volatile IView m_instance;

        protected readonly object m_syncLocker = new object();

        protected static readonly object m_staticSyncLocker = new object();
    }
}
