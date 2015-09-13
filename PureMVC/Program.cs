using System;
using System.Collections.Generic;
using PureMVC.Patterns;
using PureMVC.Interfaces;

namespace PureMVC
{
    class NotifierString
    {
        public const string STARTUP = "startup";
    }
    class StartupCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification noftication)
        {
            base.Execute(noftication);

            //基类执行
            Console.WriteLine(noftication.ToString());
        }
    }
    class UserData
    {
        public UserData(string user, string pwd)
        {
            szUser = user;
            szPwd = pwd;
        }
        public string szUser;
        public string szPwd;
    }

    //proxy
    class UserProxy : Proxy, IProxy
    {
        public new const string NAME = "UserProxy";

        public UserProxy()
            : base(NAME, new List<UserData>())
        {
            //list数据保存
            AddUser(new UserData("ch001", "123456"));
            AddUser(new UserData("ch002", "123456"));
            AddUser(new UserData("ch002", "123456"));
            AddUser(new UserData("ch002", "123456"));
        }

        //数据列表
        public IList<UserData> Users
        {
            get
            {
                return (IList<UserData>)Data;
            }
        }

        //添加用户
        public void AddUser(UserData user)
        {
            Users.Add(user);//添加用户
        }

        //删除用户
        public void DeleteUser(UserData user)
        {
            for (int i = 0; i < Users.Count;++i )
            {
                UserData dUser = Users[i];
                if (dUser != null && dUser.szUser == user.szUser)
                {
                    Users.RemoveAt(i);
                    break;
                }
            }
        }
    }

    //mediator视图管理

    class ApplicationFacade : Facade,IFacade
    {
        protected ApplicationFacade()
        {

        }

        static ApplicationFacade()
        {

        }

        public new static IFacade Instance
        {
            get
            {
                lock (m_staticSyncLocker)
                {
                    if (m_instance == null)
                    {
                        m_instance = new ApplicationFacade();
                    }

                    return m_instance;
                }
            }
        }

        protected override void InitController()
        {
            //注册command
            base.InitController();
            RegisterCommand(NotifierString.STARTUP, typeof(StartupCommand));
        }

        public void Startup(object obj)
        {
            //开始执行
            SendNotification(NotifierString.STARTUP, obj);  //执行
        }
    }

    class Program
    {
        public int i = 10;
        static void Main(string[] args)
        {
            Program cs = new Program();
            ApplicationFacade facade = (ApplicationFacade)ApplicationFacade.Instance;
            facade.Startup(cs);

            Console.Read();
        }
    }
}
