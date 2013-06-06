using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Reflection;
using Smark.Core;
namespace CN100.EnterprisePlatform.Web.Core.Binders
{
    public class ConverCore
    {
        internal static string GetValue(System.Collections.Specialized.NameValueCollection data, string key, string prefix)
        {
            string value = data[key];
            if (string.IsNullOrEmpty(prefix))
                return value;
            if (string.IsNullOrEmpty(value))
                value = data[prefix + "." + key];
            if (string.IsNullOrEmpty(value))
                value = data[prefix + "_" + key];
            if (string.IsNullOrEmpty(value))
                value = data[prefix  + key];
            return value;
        }
        internal static string[] GetValues(System.Collections.Specialized.NameValueCollection data, string key, string prefix)
        {
            string[] value = data.GetValues(key);
            if (string.IsNullOrEmpty(prefix))
                return value;
            if(value.Length ==0)
                value = data.GetValues(prefix + "." + key);
            if (value.Length == 0)
                value = data.GetValues(prefix + "_" + key);
            if (value.Length == 0)
                value = data.GetValues(prefix + key);
            return value;
        }
        private static IDictionary<Type, IConvert> mConverts;
        internal static IDictionary<Type, IConvert> Converts
        {
            get
            {
                if (mConverts == null)
                {
                    LoadConverts();
                }
                return mConverts;
            }
        }
        private static void LoadConverts()
        {
            lock (typeof(ConverCore))
            {
                if (mConverts == null)
                {
                    mConverts = new Dictionary<Type, IConvert>();
                    LoadBaseConvert();
                    
                }
            }
        }
        private static void AddConvert(Type convert, ConvertAttribute[] ca)
        {
            if (ca.Length > 0)
            {
                foreach (ConvertAttribute item in ca)
                {
                    if (Converts.ContainsKey(item.Convert))
                    {
                        Converts[item.Convert] = (IConvert)Activator.CreateInstance(convert);
                    }
                    else
                    {
                        Converts.Add(item.Convert, (IConvert)Activator.CreateInstance(convert));
                    }
                }
            }
        }
        private static void LoadBaseConvert()
        {
            Assembly ass = typeof(ConverCore).Assembly;
            LoadConvertByAssembly(ass);
        }
        private static void LoadConvertByAssembly(Assembly ass)
        {
            foreach (Type item in ass.GetTypes())
            {
                if (item.GetInterface("CN100.EnterprisePlatform.Web.Core.Binders.IConvert") != null)
                {
                    AddConvert(item, Functions.GetTypeAttributes<ConvertAttribute>(item, false));
                }
            }
        }
        public static void AddCustomConvert(params Assembly[] assemblies)
        {
            foreach (Assembly item in assemblies)
            {
                LoadConvertByAssembly(item);
            }
        }
        static Dictionary<Type, ClassBinder> mClassBinders = new Dictionary<Type, ClassBinder>();
        internal static ClassBinder GetBinder(Type type)
        {
            if (type.GetConstructor(new Type[0]) == null)
                throw new Exception(string.Format("{0}不存在默认构造函数,无法构建数据绑定!", type.Name));
            if (mClassBinders.ContainsKey(type))
                return mClassBinders[type];
            else
            {
                lock (mClassBinders)
                {
                    
                    if (!mClassBinders.ContainsKey(type))
                        mClassBinders.Add(type, new ClassBinder(type));
                    return mClassBinders[type];

                }
            }
        }
    }
}
