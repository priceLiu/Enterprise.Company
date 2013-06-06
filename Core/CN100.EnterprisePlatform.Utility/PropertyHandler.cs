using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace ConsoleApplication1
{
    /// <summary>
    /// 通过表达式树来创建一个属性调用代理
    /// <example>
    /// <code>
    ///     PropertyInfo pi = typeof(Program).GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
    ///     PropertyHandler handler = new PropertyHandler(pi);
    ///     Program obj = new Program();
    ///     handler.Set(obj, "sdfsdf");
    ///     string value = (string)handler.Get(obj);
    /// </code>
    /// </example>
    /// </summary>
    public class PropertyHandler
    {
        public PropertyHandler(PropertyInfo pi)
        {
            Get = InvokeFactory.PropertyGetValueHandler(pi);
            Set = InvokeFactory.PropertySetValueHandler(pi);
        }

        public Func<object, object> Get
        {
            get;
            set;
        }

        public Action<object, object> Set
        {
            get;
            set;
        }
    }
}
