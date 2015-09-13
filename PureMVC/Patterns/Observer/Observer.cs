using System;
using System.Reflection;    //发射
using PureMVC.Interfaces;

namespace PureMVC.Patterns
{
    //observer具体实现
    public class Observer:IObserver
    {
        public Observer(string notifyMethod, object notifyContext)
        {
            m_notifyMethod = notifyMethod;
            m_notifyContext = notifyContext;
        }

        //实现父类
        public virtual string NotifyMethod
        {
            get
            {
                return m_notifyMethod;
            }

            set
            {
                m_notifyMethod = value;
            }
        }

        public virtual object NotifyContext
        {
            get
            {
                return m_notifyContext;
            }

            set
            {
                m_notifyContext = value;
            }
        }

        //观察者调用
        public virtual void NotifyObserver(INotification notfication)
        {
            string notifyMethod;
            object notfiyContext;

            lock (m_syncLocker)
            {
                notifyMethod = NotifyMethod;
                notfiyContext = NotifyContext;
            }

            //反射调用
            Type t = notfiyContext.GetType();
            BindingFlags f = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
            MethodInfo mi = t.GetMethod(NotifyMethod, f);
            mi.Invoke(notfiyContext, new object[] { notfication }); //掉头具体方法
        }

        //比较
        public virtual bool CompareNotifyContext(object obj)
        {
            lock(m_syncLocker)
            {
                return m_notifyContext.Equals(obj);
            }
        }

        //成员
        protected string m_notifyMethod;//通知方法
        protected object m_notifyContext;//调用方法对象
        protected readonly object m_syncLocker = new object(); //locker
    }
}
