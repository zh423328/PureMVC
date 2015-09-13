using System;
using System.Collections.Generic;
using PureMVC.Interfaces;

namespace PureMVC.Interfaces
{
    //消息接口
    public interface INotification
    {
        //消息名字
        string Name { get; }

        //消息绑定的数据
        object Body { get; set; }

        //消息类型
        string Type { get; set;}

        //转为string
        string ToString();
    }

}
