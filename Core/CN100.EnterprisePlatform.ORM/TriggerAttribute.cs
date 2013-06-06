using System;

namespace CN100.EnterprisePlatform.ORM
{
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public class TriggerAttribute : Attribute
	{
		private Timing timing = Timing.None;
		
		public TriggerAttribute(Timing timing) : base()
		{
			this.timing = timing;
		}
		
		public Timing Timing
		{
			get { return timing; }
		}
	}
}
