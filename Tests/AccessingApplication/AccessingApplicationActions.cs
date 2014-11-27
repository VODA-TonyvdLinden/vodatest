using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProj.Classes;

namespace TestProj.Tests.AccessingApplication
{
    public class AccessingApplicationActions : Interfaces.IAccessingApplicationActions
    {
        public void VerifyLogoAndBanner(Classes.Browser browserInstance)
        {
            // 1. Verify that the vodacom logo and the red banner are displayed on the activation screen
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.headerLogo.left > a > img");
            var logo = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.headerLogo.left > a > img");
            browserInstance.Instance.Assert.Attribute("src", "http://aspnet.dev.afrigis.co.za/bopapp/images/logo-rotated.e90367bc.png").On(logo);

            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed");
            var redBanner = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed");
            browserInstance.Instance.Assert.Class("vodaBackgroundRed").On(redBanner);
        }
        public void VerifyOnlineIndicator(Classes.Browser browserInstance)
        {
            // 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.networkStatus > div");
            var onlineOfflineIndicator = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.networkStatus > div");
            browserInstance.Instance.Assert.Class("statusDisplay").On(onlineOfflineIndicator);
        }
        public void VerifyPageLinks(Classes.Browser browserInstance)
        {
            // 3. Verify that contact us and help me hyperlinks are displayed
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.contactUsContainer");
            var contactUs = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.contactUsContainer");
            browserInstance.Instance.Assert.True(() => contactUs.Element.Text == "Contact us");
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.helpMeContainer");
            var helpMe = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.helpMeContainer");
            browserInstance.Instance.Assert.True(() => helpMe.Element.Text == "Help me");
        }
        public void VerifyPreferedAlias(Classes.Browser browserInstance)
        {
            // 5. Verify that the preferred alias name is displayed on top right hand corner of the app with 
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.aliasName.ng-binding");
            var alias = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.aliasName.ng-binding");
            browserInstance.Instance.Assert.True(() => alias.Element.Text == TestData.Instance.DefaultData.ActivationData.Alias);
        }
        public void VerifySpazaName(Classes.Browser browserInstance)
        {
            // 5. Verify that the preferred alias name is displayed on top right hand corner of the app with 
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName.open > a");
            var spazaName = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName.open > a");


            Classes.TestDataClasses.Spaza spaza = TestData.Instance.DefaultData.ActivationData.Spazas.Find(s => s.Name == spazaName.Element.Text);
            if (spaza == null)
            {
                LogWriter.Instance.Log(string.Format("TESTCASE: VerifySpazaName -> {0} is not configured as a spaza shop for {1}", spazaName.Element.Text, TestData.Instance.DefaultData.ActivationData.Username), LogWriter.eLogType.Error);
                if (TestData.Instance.DefaultData.ActivationData.Spazas.Count < 1)
                    LogWriter.Instance.Log(string.Format("TESTCASE: VerifySpazaName -> There are no spaza shops configured for {0}", TestData.Instance.DefaultData.ActivationData.Username), LogWriter.eLogType.Error);
                spaza = TestData.Instance.DefaultData.ActivationData.Spazas[0];
            }

            browserInstance.Instance.Assert.True(() => spazaName.Element.Text == spaza.Name);
        }
        public void VerifySpecialsExists(Classes.Browser browserInstance)
        {
            // 6. Verify that the user is served with specials on special block

            LogWriter.Instance.Log("try", LogWriter.eLogType.Fatal);
            var specialGroup = browserInstance.Instance.Find("#landingPage > div > div.leftBlock > div > div > div");

            LogWriter.Instance.Log(specialGroup.Element.Text, LogWriter.eLogType.Fatal);

            //foreach (FluentAutomation.ElementProxy proxy in elements.Children)
            //{

            //}
            //browserInstance.Instance.Assert.True(() => elements.Children.Count > 0);

            ////Checking if special group exists
            //browserInstance.Instance.Assert.Exists("#landingPage > div > div.leftBlock > div > div > div");
            //var specialGroup = browserInstance.Instance.Find("#landingPage > div > div.leftBlock > div > div > div");
            //browserInstance.Instance.Assert.Class("specialsGroup").On(specialGroup);
            ////Checking if at least one special row exists
            //browserInstance.Instance.Assert.Exists("#landingPage > div > div.leftBlock > div > div > div > div:nth-child(1)");
            //var specialRow = browserInstance.Instance.Find("#landingPage > div > div.leftBlock > div > div > div > div:nth-child(1)");
            //browserInstance.Instance.Assert.Class("specialsRow").On(specialRow);
            ////Checking if at least one product container exists
            //browserInstance.Instance.Assert.Exists("#landingPage > div > div.leftBlock > div > div > div > div:nth-child(1) > div > div");
            //var product = browserInstance.Instance.Find("#landingPage > div > div.leftBlock > div > div > div > div:nth-child(1) > div > div");
            //browserInstance.Instance.Assert.Class("productContainer").On(product);
            //LogWriter.Instance.Log("TESTCASE:VerifySpecialsExists -> Can only check if at least one special is loaded on the page", LogWriter.eLogType.Info);
        }
        public void VerifyMarbilExists(Classes.Browser browserInstance)
        {
            // 7. Verify that the marbil add is displayed
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.headerAdd.normalHeaderWidth > img");
        }

        //public void VerifySubApplicationsExists(Classes.Browser browserInstance)
        //{
        //    // 6. Verify that the user is served with specials on special block
        //    //Checking if special group exists

        //    var elements = browserInstance.Instance.FindMultiple("#landingPage > div > div.rightBlock > div > div > div:nth-child(1) > div:nth-child(2)");
        //    //foreach (var el in elements)
        //    //{

        //    //}

        //    //browserInstance.Instance.Assert.Exists("#landingPage > div > div.rightBlock > div > div > div:nth-child(1) > div:nth-child(2)");
        //    //var firstApp = browserInstance.Instance.Find("#landingPage > div > div.rightBlock > div > div > div:nth-child(1) > div:nth-child(2)");

        //    //FluentAutomation.ElementProxy firstApp.Children[0].

        //    //browserInstance.Instance.Assert.Class("specialsGroup").On(specialGroup);
        //    ////Checking if at least one special row exists
        //    //browserInstance.Instance.Assert.Exists("#landingPage > div > div.leftBlock > div > div > div > div:nth-child(1)");
        //    //var specialRow = browserInstance.Instance.Find("#landingPage > div > div.leftBlock > div > div > div > div:nth-child(1)");
        //    //browserInstance.Instance.Assert.Class("specialsRow").On(specialRow);
        //    ////Checking if at least one product container exists
        //    //browserInstance.Instance.Assert.Exists("#landingPage > div > div.leftBlock > div > div > div > div:nth-child(1) > div > div");
        //    //var product = browserInstance.Instance.Find("#landingPage > div > div.leftBlock > div > div > div > div:nth-child(1) > div > div");
        //    //browserInstance.Instance.Assert.Class("productContainer").On(product);
        //    //LogWriter.Instance.Log("TESTCASE:VerifySpecialsExists -> Can only check if at least one special is loaded on the page", LogWriter.eLogType.Info);
        //}
    }
}
