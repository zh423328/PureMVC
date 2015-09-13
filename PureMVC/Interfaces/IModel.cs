using System;

namespace PureMVC.Interfaces
{
    /// <summary>
    /// 数据层接口
    /// </summary>
    public interface IModel
    {
       /// <summary>
       ///  注册数据代理 
       /// </summary>
       void RegisterProxy(IProxy proxy);

        /// <summary>
        ///   获取代理接口
        /// </summary>
       IProxy RetrieveProxy(string proxyName);

        /// <summary>
        ///   移除代理类
        /// </summary>
       IProxy RemoveProxy(string proxyName);

        /// <summary>
        ///   是否包含代理
        /// </summary>
       bool HasProxy(string proxyName);
    }
}
