// -----------------------------------------------------------------------
// <copyright file="MQConfigHelper.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CN100.EnterprisePlatform.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Guid("3F66EC71-2160-4F83-BCAB-B75957463F2E")]
    public class MQConfigHelper
    {
        #region MQQualityService
        private MQQualityServiceHelper mMQQualityHelper = new MQQualityServiceHelper();
        public MQQualityServiceHelper MQQualityHelper
        {
            get
            {
                return mMQQualityHelper;
            }
        }
        public  class MQQualityServiceHelper
        {
            public  string ServiceName
            {
                get
                {
                    return MqSection.Instance.Hosts.GetItemByKey(ConstManager.MQConstManager.MQQUALITYSERVICE).ServiceName;
                }
            }

            public  string Ip
            {
                get
                {
                    return MqSection.Instance.Hosts.GetItemByKey(ConstManager.MQConstManager.MQQUALITYSERVICE).Ip;
                }
            }

            public  int Port
            {
                get
                {
                    return MqSection.Instance.Hosts.GetItemByKey(ConstManager.MQConstManager.MQQUALITYSERVICE).Port;
                }
            }
        }
        #endregion

        #region MQConfigHelper
       

        #endregion
    }
}
