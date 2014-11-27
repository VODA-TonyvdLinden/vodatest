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
        public Activation()
        {
            Spazas = new List<Spaza>();
        }
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
        [XmlElement]
        public string InvalidOTP { get; set; }
        [XmlElement]
        public string ExpiredOPT { get; set; }
        [XmlArray]
        public List<Spaza> Spazas { get; set; }

    }
}
