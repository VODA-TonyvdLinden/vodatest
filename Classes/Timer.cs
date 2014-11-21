using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Diagnostics;

namespace TestProj.Classes
{
    public class Timer : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            string sender = string.Format("{0}_{1}", input.MethodBase.ReflectedType.Name, input.MethodBase.Name);

            IMethodReturn msg;
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            msg = getNext()(input, getNext);

            sw.Stop();

            long timeElapsed = sw.ElapsedMilliseconds;
            long seconds = timeElapsed / 1000;

            LogWriter.Instance.Log(string.Format("{0} executed in {1} seconds", sender, seconds), LogWriter.eLogType.Info);

            return msg;
        }
        public bool WillExecute
        {
            get { return true; }
        }
    }
}
