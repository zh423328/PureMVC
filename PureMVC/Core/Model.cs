using System;
using System.Collections.Generic;
using PureMVC.Interfaces;

namespace PureMVC.Core
{
    /// <summary>
    ///   Model层具体实现，单例,线程安全
    /// </summary>
    public class Model:IModel
    {
        //method
        static Model()
        {

        }
        protected Model()
        {
            m_proxyMap = new Dictionary<string, IProxy>();
            InitModel();
        }

        /// <summary>
        ///   instance接口
        /// </summary>
        public static IModel Instance
        {
            get
            {
                lock (m_staticSyncLocker)
                {
                    if (m_instance == null)
                    {
                        m_instance = new Model();
                    }
                }

                return m_instance;
            }
        }

        /// <summary>
        ///   初始化
        /// </summary>
        /// 
        protected virtual void InitModel()
        {

        }
        //实现父类的方法
        public virtual void RegisterProxy(IProxy proxy)
        {
            lock (m_proxySyncLocker)
            {
                m_proxyMap[proxy.ProxyName] = proxy;
            }

            proxy.OnRegister();//注册完回调
        }

        //获取
        public virtual IProxy RetrieveProxy(string proxyName)
        {
            lock (m_proxySyncLocker)
            {
                if (!m_proxyMap.ContainsKey(proxyName))
                    return null;

                return m_proxyMap[proxyName];
            }
        }

        //移除
        public virtual IProxy RemoveProxy(string proxyName)
        {
            IProxy proxy = null;
            lock (m_proxySyncLocker)
            {
                //移除
                proxy = RetrieveProxy(proxyName);

                if (proxy != null)
                {
                    m_proxyMap.Remove(proxyName);
                }
            }

            if (proxy != null)
            {
                proxy.OnRemove();   //移除回调
            }

            return proxy;
        }

        //包含
        public virtual bool HasProxy(string proxyName)
        {
            lock (m_proxySyncLocker)
            {
                return m_proxyMap.ContainsKey(proxyName);
            }
        }
        //member

        /// <summary>
        ///   单例
        /// </summary>
        protected static volatile IModel m_instance; 

        /// <summary>
        ///   instance lock
        /// </summary>
        /// 
        protected static readonly object m_staticSyncLocker = new object();

        /// <summary>
        ///   proxy locker
        /// </summary>
        /// 
        protected readonly object m_proxySyncLocker = new object();

        /// <summary>
        ///   proxy映射表
        /// </summary>
        /// 
        protected Dictionary<string, IProxy> m_proxyMap;
    }
}
