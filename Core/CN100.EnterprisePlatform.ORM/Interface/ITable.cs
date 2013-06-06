using System;

namespace CN100.EnterprisePlatform.ORM
{
	public interface ITable
	{		
		Type Class { get; }
		string Name { get; }
		string Schema { get; }
		IColumn[] Columns { get; }
	}
}
