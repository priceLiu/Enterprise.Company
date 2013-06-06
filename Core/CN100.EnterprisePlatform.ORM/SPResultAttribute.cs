﻿using System;

namespace CN100.EnterprisePlatform.ORM
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct,
	                AllowMultiple = false, Inherited = false)]
	public class SPResultAttribute : Attribute
	{
		public SPResultAttribute() : base()
		{}
	}
}
