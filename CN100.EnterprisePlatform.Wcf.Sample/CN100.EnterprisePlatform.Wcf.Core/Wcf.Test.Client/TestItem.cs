using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wcf.Test.Client
{
    class TestItem
    {
        public string Name
        {
            get;
            set;
        }
        public System.Threading.WaitCallback Action
        {
            get;
            set;
        }
    }
}
