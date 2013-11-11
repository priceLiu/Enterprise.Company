using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.ProductDetail.Common;
using CN100.ProductDetail.Common.Enums;
using CN100.MSMQ.Handler.Services;

namespace CN100.MSMQ.Handler.Hanlders
{
    public class FreightHanlder : IPlugin
    {
        FreightService freightService;

        public FreightHanlder()
        {
            freightService = new FreightService();
        }
        public ModuleEnum Module
        {
            get { return ModuleEnum.Freight; }
        }

        public void Process(LH.JsonHelper.JsonObject obj)
        {
            if (obj.IsNull || obj.IsEmpty)
            {
                return;
            }
            var ID = obj["ID"].ToLong(0);

            if (obj["Action"].IsNull || obj["Action"].IsEmpty)
            {
                return;
            }

            //try
            //{
            Action = (ActionEnum)Enum.Parse(typeof(ActionEnum), obj["Action"].Value);
            switch (Action)
            {
                case ActionEnum.ChangeFreightTemplate:
                    if (ID <= 0)
                    { return; }
                    freightService.SaveFreightTemplate(ID);
                    break;
                case ActionEnum.ChangeAdressBase:
                    freightService.SaveAdressBase();
                    break;
                default:
                    break;
            }
        }

        public ActionEnum Action
        {
            get;
            set;
        }
    }
}
