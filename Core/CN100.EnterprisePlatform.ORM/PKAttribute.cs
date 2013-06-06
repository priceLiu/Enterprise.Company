using System;

namespace CN100.EnterprisePlatform.ORM
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
	                Inherited = false, AllowMultiple = false)]
	public class PKAttribute : Attribute
	{
		public PKAttribute() : base()
		{}
	}
}
