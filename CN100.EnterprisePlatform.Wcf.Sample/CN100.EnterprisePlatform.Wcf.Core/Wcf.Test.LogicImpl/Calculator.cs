using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wcf.Test.LogicInterface;
using System.ServiceModel;
namespace Wcf.Test.LogicImpl
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Calculator : ICalculator
    {
        public double Add(double n1, double n2) { return n1 + n2; }
        public double Subtract(double n1, double n2) { return n1 - n2; }
        public double Multiply(double n1, double n2) { return n1 * n2; }
        public double Divide(double n1, double n2) { return n1 / n2; }
    }
}
