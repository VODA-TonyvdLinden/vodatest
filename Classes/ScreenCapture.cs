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
        ScreenshotRequirements.ScreenshotRequirement screenshotRequirement = new Classes.ScreenshotRequirements.ScreenshotRequirement() { EntryRequired = false, ExitRequired = false };

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            string sender = string.Format("{0}_{1}", input.MethodBase.ReflectedType.Name, input.MethodBase.Name);
            //LogWriter.Instance.Log(string.Format("Reflector {0}", sender), LogWriter.eLogType.Info);

            Classes.Browser automator = getAutomator(input);
            Classes.ScreenshotRequirements.ScreenshotRequirement req = getRequirement(input.MethodBase.Name);
            IMethodReturn msg;
            takeStartScreenshot(sender, req, automator);
            msg = getNext()(input, getNext);
            takeEndScreenshot(sender, req, automator);
            return msg;
        }

        private ScreenshotRequirements.ScreenshotRequirement getRequirement(string methodName)
        {
            //LogWriter.Instance.Log(string.Format("looking for {0}", methodName), LogWriter.eLogType.Info);

            Classes.ScreenshotRequirements.ScreenshotRequirement req = null;
            if (CaptureRequirement.Instance.Requirements.RequirementList.Any(r => r.EventName == methodName))
                req = CaptureRequirement.Instance.Requirements.RequirementList.Single<Classes.ScreenshotRequirements.ScreenshotRequirement>(s => s.EventName == methodName);

            if (req == null)
                if (Properties.Settings.Default.AutoCrreateScreenshotRequirements)
                {
                    Classes.ScreenshotRequirements.BuildScreenshotRequirements reqs = new ScreenshotRequirements.BuildScreenshotRequirements();
                    reqs.AddMissing(methodName);
                }
                else
                    LogWriter.Instance.Log(string.Format("Screenshot requirement for method {0} not found", methodName), LogWriter.eLogType.Error);
            return req;
        }

        private void takeStartScreenshot(string sender, Classes.ScreenshotRequirements.ScreenshotRequirement req, Classes.Browser automator)
        {
            if (req != null && Properties.Settings.Default.TakeMethodStartScreenShots && req.EntryRequired)
                automator.Instance.TakeScreenshot(string.Format("{0}_Enter.png", sender));
        }
        private void takeEndScreenshot(string sender, Classes.ScreenshotRequirements.ScreenshotRequirement req, Classes.Browser automator)
        {
            if (req != null && Properties.Settings.Default.TakeMethodStartScreenShots && req.ExitRequired)
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
                if (input.Arguments[i].GetType() == typeof(Classes.ScreenshotRequirements.ScreenshotRequirement))
                    screenshotRequirement = (Classes.ScreenshotRequirements.ScreenshotRequirement)input.Arguments[i];
                else
                    if (input.Arguments[i].GetType() == typeof(Classes.Browser))
                        ret = (Classes.Browser)input.Arguments[i];
            }
            return ret;
        }
    }
}
