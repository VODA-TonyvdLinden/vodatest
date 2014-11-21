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
    public class ScreenCapture : IInterceptionBehavior
    {
        ScreenshotRequirement screenshotRequirement = new Classes.ScreenshotRequirement() { EntryRequired = false, ExitRequired = false };

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            string sender = string.Format("{0}_{1}", input.MethodBase.ReflectedType.Name, input.MethodBase.Name);

            Classes.Browser automator = getAutomator(input);
            IMethodReturn msg;
            takeStartScreenshot(sender, automator);
            msg = getNext()(input, getNext);
            takeEndScreenshot(sender, automator);
            return msg;
        }

        private void takeStartScreenshot(string sender, Classes.Browser automator)
        {
            if (Properties.Settings.Default.TakeMethodStartScreenShots && screenshotRequirement.EntryRequired)
                automator.Instance.TakeScreenshot(string.Format("{0}_Enter.png", sender));
        }
        private void takeEndScreenshot(string sender, Classes.Browser automator)
        {
            if (Properties.Settings.Default.TakeMethodEndScreenshot && screenshotRequirement.ExitRequired)
                automator.Instance.TakeScreenshot(string.Format("{0}_Exit.png", sender));
        }

        public bool WillExecute
        {
            get { return true; }
        }

        private Classes.Browser getAutomator(IMethodInvocation input)
        {
            Classes.Browser ret = null;
            for (int i = 0; i < input.Arguments.Count; i++)
            {
                if (input.Arguments[i].GetType() == typeof(Classes.ScreenshotRequirement))
                    screenshotRequirement = (Classes.ScreenshotRequirement)input.Arguments[i];
                else
                if (input.Arguments[i].GetType() == typeof(Classes.Browser))
                    ret = (Classes.Browser)input.Arguments[i];
            }
            return ret;
        }
    }
}
