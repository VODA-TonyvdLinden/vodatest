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

            Classes.Browser automator = getAutomator(input);
            IMethodReturn msg;
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            takeStartScreenshot(sender, automator);
            sw.Start();

            msg = getNext()(input, getNext);

            sw.Stop();
            takeEndScreenshot(sender, automator);

            long timeElapsed = sw.ElapsedMilliseconds;
            long seconds = timeElapsed / 1000;

            LogWriter.Instance.Log(string.Format("{0} executed in {1} seconds", sender, seconds), LogWriter.eLogType.Info);

            return msg;
        }

        private static void takeStartScreenshot(string sender, Classes.Browser automator)
        {
            if (Properties.Settings.Default.TakeMethodStartScreenShots)
                automator.Instance.TakeScreenshot(string.Format("{0}_Enter.png", sender));
        }
        private static void takeEndScreenshot(string sender, Classes.Browser automator)
        {
            if (Properties.Settings.Default.TakeMethodEndScreenshot)
                automator.Instance.TakeScreenshot(string.Format("{0}_Exit.png", sender));
        }




        public bool WillExecute
        {
            get { return Properties.Settings.Default.TakeMethodStartScreenShots; }
        }

        private Classes.Browser getAutomator(IMethodInvocation input)
        {
            for (int i = 0; i < input.Arguments.Count; i++)
            {
                if (input.Arguments[i].GetType() == typeof(Classes.Browser))
                {
                    return (Classes.Browser)input.Arguments[i];
                }
            }
            return null;
        }

        //private string getParameterInfo(IMethodInvocation input)
        //{
        //    string ret = "";
        //    for (int i = 0;i < input.Arguments.Count;i++)
        //    {
        //        ret += input.Arguments.GetParameterInfo(i).Name + " - " + input.Arguments[i] + " | ";
        //    }
        //    return ret;
        //}
    }
}
