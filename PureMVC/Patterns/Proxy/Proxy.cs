using System;
using PureMVC.Interfaces;

namespace PureMVC.Patterns
{
    //代理数据接口
    class Proxy : Notifier,IProxy
    {
        //构造函数
        public Proxy(string name, object data)
        {
            m_nName = name;
            m_data = data;
        }

        public Proxy(string name)
            : this(name, null)
        {

        }

        public Proxy()
            :this(NAME,null)
        {

        }

        //接口实现
        public virtual string ProxyName
        {
            get
            {
                return m_nName;
            }
        }

        public virtual object Data
        {
            get
            {
                return m_data;
            }

            set
            {
                m_data = value;
            }
        }

        public virtual void OnRegister()
        {

        }

        public virtual void OnRemove()
        {

        }

        //成员对象
        protected string m_nName;
        protected object m_data;
        public static string NAME = "Proxy";

    }
}
