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
        }

        public void Kill()
        {
            LogWriter.Instance.Log("Killing chromedriver.exe", LogWriter.eLogType.Info);
            var chromeDriverProcesses = Process.GetProcesses().
                                 Where(pr => pr.ProcessName == "chromedriver");

            LogWriter.Instance.Log(string.Format("Chromedriver count = {0}", chromeDriverProcesses.Count()), LogWriter.eLogType.Info);
            foreach (var process in chromeDriverProcesses)
            {
                LogWriter.Instance.Log(string.Format("Found {0} -> Killing", process.ProcessName), LogWriter.eLogType.Info);
                process.Kill();
            }
        }
    }
}
