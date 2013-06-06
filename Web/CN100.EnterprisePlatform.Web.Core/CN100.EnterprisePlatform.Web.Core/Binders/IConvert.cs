using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
namespace CN100.EnterprisePlatform.Web.Core.Binders
{
    public interface IConvert
    {
        object Parse(NameValueCollection data, string key, string prefix, out bool succeed);
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public class ConvertAttribute : Attribute
    {
        public ConvertAttribute()
        {
            
        }
        public ConvertAttribute(Type convert)
        {
            Convert = convert;
        }
        public Type Convert
        {
            get;
            set;
        }
    }
    //[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    //public class ConvertAttribute<T> : Attribute where T : IConvert
    //{
    //    public ConvertAttribute()
    //    {

    //    }
    //}
}
