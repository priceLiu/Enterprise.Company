using System;

namespace CN100.EnterprisePlatform.ORM
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
	                Inherited = false, AllowMultiple = false)]
	public class IDAttribute : Attribute
	{
		public IDAttribute() : base()
		{}
	}
}
