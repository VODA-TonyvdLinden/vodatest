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
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName > a");
            var spazaName = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName > a");

            //LogWriter.Instance.Log(spazaName.Element.Text, LogWriter.eLogType.Error);

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
            var multi = browserInstance.Instance.FindMultiple("#landingPage > div > div.leftBlock > div > div > div > div");
            browserInstance.Instance.Assert.True(() => multi.Elements.Count > 0);
        }
        public void VerifyMarbilExists(Classes.Browser browserInstance)
        {
            // 7. Verify that the marbil add is displayed
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.headerAdd.normalHeaderWidth > img");
        }

        public void VerifySubApplicationsExists(Classes.Browser browserInstance)
        {
            // 8. Verify that the sub application are displayed and also greyed out
            var multi = browserInstance.Instance.FindMultiple("#landingPage > div > div.rightBlock > div > div > div > div");
            browserInstance.Instance.Assert.True(() => multi.Elements.Count > 0);
            browserInstance.Instance.Assert.True(() => multi.Elements.Count == 6);
            LogWriter.Instance.Log("TETCASE: VerifySubApplicationsExists -> I can see 6 app DIVs, but cannot check if they are enabled or not. PLEASE ADD ENABLE/DISABLE CLASS", LogWriter.eLogType.Error);

            //HOW TO ITTERATE INNER OBJECTS
            //multi.Elements.ForEach((elementTuple) =>
            //    {
            //        var commandProvider = elementTuple.Item1;
            //        var elementFunc = elementTuple.Item2;

            //        FluentAutomation.ElementProxy proxy = new FluentAutomation.ElementProxy(commandProvider, elementFunc);

            //        browserInstance.Instance.Assert.Class("app").On(proxy);

            //        LogWriter.Instance.Log(proxy.Elements.Count.ToString(), LogWriter.eLogType.Fatal);

            //         foreach (var element in proxy.Elements)
            //         {
            //             var commandProvider2 = element.Item1;
            //             var elementFunc2 = element.Item2;
            //             var newEl = elementFunc2();
            //             LogWriter.Instance.Log(newEl.TagName, LogWriter.eLogType.Fatal);
            //             LogWriter.Instance.Log(newEl.Attributes.Get("class"), LogWriter.eLogType.Fatal);

            //             FluentAutomation.ElementProxy proxy2 = new FluentAutomation.ElementProxy(commandProvider2, elementFunc2);
            //             LogWriter.Instance.Log(proxy.Elements.Count.ToString(), LogWriter.eLogType.Fatal);
            //             foreach (var element2 in proxy.Elements)
            //             {
            //                 var commandProvider3 = element2.Item1;
            //                 var elementFunc3 = element2.Item2;
            //                 var newEl2 = elementFunc3();
            //                 LogWriter.Instance.Log(newEl2.TagName, LogWriter.eLogType.Fatal);
            //                 LogWriter.Instance.Log(newEl2.Attributes.Get("class"), LogWriter.eLogType.Fatal);
            //             }
            //         }
            //    });

        }

        public void VerifyBottomNavExists(Classes.Browser browserInstance)
        {
            // 9. Verify that the catalogue , basket, orders and favourites blocks are displayed
            browserInstance.Instance.Assert.Exists("body > div.ui-footer.ng-scope > ul > li:nth-child(1) > div");
            browserInstance.Instance.Assert.Exists("body > div.ui-footer.ng-scope > ul > li:nth-child(2) > div");
            browserInstance.Instance.Assert.Exists("body > div.ui-footer.ng-scope > ul > li:nth-child(3) > div");
            browserInstance.Instance.Assert.Exists("body > div.ui-footer.ng-scope > ul > li:nth-child(4) > div");
        }
        public void VerifyAlertNotificationExists(Classes.Browser browserInstance)
        {
            // 10. Verify that the Alert Notification and label are displayed
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.alertStatus");
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.alertStatus > a > div");
        }

        public void VerifyBasketTotalFieldExists(Classes.Browser browserInstance)
        {
            // 11. Verify that the basket total value field is displayed
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.itemCount.ng-binding");
        }
        public void VerifyBasketLabelExists(Classes.Browser browserInstance)
        {
            // 12. Verify that the basket label is displayed
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.basketLinkContainer");
        }
        public void VerifyBasketTotalAmountExists(Classes.Browser browserInstance)
        {
            // 13. Verify that the basket total amount of items field is displayed
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.basketValue.ng-binding");
        }
        public void VerifySearchFieldExists(Classes.Browser browserInstance)
        {
            // 14. Verify that the search field is displayed on the top right hand corner of the screen
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.searchInputContainer");
        }
        public void VerifySearchFieldTextExists(Classes.Browser browserInstance)
        {
            // 15. Verify the text in the search field, it states that i am looking for
            var searchBox = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.searchInputContainer > div > input");
            browserInstance.Instance.Assert.Attribute("placeholder", "I'm looking for...").On(searchBox);
        }
        public void VerifySearchFieldTextEditableExists(Classes.Browser browserInstance)
        {
            // 16. Verify that the search text field is editable
            var searchBox = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.searchInputContainer > div > input");
            browserInstance.Instance.Enter("test").In(searchBox);
            browserInstance.Instance.Assert.Text("test").In(searchBox);

        }
    }
}
