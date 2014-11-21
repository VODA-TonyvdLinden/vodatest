using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Interfaces
{
    public interface ITestUnit
    {
        void TestMethod(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
    }
}
