using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Classes
{
    public sealed class ProcessKiller
    {

        static ProcessKiller _instance;
        public static ProcessKiller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProcessKiller();
                }
                return _instance;
            }
        }
        ProcessKiller()
        {
            //container.RegisterType<Interfaces.IActivationActions, Tests.Activation.ActivationActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
        }

        public void Kill()
        {
            foreach (Process proc in Process.GetProcessesByName("chromedriver.exe"))
            {
                proc.Kill();
            }
        }
    }
}
