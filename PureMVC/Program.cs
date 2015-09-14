using System;
using System.Collections.Generic;
using PureMVC.Patterns;
using PureMVC.Interfaces;

namespace PureMVC
{
    class NotifierString
    {
        public const string STARTUP = "startup";
        public const string ADDUSER = "add";
        public const string DELETEUSER = "delete";
    }
    class StartupCommand : SimpleCommand, ICommand
    {
        public override void Execute(INotification noftication)
        {
            base.Execute(noftication);

            Facade.RegisterProxy(new UserProxy());
            //执行
            Program cs = (Program)noftication.Body;

            Facade.RegisterMediator(new UsetListMediator(cs.form));
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
    class UserProxy : Proxy
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

    //mediator视图管理，
    //最先操作，然后传个给mediator,（而mediator很早就注册了meditor操作的方法）
    class UserListForm
    {
        //接收操作
        public Action<int> m_pOperator;

        public void Operator(int i)
        {
            if (m_pOperator != null)
            {
                //
                m_pOperator(i);     //操作
            }
        }
    }

    class UsetListMediator : Mediator
    {
        private UserProxy proxy;

        public new const string NAME = "UseListMediator";

        public UsetListMediator(UserListForm form)
            : base(NAME, form)
        {
            form.m_pOperator += Operator;       //操作方法
        }


        public void Operator(int i)
        {
            //真实调用
            char ch = (char)i;
            switch (ch)
            {
                case 'a':
                    proxy.AddUser(new UserData("user" + i, "123456"));
                    break;
                case 'b':
                    Facade.SendNotification(NotifierString.ADDUSER);
                    break;
                case 'd':
                    Facade.SendNotification(NotifierString.DELETEUSER);
                    break;
            }
            
        }


        private UserListForm UseList
        {
            get
            {
                return (UserListForm)ViewComponent; //获取list
            }
        }


        //操作方法,注册时重载
        public override IList<string> ListNotificationInterests()
        {
            IList<string> list = new List<string>();
            list.Add(NotifierString.ADDUSER);
            list.Add(NotifierString.DELETEUSER);
            return list;
        }

        //获取注册数据
        public override void OnRegister()
        {
            base.OnRegister();
            proxy = (UserProxy)Facade.RetrieveProxy(UserProxy.NAME);
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case NotifierString.ADDUSER:
                    {
                        proxy.AddUser(new UserData("user123456", "123456"));
                    }
                    break;
                case NotifierString.DELETEUSER:
                    {
                        proxy.DeleteUser(new UserData("user123456", "123456"));
                    }
                    break;
            }
        }
    }

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
        public UserListForm form = new UserListForm();

        static void Main(string[] args)
        {
            Program cs = new Program();
            
            ApplicationFacade facade = (ApplicationFacade)ApplicationFacade.Instance;
            facade.Startup(cs);

            //读取
            int ch = 0;

            while((ch = Console.Read()) != 9)
            {
                cs.form.Operator(ch);
            }
           // Console.Read();
        }
    }
}
