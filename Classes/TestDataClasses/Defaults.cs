using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestProj.Classes.TestDataClasses
{
    [XmlRoot("Defaults")]
    public class Defaults
    {
        [XmlElement]
        public Activation ActivationData { get; set; }

    }
}
