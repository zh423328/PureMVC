using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Core;


namespace PureMVC.Patterns
{
    //单例管理
    public class  Facade :IFacade
    {
        protected Facade()
        {
            InitFacade();
        }

        static Facade()
        {

        }

        public static IFacade Instance
        {
            get
            {
                lock (m_staticSyncLocker)
                {
                    if (m_instance == null)
                    {
                        m_instance = new Facade();
                    }
                }

                return m_instance;
            }
        }

        #region Proxy
        //注册
        public virtual void RegisterProxy(IProxy proxy)
        {
            m_model.RegisterProxy(proxy);
        }
        //获取
        public virtual IProxy RetrieveProxy(string proxyName)
        {
            return m_model.RetrieveProxy(proxyName);
        }
        //移除
        public virtual IProxy RemoveProxy(string proxyName)
        {
            return m_model.RemoveProxy(proxyName);
        }
        //检测
        public virtual bool HasProxy(string proxyName)
        {
            return m_model.HasProxy(proxyName);
        }
        #endregion

        #region COMMAND
        public virtual void RegisterCommand(string notificationName, Type commandType)
        {
            m_controller.RegisterCommand(notificationName, commandType);
        }
        public virtual void RemoveCommand(string notificationName)
        {
            m_controller.RemoveCommand(notificationName);
        }
        public virtual bool HasCommand(string notificationName)
        {
            return m_controller.HasCommand(notificationName);
        }
        #endregion

        #region Mediator
        public virtual void RegisterMediator(IMediator mediator)
        {
            m_view.RegisterMediator(mediator);
        }
        public virtual IMediator RetrieveMediator(string mediatorName)
        {
            return m_view.RetrieveMediator(mediatorName);
        }
        public virtual IMediator RemoveMediator(string mediatorName)
        {
            return m_view.RemoveMediator(mediatorName);
        }
        public virtual bool HasMediator(string mediatorName)
        {
            return m_view.HasMediator(mediatorName);
        }
        #endregion

        #region Observer
        public virtual void NotifyObservers(INotification note)
        {
            m_view.NotifyObservers(note);
        }
        #endregion

        #region INotifier Members

        //发送通知
        public virtual void SendNotification(string notificationName)
        {
            NotifyObservers(new Notification(notificationName));
        }

        //发送通知
        public virtual void SendNotification(string notificationName, object body)
        {
            NotifyObservers(new Notification(notificationName, body));
        }

        public virtual void SendNotification(string notificationName, object body, string type)
        {
            NotifyObservers(new Notification(notificationName, body, type));
        }

        #endregion

        protected virtual void InitModel()
        {
            if (m_model != null)
                return;
            m_model = Model.Instance;
        }

        protected virtual void InitView()
        {
            if (m_view != null)
                return;

            m_view = View.Instance;
        }

        protected virtual void InitController()
        {
            if (m_controller != null)
                return;
            m_controller = Controller.Instance;
        }
        protected virtual void InitFacade()
        {
            InitModel();
            InitView();
            InitController(); ;
        }

        //成员
        
        //mvc
        protected IController m_controller;
        protected IModel m_model;
        protected IView m_view;

        protected static volatile IFacade m_instance;

        protected static readonly object m_staticSyncLocker = new object();
    }
}
