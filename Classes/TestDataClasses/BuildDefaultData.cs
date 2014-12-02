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
            Classes.TestDataClasses.Spaza spaza = new Spaza() { Name = "Mohammad store" };

            Classes.TestDataClasses.User singleSpazaUser = new User() { ActivationKey = "137456", Alias = "Single Spaza Tester", MSISDN = "0829985519", OTP = "151846", Username = "3012300230085", InvalidOTP = "INV123", ExpiredOPT = "EXP123", ChallengeQuestion = 3, ChallengeAnswer = "Pretoria" };
            singleSpazaUser.Spazas.Add(spaza);

            Classes.TestDataClasses.User multiSpazaUser = new User() { ActivationKey = "123456", Alias = "Multi Spaza Tester", MSISDN = "0829985519", OTP = "151846", Username = "5212055461087", InvalidOTP = "INV123", ExpiredOPT = "EXP123", ChallengeQuestion = 3, ChallengeAnswer = "Pretoria" };
            Classes.TestDataClasses.Spaza multiSpaza1 = new Spaza() { Name = "10 City Tuck Shop" };
            Classes.TestDataClasses.Spaza multiSpaza2 = new Spaza() { Name = "16 Tuck Shop" };

            multiSpazaUser.Spazas.Add(multiSpaza1);
            multiSpazaUser.Spazas.Add(multiSpaza2);
            

            def.ActivationData = new Classes.TestDataClasses.Activation() { SingleSpazaUser = singleSpazaUser, MultiSpazaUser = multiSpazaUser };

            //def.ActivationData = new Classes.TestDataClasses.Activation() { ActivationKey = "123456", Alias = "NUnit Tester", MSISDN = "0829985519", OTP = "151846", Username = "5212055461087", InvalidOTP = "INV123", ExpiredOPT = "EXP123", ChallengeQuestion = 3, ChallengeAnswer = "Pretoria" };
            //def.ActivationData.Spazas.Add(spaza);
            string path = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, Properties.Settings.Default.TestDataPath);

            Classes.XMLSeriallizer.Serialize<Classes.TestDataClasses.Defaults>(def, path);
        }
    }
}
