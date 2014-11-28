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
            Classes.TestDataClasses.Spaza spaza = new Spaza() { Name = "NUnit Spaza" };
            def.ActivationData = new Classes.TestDataClasses.Activation() { ActivationKey = "123456", Alias = "NUnit Tester", MSISDN = "0726629728", OTP = "195451", Username = "5212055461087", InvalidOTP = "INV123", ExpiredOPT = "EXP123", ChallengeQuestion = 4,ChallengeAnswer = "BOP" };
            def.ActivationData.Spazas.Add(spaza);
            string path = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, Properties.Settings.Default.TestDataPath);

            Classes.XMLSeriallizer.Serialize<Classes.TestDataClasses.Defaults>(def, path);
        }
    }
}
