using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using CN100.MSMQ.IAPI;
using CN100.MSMQ.Configration;
using System.Configuration;

namespace CN100.MSMQ.APIFactory
{



    public class APIFactory
    {
        public static APIAssemblySection Configration;
        private static IActivityMessage _ActivityMessage;
        private static IProductMessage _ProductMessage;
        private static IOrderMessage _OrderMessage;
        private static IFreightMessage _FreightMessage;
        private static IStoreMessage _StoreMessage;
        private static string Path = "CN100.MSMQ.API";
        private static Dictionary<string, object> CacheDic = new Dictionary<string, object>();
        private static readonly object Locker = new object();
        /// <summary>API工厂
        /// </summary>
        /// <exception cref="ConfigurationErrorsException">未能加载配置文件</exception>
        static APIFactory()
        {
            try
            {
                Configration = (APIAssemblySection)ConfigurationManager.GetSection("ApiSection");
            }
            catch (ConfigurationErrorsException ex)
            {

                throw new ConfigurationErrorsException("未能加载配置文件，请检查配置文件节点ApiSection配置是否正确", ex.InnerException);
            }

        }

        /// <summary>创建接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">全类名（命名空间+类名）</param>
        /// <returns></returns> 
        /// <exception cref="System.IO.FileNotFoundException"> 程序集未找到</exception>
        /// <exception cref="System.FileLoadException">程序集未能加载</exception>
        /// <exception cref="MissingMethodException">为找到匹配的构造函数</exception> 
        public static T CreateInterface<T>(string fullClassName)
        {

            return (T)CreateObject(fullClassName);
        }
        /// <summary> 创建接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyString">程序集长名称</param>
        /// <param name="fullClassName">全类名（命名空间+类名）</param>
        /// <returns></returns>
        public static T CreateInterface<T>(string assemblyString, string fullClassName)
        {
            if (string.IsNullOrWhiteSpace(assemblyString))
            {
                throw new ArgumentOutOfRangeException(assemblyString, "程序集长名称无效");
            }

            if (string.IsNullOrWhiteSpace(fullClassName))
            {
                throw new ArgumentOutOfRangeException(fullClassName, "全类名无效");
            }

            Path = assemblyString;
            return (T)CreateObject(fullClassName);
        }

        #region 实现接口
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strCacheKey"></param>
        /// <returns></returns>
        ///     
        static object CreateObject(string strCacheKey)
        {
            //测试阶段先要去掉缓存
            //DeleteCache(strCacheKey);
            //   DbFactory.Instance.EnableOdpNet = true;

            object objType = null;
            if (CacheDic.ContainsKey(strCacheKey))
            {
                objType = CacheDic[strCacheKey];//从缓存读取
            }
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(Path).CreateInstance(strCacheKey);//反射创建
                    lock (Locker)
                    {
                        if (CacheDic.ContainsKey(strCacheKey))
                        {
                            CacheDic[strCacheKey] = objType;
                        }
                        else
                        {
                            CacheDic.Add(strCacheKey, objType);// 写入缓存
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }



            object objNEW = ((ICloneable)objType).Clone();
            return objNEW;
        }
        #endregion

        /// <summary>获取活动接口
        /// </summary>
        public static IActivityMessage ActivityMessage
        {
            get
            {
                if (_ActivityMessage == null)
                {
                    var ActivityConfig = Configration.Assemblies[APIAssemblyConst.Activity];
                    if (ActivityConfig == null)
                    {
                        _ActivityMessage = APIFactory.CreateInterface<IActivityMessage>("CN100.MSMQ.API.NoReturn.ActivityMessage");
                    }
                    else
                    {
                        _ActivityMessage = APIFactory.CreateInterface<IActivityMessage>(ActivityConfig.DLLName, ActivityConfig.FullClassName);
                    }

                }
                return _ActivityMessage;
            }
        }

        /// <summary>获取产品接口
        /// </summary>
        public static IProductMessage ProductMessage
        {
            get
            {
                if (_ProductMessage == null)
                {
                    var Config = Configration.Assemblies[APIAssemblyConst.Product];
                    if (Config == null)
                    {
                        _ProductMessage = APIFactory.CreateInterface<IProductMessage>("CN100.MSMQ.API.NoReturn.ProductMessage");

                    }
                    else
                    {
                        _ProductMessage = APIFactory.CreateInterface<IProductMessage>(Config.DLLName, Config.FullClassName);
                    }
                }
                return _ProductMessage;
            }
        }

        /// <summary>获取订单接口
        /// </summary>
        public static IOrderMessage OrderMessage
        {
            get
            {
                if (_OrderMessage == null)
                {
                    var Config = Configration.Assemblies[APIAssemblyConst.Order];
                    if (Config == null)
                    {
                        _OrderMessage = APIFactory.CreateInterface<IOrderMessage>("CN100.MSMQ.API.NoReturn.OrderMessage");
                    }
                    else
                    {
                        _OrderMessage = APIFactory.CreateInterface<IOrderMessage>(Config.DLLName, Config.FullClassName);
                    }
                }
                return _OrderMessage;
            }
        }

        /// <summary>获取运费接口
        /// </summary>
        public static IFreightMessage FreightMessage
        {
            get
            {
                if (_FreightMessage == null)
                {
                    var Config = Configration.Assemblies[APIAssemblyConst.Freight];
                    if (Config == null)
                    {

                        _FreightMessage = APIFactory.CreateInterface<IFreightMessage>("CN100.MSMQ.API.NoReturn.FreightMessage");
                    }
                    else
                    {
                        _FreightMessage = APIFactory.CreateInterface<IFreightMessage>(Config.DLLName, Config.FullClassName);
                    }
                }
                return _FreightMessage;
            }
        }
        public static IStoreMessage StoreMessage
        {
            get
            {
                if (_StoreMessage == null)
                {
                    var Config = Configration.Assemblies[APIAssemblyConst.Store];
                    if (Config == null)
                    {
                        _StoreMessage = APIFactory.CreateInterface<IStoreMessage>("CN100.MSMQ.API.NoReturn.StoreMessage");
                    }
                    else
                    {
                        _StoreMessage = APIFactory.CreateInterface<IStoreMessage>(Config.DLLName, Config.FullClassName);
                    }
                }
                return _StoreMessage;
            }
        }


    }
}
