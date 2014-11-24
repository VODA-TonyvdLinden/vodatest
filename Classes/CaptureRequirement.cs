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
            Requirements = XMLSeriallizer.Deserialize<ScreenshotRequirements>(Properties.Settings.Default.ScreenshotRequirementsPath);
        }

        public ScreenshotRequirements Requirements { get; set; }
    }
}
