using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestProj.Classes.TestDataClasses
{
    public class Spaza
    {
        [XmlElement]
        public string Name { get; set; }
    }
}
