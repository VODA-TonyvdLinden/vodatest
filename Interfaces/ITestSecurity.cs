using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Interfaces
{
    public interface ITestSecurity
    {
        void TestLogin(Classes.Browser browserInstance);
        void TestFailedLogin(Classes.Browser browserInstance);
        void TestLogoff(Classes.Browser browserInstance);
    }
}
