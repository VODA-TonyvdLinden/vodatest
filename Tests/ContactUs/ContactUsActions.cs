using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProj.Classes;
using TestProj.Tests.Common;

namespace TestProj.Tests.ContactUs
{
    public class ContactUsActions : Interfaces.IContactUs
    {
        public void VerifyContactUsLablelAndIcon(Classes.Browser browserInstance, string icon, string label)
        {
            Helpers.Instance.Exists(browserInstance, icon);
            Helpers.Instance.Exists(browserInstance, label);
        }
        public void VerifyContactUsSubMenus(Classes.Browser browserInstance, string menu1, string menu2, string menu3, string menu4)
        {
            Helpers.Instance.Exists(browserInstance, menu1);
            Helpers.Instance.Exists(browserInstance, menu2);
            Helpers.Instance.Exists(browserInstance, menu3);
            Helpers.Instance.Exists(browserInstance, menu4);
        }
        public void VerifyFAQSection(Classes.Browser browserInstance)
        {
            // 2.6 Verify that the frequently asked icon and label are displayed on the screen    
            VerifyContactUsLablelAndIcon(browserInstance,
                "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(3) > div > span:nth-child(1) > img",
                "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(3) > div > span.txt");

            LogWriter.Instance.Log("TESTCASE:_01_ContactUsTest -> Test case incomplete - Submenu check for frequently asked not specified. Update test case", LogWriter.eLogType.Error);
            VerifyContactUsSubMenus(browserInstance,
             "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(3) > ul > li:nth-child(1)",
             "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(3) > ul > li:nth-child(2)",
             "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(3) > ul > li:nth-child(3)",
             "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(3) > ul > li:nth-child(4)");
        }
        public void VerifySelfServiceSection(Classes.Browser browserInstance)
        {
            // 2.4 Verify that the self service label and icon are displayed on on the screen   
            VerifyContactUsLablelAndIcon(browserInstance, "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(2) > div > span:nth-child(1) > img", "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(2) > div > span.txt");

            // 2.5 Verify that the sub menus labels are displayed on the screen     
            VerifyContactUsSubMenus(browserInstance,
             "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(2) > ul > li:nth-child(1)",
             "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(2) > ul > li:nth-child(2)",
             "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(2) > ul > li:nth-child(3)",
             "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(2) > ul > li:nth-child(4)");
        }
        public void VerifyPerfectStartupSection(Classes.Browser browserInstance)
        {
            // 2.2 Verify that the perfect start-up label and icon are available  
            // 2.2  The perfect start-up label and icon are displayed 
            VerifyContactUsLablelAndIcon(browserInstance, "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(1) > div > span:nth-child(1) > img", "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(1) > div > span.txt");

            // 2.3 Verify that  the sub menus labels are displayed under perfect start-up menu
            // 2.3 The sub menus are displayed under perfect start-up menu
            VerifyContactUsSubMenus(browserInstance,
                "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(1) > ul > li:nth-child(1)",
                "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(1) > ul > li:nth-child(2)",
                "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(1) > ul > li:nth-child(3)",
                "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.row > div:nth-child(1) > ul > li:nth-child(4)");
        }
        public void VerifyCustomerServiceContactNo(Classes.Browser browserInstance)
        {
            // 2.1 Verify that customer service contact number   "
            LogWriter.Instance.Log("TESTCASE:_01_ContactUsTest -> Test step unclear. '2.1 Verify that customer service contact number' - Update test case", LogWriter.eLogType.Error);
            Helpers.Instance.Exists(browserInstance, "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.contactTitle.ng-binding");
            // 2.1 The customer service number is displayed   
            var csNumber = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(2) > div > div.container-fluid > div > div.col-sm-offset-2.col-sm-8 > div.contactTitle.ng-binding");
            browserInstance.Instance.Assert.True(() => csNumber.Element.Text == "Customer Service: 082 123 456");
        }
        public void GoToContactUs(Classes.Browser browserInstance)
        {
            // 1. Click on the contact us hyperlink     
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.contactUsContainer"));
            // 1. The Contact us page is displayed    
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/contact-us");
            // . Verify the following on the contact us page    
        }
    }
}
