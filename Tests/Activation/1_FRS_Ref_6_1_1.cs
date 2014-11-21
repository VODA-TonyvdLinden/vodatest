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

        IUnityContainer container;

        public _1_FRS_Ref_6_1_1()
        {
            container = new UnityContainer();
            container.AddNewExtension<Interception>();
            container.RegisterType<Interfaces.IActivation, Tests.Activation.Activation>(
              new Interceptor<InterfaceInterceptor>(),
              new InterceptionBehavior<Classes.Timer>(),
              new InterceptionBehavior<Classes.ScreenCapture>()
              );
        }

        public void _1_ActivationPage(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement)
        {
            //browserInstance.Navigate(new Uri(Properties.Settings.Default.LogonURL));

            Interfaces.IActivation activation = container.Resolve<Interfaces.IActivation>();
            activation.VerifyActivationLandingPage(browserInstance, new Classes.ScreenshotRequirement() { EntryRequired = false, ExitRequired = false });
        }

        public void _2_ActivationForm(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement)
        {
            Interfaces.IActivation activation = container.Resolve<Interfaces.IActivation>();

            activation.ActivationFormFieldValidation(browserInstance, new Classes.ScreenshotRequirement() { EntryRequired = false, ExitRequired = false });
            activation.ActivationFormIncorrectUserDetails(browserInstance, new Classes.ScreenshotRequirement() { EntryRequired = false, ExitRequired = false });
            activation.ActivationFormCorrectUserDetails(browserInstance, new Classes.ScreenshotRequirement() { EntryRequired = false, ExitRequired = false });

        }
    }
}
