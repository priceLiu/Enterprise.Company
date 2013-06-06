using System;

namespace CN100.EnterprisePlatform.ORM
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct,
	                AllowMultiple = true, Inherited = false)]
	public class MapAttribute : ColumnAttribute
	{
		private string field;
		private bool pk;
		private bool id;
		
		public MapAttribute(string field) : base()
		{
			this.field = field;
		}
		
		public string Field
		{
			get { return field; }
		}
		
		public bool PK
		{
			get { return pk; }
			set { pk = value; }
		}
		
		public bool ID
		{
			get { return id; }
			set { id = value; }
		}
	}
}
