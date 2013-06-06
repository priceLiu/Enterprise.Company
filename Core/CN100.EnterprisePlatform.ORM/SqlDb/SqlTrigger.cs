using System.Reflection;

namespace CN100.EnterprisePlatform.ORM.DB
{
	public class SqlTrigger
	{
		private MethodInfo method;
		private Timing timing = Timing.None;
		
		public SqlTrigger(MethodInfo method, Timing timing)
		{
			this.method = method;
			this.timing = timing;
		}
		
		public MethodInfo Method
		{
			get { return method; }
		}
		
		public Timing Timing
		{
			get { return timing; }
		}
		
		public void Fire(object target, object[] args)
		{
			method.Invoke(target, args);
		}
	}
}
