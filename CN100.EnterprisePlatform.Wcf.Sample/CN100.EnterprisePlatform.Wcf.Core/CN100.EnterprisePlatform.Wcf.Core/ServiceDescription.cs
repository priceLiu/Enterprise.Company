using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace CN100.EnterprisePlatform.Wcf.Core
{
    /// <summary>
    /// 服务描述
    /// </summary>
    public class ServiceDescription
    {
        /// <summary>
        /// 服务类型接口
        /// </summary>
        public Type IType
        {
            get;
            set;
        }

        /// <summary>
        /// 服务类型接口实现类
        /// </summary>
        public Type ImplType
        {
            get;
            set;
        }

        /// <summary>
        /// 对应的ServiceHost
        /// </summary>
        public ServiceHost Host
        {
            get;
            set;
        }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }
    }
}
