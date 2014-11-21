using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Tests.Activation
{
    public class ActivationPage : Interfaces.IActivationPage
    {
        public void VerifyActivationLandingPage(Classes.Browser browserInstance)
        {
            TestProj.Classes.LogWriter.Instance.Log("VerifyActivationLandingPage started", Classes.LogWriter.eLogType.Info);

        }
        public void FieldValidation(Classes.Browser browserInstance)
        {
            TestProj.Classes.LogWriter.Instance.Log("FieldValidation started", Classes.LogWriter.eLogType.Info);

        }
        public void IncorrectUserDetails(Classes.Browser browserInstance)
        {
            TestProj.Classes.LogWriter.Instance.Log("IncorrectUserDetails started", Classes.LogWriter.eLogType.Info);

        }

        public void CorrectUserDetails(Classes.Browser browserInstance)
        {
            TestProj.Classes.LogWriter.Instance.Log("CorrectUserDetails started", Classes.LogWriter.eLogType.Info);

        }
        public void VerifyActivationOneTimePinLandingPage(Classes.Browser browserInstance)
        {
            TestProj.Classes.LogWriter.Instance.Log("VerifyActivationOneTimePinLandingPage started", Classes.LogWriter.eLogType.Info);

        }
        public void ActivationOneTimePinFieldValidation(Classes.Browser browserInstance)
        {
            TestProj.Classes.LogWriter.Instance.Log("ActivationOneTimePinFieldValidation started", Classes.LogWriter.eLogType.Info);

        }
    }
}
