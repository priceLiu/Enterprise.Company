using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.ProductDetail.Common.Enums;
using CN100.MSMQ.IAPI;

namespace CN100.MSMQ.API
{
    /// <summary>运费模板相关消息发送器
    /// </summary>
    public class FreightMessage : Client,IFreightMessage
    {

        /// <summary>店铺收藏变更
        /// </summary>
        /// <param name="storeId"></param>
        public void ChangeAdressBase()
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Freight, string.Format(ArgumentFormart, 0, ActionEnum.ChangeAdressBase)));
        }
        /// <summary>修改店铺运费模板
        /// </summary>
        /// <param name="storeId"></param>
        public void ChangeStoreFreightTemplate(Int64 storeId)
        {
            SendRequest(string.Format(ConstClass.CommandFormart, ModuleEnum.Freight, string.Format(ArgumentFormart, storeId, ActionEnum.ChangeFreightTemplate)));
        }

        public object Clone()
        {
            return new FreightMessage();
        }
    }
}
