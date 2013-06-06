/*********************************************************************
 * Copyright © CN100.COM 2012
 * File: IWcfTcpServer.cs
 * Time: 9/5/2012 3:22:21 PM
 * Author: Sharpish
 * URL:  http://www.cn100.com
 * Description:
 * History:
 * 
 * 
 *********************************************************************/
namespace CN100.EnterprisePlatform.Wcf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IWcfTcpServer : IDisposable
    {
        string Address { get; set; }
        int Port { get; set; }
        void LoadInterfaceAssembly(System.Reflection.Assembly assembly);
        void LoadImplAssembly(System.Reflection.Assembly assembly);
        void Start();
    }
}
