using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
namespace ConsoleApplication1
{
    
    public class InvokeFactory
    {
        static Dictionary<Type, Func<object>> mHandlers = new Dictionary<Type, Func<object>>();

        static Dictionary<PropertyInfo, Func<object, object>> mPropertyGetValueHandlers = new Dictionary<PropertyInfo, Func<object, object>>();

        static Dictionary<PropertyInfo, Action<object, object>> mPropertySetValueHandlers = new Dictionary<PropertyInfo, Action<object, object>>();

        public static Func<object> InstanceHandler(Type type)
        {
            Func<object> result;
            lock (mHandlers)
            {
                if (!mHandlers.TryGetValue(type, out result))
                {


                    result = Expression.Lambda<Func<object>>(Expression.New(type)).Compile();
                    mHandlers.Add(type, result);
                }
            }
            return result;
        }

        public static Func<object, object> PropertyGetValueHandler(PropertyInfo pi)
        {
            Func<object, object> result;
            lock (mPropertyGetValueHandlers)
            {
                if (!mPropertyGetValueHandlers.TryGetValue(pi, out result))
                {
                    ParameterExpression paramP1 = Expression.Parameter(typeof(Object), "paramP1");
                    Expression convertedParamo = Expression.Convert(paramP1, pi.ReflectedType);
                    Expression invoke = Expression.Property(convertedParamo, pi.Name);
                    LambdaExpression lambda = Expression.Lambda<Func<Object, object>>(invoke, paramP1);
                    Expression<Func<Object, object>> dynamicSetterExpression = (Expression<Func<Object,object>>)lambda;
                    result= dynamicSetterExpression.Compile();
                    mPropertyGetValueHandlers.Add(pi, result);
                }
            }
            return result;
                
        }

        public static Action<object, object> PropertySetValueHandler(PropertyInfo pi)
        {
            Action<object, object> result = null;
            if (pi.CanWrite)
            {
                lock (mPropertySetValueHandlers)
                {
                    if (!mPropertySetValueHandlers.TryGetValue(pi, out result))
                    {
                        ParameterExpression pobj = Expression.Parameter(typeof(Object), "obj");
                        ParameterExpression pvalue = Expression.Parameter(typeof(Object), "value");
                        Expression invoke = Expression.Assign(
                            Expression.Property(Expression.Convert(pobj, pi.ReflectedType), pi.Name),
                            Expression.Convert(pvalue, pi.PropertyType));
                        LambdaExpression lambda = Expression.Lambda<Action<object, object>>(invoke,pobj,pvalue);
                        Expression<Action<object, object>> dynamicSetterExpression = (Expression<Action<object, object>>)lambda;
                        result = dynamicSetterExpression.Compile();
                        mPropertySetValueHandlers.Add(pi, result);
                    }
                }
            }
            return result;
        }
        
    }
}
