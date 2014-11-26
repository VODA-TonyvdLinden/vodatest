using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Classes
{
    public sealed class CaptureRequirement
    {
        static CaptureRequirement _instance;
        public static CaptureRequirement Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CaptureRequirement();
                }
                return _instance;
            }
        }
        CaptureRequirement()
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Properties.Settings.Default.ScreenshotRequirementsPath);
            //string path = string.Format("{0}\\{1}",AppDomain.CurrentDomain.BaseDirectory,Properties.Settings.Default.ScreenshotRequirementsPath);
            Requirements = XMLSeriallizer.Deserialize<ScreenshotRequirements.ScreenshotRequirements>(path);
        }

        public ScreenshotRequirements.ScreenshotRequirements Requirements { get; set; }
    }
}
