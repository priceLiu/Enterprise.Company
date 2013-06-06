using System;

namespace CN100.EnterprisePlatform.ORM.DB
{
	public interface ISqlTableFactory
	{
		SqlTable Build(Type type);
	}
}
