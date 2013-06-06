using System;
using System.Reflection;

namespace CN100.EnterprisePlatform.ORM.DB
{
	public class PropertyBridge : IDataBridge
	{
		private PropertyInfo field;
		
		public PropertyBridge(PropertyInfo field)
		{
			this.field = field;
		}
		
		public bool Readable
		{
			get { return field.CanRead; }
		}
		
		public bool Writeable
		{
			get { return field.CanWrite; }
		}
		
		public Type DataType
		{
			get { return field.PropertyType; }
		}
		
		public object Read(object obj)
		{
            return DynamicMethodCompiler.GetCachedGetPropertyHandlerDelegate(obj.GetType(), field)(obj);
		}

        public void Write(object obj, object val)
        {
            string strType = field.PropertyType.ToString();

            if ((val!=null) && !field.PropertyType.Equals(val.GetType()))
            {
                switch (strType)
                {
                    case "System.String":
                        val = Convert.ToString(val);
                        break;
                    case "System.Int16":
                        val = Convert.ToInt16(val);
                        break;
                    case "System.Int32":
                        val = Convert.ToInt32(val);
                        break;
                    case "System.Int64":
                        val = Convert.ToInt64(val);
                        break;
                    case "System.Decimal":
                        val = Convert.ToDecimal(val);
                        break;
                    case "System.DateTime":
                        val = Convert.ToDateTime(val);
                        break;
                }
            }
            
            DynamicMethodCompiler.GetCachedSetPropertyHandlerDelegate(obj.GetType(), field)(obj, val);
        }
	}
}
