using System;
using System.Collections.Generic;
using System.Text;

namespace CN100.EnterprisePlatform.Web.Core.Binders
{
    [AttributeUsage(AttributeTargets.Property| AttributeTargets.Parameter)]
    public class BindAttribute:Attribute
    {
        public string Prefix
        {
            get;
            set;
        }
        public Type Convert
        {
            get;
            set;
        }
        public Type Fungible
        {
            get;
            set;
        }
        public IConvert GetConvert()
        {
            if (Convert == null)
                return null;
            return (IConvert)Activator.CreateInstance(Convert);
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class ViewStateAttribute : Attribute
    {
        public bool ByPostData
        {
            get;
            set;
        }
    }
}
