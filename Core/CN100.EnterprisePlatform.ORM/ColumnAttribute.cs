using System;

namespace CN100.EnterprisePlatform.ORM
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,
	                Inherited = false, AllowMultiple = false)]
	public class ColumnAttribute : Attribute
	{
		private string name;
		private string alias;
		
		public ColumnAttribute() : base()
		{}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		
		public string Alias
		{
			get { return alias; }
			set { alias = value; }
		}
	}
}
