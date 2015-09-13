using System;
using System.Collections.Generic;
using PureMVC.Interfaces;

namespace PureMVC.Patterns
{
    //消息体的具体实现
    public class Notification : INotification
    {
        //构造体
        public Notification(string name, object body, string type)
        {
            m_nName = name;
            m_body = body;
            m_nType = type;
        }

        public Notification(string name)
            : this(name, null, null)
        {

        }

        public Notification(string name,object body)
            : this(name, body, null)
        {

        }

        //接口具体实现
        public virtual string ToString()
        {
            string msg = "Notification Name:" + m_nName;
            msg += "\nBody:" + ((m_body == null) ? "null" : m_body.ToString());
            msg += "\nType:" + ((m_nType == null) ? "null" : m_nType);
            return msg;
        }

        public virtual string Name
        {
            get
            {
                return m_nName;
            }
        }

        public virtual object Body
        {
            get
            {
                return m_body;
            }

            set
            {
                m_body = value;
            }
        }

        public virtual string Type
        {
            get
            {
                return m_nType;
            }

            set
            {
                m_nType = value;
            }
        }
        //成员对象
        protected string m_nName;
        protected object m_body;
        protected string m_nType;
    }
}
