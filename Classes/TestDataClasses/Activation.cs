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
            SingleSpazaUser = new User();
            MultiSpazaUser = new User();
        }

        public User SingleSpazaUser { get; set; }
        public User MultiSpazaUser { get; set; }

    }
}
