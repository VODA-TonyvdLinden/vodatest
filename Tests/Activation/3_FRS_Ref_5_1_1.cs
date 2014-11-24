using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Tests.Activation
{
    public class _3_FRS_Ref_5_1_1 : Interfaces.I_3_FRS_Ref_5_1_1
    {
        IUnityContainer container;

        public _3_FRS_Ref_5_1_1()
        {
            container = new UnityContainer();
            container.AddNewExtension<Interception>();
            container.RegisterType<Interfaces.IActivation, Tests.Activation.Activation>(
              new Interceptor<InterfaceInterceptor>(),
              new InterceptionBehavior<Classes.Timer>(),
              new InterceptionBehavior<Classes.ScreenCapture>()
              );
        }

        public void _1_Setup_Catalogue(Classes.Browser browserInstance)
        {
            Interfaces.IActivation activation = container.Resolve<Interfaces.IActivation>();
            activation.SetupCatalogueLandingPage(browserInstance);
            activation.SetupCatalogueValidations(browserInstance);
            activation.SetupCatalogueOnDeviceGEOLocationService(browserInstance);
            activation.SetupCatalogueSearchFieldReturningNoResults(browserInstance);
            activation.SetupCatalogueSearchFieldAutoComplete(browserInstance);
            activation.SetupCatalogueLandingPageInterruptions(browserInstance);
            activation.SetupCatalogueSearchFieldReturningOneOrMultipleResults(browserInstance);
        }
    }
}
