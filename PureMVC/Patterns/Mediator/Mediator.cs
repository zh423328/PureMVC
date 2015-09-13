using System;
using System.Collections.Generic;
using PureMVC.Interfaces;


namespace PureMVC.Patterns
{
    //视图操作接口实现
    public class Mediator:Notifier,IMediator
    {
        //构造函数
        public Mediator(string name, object viewComponent)
        {
            m_MediatorName = name;
            m_viewCompent = viewComponent;
        }

        public Mediator(string name)
            : this(name, null)
        {

        }

        public Mediator()
            : this(NAME, null)
        {

        }

        //父类实现
        public virtual string MediatorName
        {
            get
            {
                return m_MediatorName;
            }
        }

        public virtual object ViewComponent
        {
            get
            {
                return m_viewCompent;
            }

            set
            {
                m_viewCompent = value;
            }
        }

        //list notificationname
        public virtual IList<string> ListNotificationInterests()
        {
            return new List<string>();
        }

        public virtual void HandleNotification(INotification notification)
        {

        }

        //注册回调
        public virtual void OnRegister()
        {

        }
        //移除回调
        public virtual void OnRemove()
        {

        }
        //成员
        public const string NAME = "Mediator";
        protected string m_MediatorName;
        protected object m_viewCompent; //组件（视图）
    }
}
