using FluentAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Classes.Browsers
{
    public class Chrome : FluentTest
    {
        public FluentAutomation.Interfaces.IActionSyntaxProvider Create()
        {
            SeleniumWebDriver.Bootstrap(
               SeleniumWebDriver.Browser.Chrome
               );
            return I;
        }
    }
}
