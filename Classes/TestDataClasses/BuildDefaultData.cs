using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Classes.TestDataClasses
{
    public class BuildDefaultData
    {
        public void Go()
        {

            Classes.TestDataClasses.Defaults def = new Classes.TestDataClasses.Defaults();
            def.ActivationData = new Classes.TestDataClasses.Activation() { ActivationKey = "123456", Alias = "NUnit Tester", MSISDN = "", OTP = "195451", Username = "5212055461087" };
            string path = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, Properties.Settings.Default.TestDataPath);

            Classes.XMLSeriallizer.Serialize<Classes.TestDataClasses.Defaults>(def, path);
        }
    }
}
