using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.ControlCenter.IServices;
using CN100.ProductDetail.BLL.Model;
using CN100.ProductDetail.Const;

namespace CN100.MSMQ.Handler.Services
{
    public class FreightService : BaseService
    {
        public void SaveFreightTemplate(long ID)
        {
            var key = RedisKeyConst.GetStoreFreightInfoKey(ID).Key;
            using (var client = factory.CreateClient<IFreightTemplateService>())
            {

                var item = client.Channel.GetFreightTemplateDetail(ID);
                if (item != null)
                {
                    CN100.Redis.Client.RedisClientUtility.SetData<IList<FreightTempleteDetailModel>>(key, item);
                }
                else
                {
                    CN100.Redis.Client.RedisClientUtility.Del(key);
                }
            }
        }

        public void SaveAdressBase()
        {
            using (var client = factory.CreateClient<IFreightTemplateService>())
            {

                var item = client.Channel.GetFreightProvince();
                var keyType = RedisKeyConst.GetProvincesKey();
                if (item != null)
                {

                    CN100.Redis.Client.RedisClientUtility.SetData<IList<FreightProvinceModel>>(keyType.Key, item);
                }
                else
                {
                    CN100.Redis.Client.RedisClientUtility.Del(keyType.Key);
                }
            }
        }
    }
}
