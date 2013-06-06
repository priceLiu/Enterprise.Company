using System;
using System.Collections.Generic;
using System.Text;

namespace CN100.EnterprisePlatform.Web.Core.Binders
{
    public class ValidaterException:Exception
    {
        public ValidaterException() { }
        public ValidaterException(string err) : base(err) { }
        public ValidaterException(string err, Exception baseexc) : base(err, baseexc) { }
    }
}
