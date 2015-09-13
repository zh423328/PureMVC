using System;

namespace PureMVC.Interfaces
{
    /// <summary>
    /// 数据代理
    /// </summary>
    public interface IProxy
    {
        /// <summary>
        /// 代理名字
        /// </summary>
        string ProxyName { get; }

        /// <summary>
        /// 数据存储
        /// </summary>
        object Data { get; set; }

        /// <summary>
        /// 当Model层注册Proxy时调用
        /// </summary>
        void OnRegister();

        /// <summary>
        /// 当Model层删除Proxy时调用
        /// </summary>
        void OnRemove();
    }
}
