using System;
using System.Reflection;

namespace CN100.EnterprisePlatform.ORM.DB
{
	public class FieldBridge : IDataBridge
	{
		private FieldInfo field;
		
		public FieldBridge(FieldInfo field)
		{
			this.field = field;
		}
		
		public bool Readable
		{
			get { return true; }
		}
		
		public bool Writeable
		{
			get { return true; }
		}
		
		public Type DataType
		{
			get { return field.FieldType; }
		}
		
		public object Read(object obj)
		{
            return DynamicMethodCompiler.GetCachedGetFieldHandlerDelegate(obj.GetType(), field)(obj);
		}
		
		public void Write(object obj, object val)
		{
            string strType = field.FieldType.ToString();

            if ((val!=null) && !field.FieldType.Equals(val.GetType()))
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

            DynamicMethodCompiler.GetCachedSetFieldHandlerDelegate(obj.GetType(), field)(obj, val);

		}
	}
}
