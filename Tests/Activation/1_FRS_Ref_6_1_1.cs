using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Tests.Activation
{
    public class _1_FRS_Ref_6_1_1 : Interfaces.I_1_FRS_Ref_6_1_1 
    {
        public void TestActivationPage(Classes.Browser browserInstance)
        {
            IUnityContainer container = new UnityContainer();
            container.AddNewExtension<Interception>();
            container.RegisterType<Interfaces.IActivationPage, Tests.Activation.ActivationPage>(
              new Interceptor<InterfaceInterceptor>(),
              new InterceptionBehavior<Classes.Timer>());

            //browserInstance.Navigate(new Uri(Properties.Settings.Default.LogonURL));

            Interfaces.IActivationPage activationPage = container.Resolve<Interfaces.IActivationPage>();

            activationPage.VerifyActivationLandingPage(browserInstance);
            activationPage.FieldValidation(browserInstance);
            activationPage.IncorrectUserDetails(browserInstance);
            activationPage.CorrectUserDetails(browserInstance);
            activationPage.VerifyActivationOneTimePinLandingPage(browserInstance);
            activationPage.ActivationOneTimePinFieldValidation(browserInstance);
        }


        public void TestOther(Classes.Browser browserInstance)
        {


        }
    }
}
