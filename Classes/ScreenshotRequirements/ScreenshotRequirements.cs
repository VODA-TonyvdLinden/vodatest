using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestProj.Classes.ScreenshotRequirements
{
    [XmlRoot("ScreenshotRequirements")]
    public class ScreenshotRequirements
    {
        public ScreenshotRequirements()
        {
            RequirementList = new List<ScreenshotRequirement>();
        }
        [XmlArray]
        public List<ScreenshotRequirement> RequirementList { get; set; }
    }
}
