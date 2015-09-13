using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;


namespace PureMVC.Core
{
    //控制单例
    public class Controller : IController
    {
        public Controller()
        {
            m_CommandMap = new Dictionary<string, Type>();
            InitController();
        }

        static Controller()
        {

        }
        protected virtual void InitController()
        {
            m_view = View.Instance;
        }

        //单例
        public static IController Instance
        {
            get
            {
                lock (m_syncStaticLocker)
                {
                    if (m_instance == null)
                    {
                        m_instance = new Controller();
                    }
                }

                return m_instance;
            }
        }

        //接口实现
        //注册
        public virtual void RegisterCommand(string notificationName, Type commandType)
        {
            lock (m_syncLocker)
            {
                if (!m_CommandMap.ContainsKey(notificationName))
                {
                    // This call needs to be monitored carefully. Have to make sure that RegisterObserver
                    // doesn't call back into the controller, or a dead lock could happen.
                    m_view.RegisterObserver(notificationName, new Observer("executeCommand", this));//执行
                }

                m_CommandMap[notificationName] = commandType;
            }
        }
        //移除命令
        public virtual void RemoveCommand(string notificationName)
        {
            lock(m_syncLocker)
            {
                if (m_CommandMap.ContainsKey(notificationName))
                {
                    //移除
                    m_view.RemoveObserver(notificationName, this);
                    m_CommandMap.Remove(notificationName);
                }
            }
        }

        //是否存在
        public virtual bool HasCommand(string noticationName)
        {
            lock (m_syncLocker)
            {
                return m_CommandMap.ContainsKey(noticationName);
            }
        }

        //执行
        public virtual void ExecuteCommand(INotification notification)
        {
            Type commandType = null;
            lock(m_syncLocker)
            {
                if (m_CommandMap.ContainsKey(notification.Name))
                    commandType = m_CommandMap[notification.Name];
            }

            object commandInstance = Activator.CreateInstance(commandType);

            if (commandInstance is ICommand)
            {
                ((ICommand)commandInstance).Execute(notification);
            }
        }

        //成员
        protected IView m_view; //视图

        protected Dictionary<string, Type> m_CommandMap;
        protected volatile static IController m_instance;//单例
        protected readonly object m_syncLocker = new object();
        protected static readonly object m_syncStaticLocker = new object();
    }
}
