using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Classes
{
    public sealed class LogWriter
    {
        ILog log;

        static LogWriter _instance;
        public static LogWriter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LogWriter();
                }
                return _instance;
            }
        }
        LogWriter()
        {
            log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            log4net.Config.XmlConfigurator.Configure();
        }


        public enum eLogType { Debug, Info, Error, Fatal };
        public void Log(string message, eLogType logType)
        {
            switch (logType)
            {
                case eLogType.Debug:
                    log.Debug(message);
                    break;
                case eLogType.Info:
                    log.Info(message);
                    break;
                case eLogType.Error:
                    log.Error(message);
                    break;
                case eLogType.Fatal:
                    log.Fatal(message);
                    break;
                default:
                    break;
            }
        }
    }
}
