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
        public FluentAutomation.Interfaces.IActionSyntaxProvider Create()
        {
            SeleniumWebDriver.Bootstrap(
               SeleniumWebDriver.Browser.Chrome
               );
            FluentConfig.Current.WindowMaximized(true);
            return I;
        }
    }
}
