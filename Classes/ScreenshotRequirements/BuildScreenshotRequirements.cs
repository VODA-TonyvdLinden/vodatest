using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Classes.ScreenshotRequirements
{
    public class BuildScreenshotRequirements
    {
        public void AddMissing(string requirementName)
        {
            ScreenshotRequirements reqs = CaptureRequirement.Instance.Requirements;
            reqs.RequirementList.Add(new ScreenshotRequirement() { EventName = requirementName, EntryRequired = Properties.Settings.Default.TakeMethodStartScreenShots, ExitRequired = Properties.Settings.Default.TakeMethodEndScreenshot });

            string path = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, Properties.Settings.Default.ScreenshotRequirementsPath);

            Classes.XMLSeriallizer.Serialize<Classes.ScreenshotRequirements.ScreenshotRequirements>(reqs, path);
        }
    }
}
