using FluentAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Classes.Browsers
{
    //http://stackoverflow.com/questions/tagged/fluentautomation
    public class Chrome : FluentTest
    {
        public Chrome()
        {
            FluentConfig.Current.WaitOnAllActions(true);
            FluentConfig.Current.ScreenshotOnFailedAction(true);
            FluentConfig.Current.WindowSize(1920, 1080);
            FluentConfig.Current.WindowMaximized(true);
        }
        public FluentAutomation.Interfaces.IActionSyntaxProvider Create()
        {
            SeleniumWebDriver.Bootstrap(
               SeleniumWebDriver.Browser.Chrome
               );
            //FluentConfig.Current.WindowMaximized(true);

            return I;
        }
    }
}
