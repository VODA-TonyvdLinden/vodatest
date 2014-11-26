using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestProj.Classes.TestDataClasses
{
    public class Activation
    {
        [XmlElement]
        public string Username { get; set; }
        [XmlElement]
        public string MSISDN { get; set; }
        [XmlElement]
        public string ActivationKey { get; set; }
        [XmlElement]
        public string Alias { get; set; }
        [XmlElement]
        public string OTP { get; set; }
    }
}
