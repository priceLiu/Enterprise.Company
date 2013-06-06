using System;

namespace CN100.EnterprisePlatform.ORM
{
	public interface IColumn
	{
		ITable Table { get; }
		string Name { get; }
		Type DataType { get; }
	}
}
