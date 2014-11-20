using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace TestProj.Classes
{
    public class ScreenCapture : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            throw new NotImplementedException();
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            string sender = string.Format("{0}_{1}", input.MethodBase.ReflectedType.Name, input.MethodBase.Name);
            //string parameters = getParameterInfo(input);

            IMethodReturn msg;

            Classes.Browser automator = getAutomator(input);
            automator.Instance.TakeScreenshot(string.Format("c:\\{0}_Before.jpg",sender));
            msg = getNext()(input,getNext);
            automator.Instance.TakeScreenshot(string.Format("c:\\{0}_After.jpg", sender));
            return msg;
        }

        public bool WillExecute
        {
            get { return Properties.Settings.Default.TakeScreenShots; }
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

        private string getParameterInfo(IMethodInvocation input)
        {
            string ret = "";
            for (int i = 0;i < input.Arguments.Count;i++)
            {
                ret += input.Arguments.GetParameterInfo(i).Name + " - " + input.Arguments[i] + " | ";
            }
            return ret;
        }
    }
}
