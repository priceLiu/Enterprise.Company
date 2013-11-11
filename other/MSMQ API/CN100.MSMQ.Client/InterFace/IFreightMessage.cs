using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.MSMQ.IAPI
{
    public interface IFreightMessage : ICloneable
    {
        /// <summary>店铺收藏变更
        /// </summary>
        /// <param name="storeId"></param>
        void ChangeAdressBase();
        /// <summary>修改店铺运费模板
        /// </summary>
        /// <param name="storeId"></param>
        void ChangeStoreFreightTemplate(Int64 storeId);
    }
}
