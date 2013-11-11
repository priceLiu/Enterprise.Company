using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LH.JsonHelper;
using CN100.ProductDetail.Common.Enums;

namespace CN100.ProductDetail.Common
{
    public interface IPlugin
    {
        /// <summary>
        /// 可接受的参数类型
        /// </summary>
        ModuleEnum Module { get; }

        /// <summary>
        /// 参数处理器
        /// </summary>
        /// <param name="obj">参数</param>
        void Process(JsonObject obj);
        /// <summary>操作
        /// </summary>
        ActionEnum Action { get; set; }
    }
}
