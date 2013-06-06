using System;

namespace CN100.EnterprisePlatform.ORM.DB
{
	public interface IDataBridge
	{
		bool Readable { get; }
		bool Writeable { get; }
		object Read(object obj);
		void Write(object obj, object val);
		Type DataType { get; }
	}
}
