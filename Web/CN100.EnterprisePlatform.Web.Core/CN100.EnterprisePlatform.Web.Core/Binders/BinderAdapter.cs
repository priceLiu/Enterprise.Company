using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Reflection;
namespace CN100.EnterprisePlatform.Web.Core.Binders
{
    
 
    public class BinderAdapter
    {
        static BinderAdapter()
        {
            AddCustomConvert(typeof(BinderAdapter).Assembly);
        }
        static Dictionary<Type, IConvert> mIListConvert = new Dictionary<Type, IConvert>();
        public static void Full(object obj, NameValueCollection data, string prefix, bool ispostback)
        {
            ClassBinder cb = ConverCore.GetBinder(obj.GetType());
            cb.FullData(obj, data, prefix,ispostback);
        }
        static IConvert GetListConvert<T>() where T:new()
        {
            lock (mIListConvert)
            {
                IConvert convert=null;
                Type key = typeof(T);
                if (!mIListConvert.TryGetValue(key, out convert))
                {
                    convert = new ToIList<T>();
                    mIListConvert.Add(key, convert);
                }
                return convert;
            }
        }
        public static IList<T> FullList<T>(NameValueCollection data, string prefix) where T:new()
        {
            IConvert convert = GetListConvert<T>();
            bool succeed;
            return (IList<T>)convert.Parse(data, "", prefix, out succeed);
        }
        public static object CreateInstance(Type type, NameValueCollection data)
        {
            return CreateInstance(type, data, null);
        }
        public static object CreateInstance(Type type, NameValueCollection data, string prefix)
        {
            if (ConverCore.Converts.ContainsKey(type))
            {
                IConvert convert = ConverCore.Converts[type];
                bool succed;
                return convert.Parse(data, null, prefix, out succed);
            }
            ClassBinder cb = ConverCore.GetBinder(type);
            return cb.CreateObject(data, prefix);
            
        }
        public static T CreateInstance<T>(NameValueCollection data, string prefix) where T : new()
        {
            return (T)CreateInstance(typeof(T), data, prefix);
        }
        public static T CreateInstance<T>(NameValueCollection data) where T : new()
        {
            return CreateInstance<T>(data, null);
        }
        public static void AddCustomConvert(params Assembly[] assemblies)
        {
            ConverCore.AddCustomConvert(assemblies);
        }
        public static IList<ValidaterInfo> Validating(object obj)
        {
            ClassBinder cb = ConverCore.GetBinder(obj.GetType());
            return cb.Validating(obj);
        }
        public static IList<ValidaterInfo> Validating(object obj, bool throwerr)
        {
            ClassBinder cb = ConverCore.GetBinder(obj.GetType());
            IList<ValidaterInfo> result= cb.Validating(obj);
            if (throwerr)
            {
                foreach (ValidaterInfo vi in result)
                {
                    if (vi.State == ValidaterState.Error)
                        throw new ValidaterException(vi.Message);
                }
            }
            return result;
        }

    }



}
