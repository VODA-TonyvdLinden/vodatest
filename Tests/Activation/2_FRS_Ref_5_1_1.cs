using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Tests.Activation
{
    public class _2_FRS_Ref_5_1_1 : Interfaces.I_2_FRS_Ref_5_1_1
    {
        IUnityContainer container;

        public _2_FRS_Ref_5_1_1()
        {
            container = new UnityContainer();
            container.AddNewExtension<Interception>();
            container.RegisterType<Interfaces.IActivation, Tests.Activation.Activation>(
              new Interceptor<InterfaceInterceptor>(),
              new InterceptionBehavior<Classes.Timer>(),
              new InterceptionBehavior<Classes.ScreenCapture>()
              );
        }

        public void _1_Verify_User_OTP(Classes.Browser browserInstance)
        {
            Interfaces.IActivation activation = container.Resolve<Interfaces.IActivation>();
            activation.VerifyActivationOneTimePinLandingPage(browserInstance);
            activation.ActivationOneTimePinFieldValidation(browserInstance);
            activation.CorrectOneTimePinAndApplicationOffline(browserInstance);
            activation.IncorrectOneTimePin(browserInstance);
            activation.ResendOneTimePin(browserInstance);
            activation.CorrectOneTimePin(browserInstance);
        }
    }
}
