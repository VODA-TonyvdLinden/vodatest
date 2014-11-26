using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestProj.Classes.ScreenshotRequirements
{
    [XmlRoot("ScreenshotRequirement")]
    public class ScreenshotRequirement
    {
        [XmlElement]
        public string EventName { get; set; }
        [XmlElement]
        public bool EntryRequired { get; set; }
        [XmlElement]
        public bool ExitRequired { get; set; }
    }
}
