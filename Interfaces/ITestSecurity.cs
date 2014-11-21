using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Interfaces
{
    public interface ITestSecurity
    {
        void TestLogin(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void TestFailedLogin(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void TestLogoff(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
    }
}
