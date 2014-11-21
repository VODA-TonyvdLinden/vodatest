using FluentAutomation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Classes
{
    public class Browser : FluentTest
    {
        public enum eBrowser
        {
            IE = 1,
            Firefox = 3,
            Chrome = 4,
        }

        private FluentAutomation.Interfaces.IActionSyntaxProvider browser;
        public FluentAutomation.Interfaces.IActionSyntaxProvider Instance
        {
            get
            {
                return browser;
            }
        }

        public Browser(eBrowser browserType)
        {
            switch (browserType)
            {
                case eBrowser.Chrome:
                    browser = new Browsers.Chrome().Create();
                    break;
                case eBrowser.Firefox:
                    browser = new Browsers.Firefox().Create();
                    break;
                case eBrowser.IE:
                    browser = new Browsers.IE().Create();
                    break;
            }

        }
        public void Navigate(Uri path)
        {
            browser.Open(path.AbsoluteUri);
        }
    }
}
