using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProj.Classes;
using TestProj.Tests.Common;

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
            browserInstance.Instance.Assert.True(() => alias.Element.Text == TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Alias);
        }
        public void VerifySpazaName(Classes.Browser browserInstance)
        {
            // 5. Verify that the preferred alias name is displayed on top right hand corner of the app with 
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName > a");
            var spazaName = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName > a");

            //LogWriter.Instance.Log(spazaName.Element.Text, LogWriter.eLogType.Error);

            Classes.TestDataClasses.Spaza spaza = TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Spazas.Find(s => s.Name == spazaName.Element.Text);
            if (spaza == null)
            {
                LogWriter.Instance.Log(string.Format("TESTCASE: VerifySpazaName -> {0} is not configured as a spaza shop for {1}", spazaName.Element.Text, TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Username), LogWriter.eLogType.Error);
                if (TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Spazas.Count < 1)
                    LogWriter.Instance.Log(string.Format("TESTCASE: VerifySpazaName -> There are no spaza shops configured for {0}", TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Username), LogWriter.eLogType.Error);
                spaza = TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Spazas[0];
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
        public void VerifyAlertSearchBox(Classes.Browser browserInstance)
        {
            var alertLink = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.alertStatus > a > div");
            Helpers.Instance.ClickButton(browserInstance, alertLink);
            var searchBox = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.searchInputContainer > div > input");
            //   2.1.1 Please enter alphanumeric  < 07@ >
            //   2.1.2 Please enter space before entering input on the field
            //   2.1.3 Please enter special characters  <@@, &&> 
            //   2.1.4 Please enter decimal numbers <0.00444> 
            //   2.1.5 Please enter negative value <-1>
            Helpers.Instance.TestFieldInputValidation(browserInstance, searchBox);
            LogWriter.Instance.Log("TESTCASE:_02_ApplicationFieldValidation -> What is supposed to happen when invalid chars are inserted in the box? is it supposed to disallow you totally, or are you supposed to get an error of some type?", LogWriter.eLogType.Error);
        }
        public void VerifyBaskSearchBox(Classes.Browser browserInstance)
        {
            var basketLink = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.basketLinkContainer > div");
            Helpers.Instance.ClickButton(browserInstance, basketLink);

            // 3. Verify that the basket search field validations
            //   3.1.1 Please enter alphanumeric  < 07@ >
            //   3.1.2 Please enter space before entering input on the field
            //   3.1.3 Please enter special characters  <@@, &&> 
            //   3.1.4 Please enter decimal numbers <0.00444> 
            //   3.1.5 Please enter negative value <-1>
            var searchBox = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.searchInputContainer > div > input");
            LogWriter.Instance.Log("TESTCASE:_02_ApplicationFieldValidation -> What is supposed to happen when invalid chars are inserted in the box? is it supposed to disallow you totally, or are you supposed to get an error of some type?", LogWriter.eLogType.Error);
            Helpers.Instance.TestFieldInputValidation(browserInstance, searchBox);
        }

        public void VerifySubAppAccessibility(Classes.Browser browserInstance)
        {
            // 3. Verify that the sub applications are in active for this version
            var multiApplications = browserInstance.Instance.FindMultiple("#landingPage > div > div.rightBlock > div > div > div > div");
            int firstInstance = 1;
            LogWriter.Instance.Log("TESTCASE:_03_ApplicationLandingContentsFunctionality -> Test case wrong. The first application link is a link to specials, and is clickable", LogWriter.eLogType.Error);
            multiApplications.Elements.ForEach((elementTuple) =>
            {
                //The first application is a link and not an app
                if (firstInstance > 1)
                {
                    var commandProvider = elementTuple.Item1;
                    var elementFunc = elementTuple.Item2;

                    FluentAutomation.ElementProxy proxy = new FluentAutomation.ElementProxy(commandProvider, elementFunc);
                    //   3.1 Click on the sub application place holder to see if they are accessable
                    Helpers.Instance.ClickButton(browserInstance, proxy);
                    //   3.1 The sub applications place holders are not accessable
                    browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/main");
                }

                firstInstance++;
            });
        }

        public void VerifyMarbilAccessibility(Classes.Browser browserInstance)
        {
            // NB. Please note that the marbil add is only accesible when application is online 
            if (Helpers.Instance.ApplicationIsOnline(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.networkStatus > div"))
            {
                // 2. Verify the Marbil add if is clickable
                //   2.1 Click on the Marbil ad that is displayed on the screen.
                var marbil = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.headerAdd.normalHeaderWidth");
                Helpers.Instance.ClickButton(browserInstance, marbil);
                //   2.1 The item selected from the marbil add is displayed
                browserInstance.Instance.Assert.Url("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.headerAdd.normalHeaderWidth > img");
                LogWriter.Instance.Log("TESTCASE:_03_ApplicationLandingContentsFunctionality -> Cannot test MARBIL. Nothing happens when you click on it.", LogWriter.eLogType.Error);
                LogWriter.Instance.Log("TESTCASE:_03_ApplicationLandingContentsFunctionality -> is the MARBIL page url always the same?", LogWriter.eLogType.Error);
            }
            else
            {
                LogWriter.Instance.Log("TESTCASE:_03_ApplicationLandingContentsFunctionality -> Application is offline. Cannot test MARBIL", LogWriter.eLogType.Info);
            }
        }

        public void VerifySpecialAccessibility(Classes.Browser browserInstance)
        {
            //   1.1 Select any specail within the catalogue to see if is selectable by a single click
            var specialItem = Helpers.Instance.GetProxy(browserInstance, "#landingPage > div > div.leftBlock > div > div > div > div:nth-child(1) > div > div");
            Helpers.Instance.ClickButton(browserInstance, specialItem);
            //   1.1 The selected item is marked
            LogWriter.Instance.Log("TESTCASE:_03_ApplicationLandingContentsFunctionality -> Item is not market when single click. Cannot test for this. Test case wrong. Same behaviour as double click", LogWriter.eLogType.Error);
            //   1.2 double click on the item to see if clickable
            Helpers.Instance.DoubleClickButton(browserInstance, specialItem);
            //   1.2 The item is clickable and is displayed on the user interface two show that the user has selected it.
            Helpers.Instance.Exists(browserInstance, "#product_modal > div");
        }

        public void SelectSpaza(Classes.Browser browserInstance, string spazaName)
        {
            // 3.Select any spaza on the list
            var spazaLink = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName > a");
            //var spazaLink = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName");
            //var spazaLink = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName > a");
            Helpers.Instance.ClickButton(browserInstance, spazaLink);
            //ISSUE HERE



            var spazaOne = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName.open > ul > li:nth-child(2) > a");
            var spazaTwo = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName.open > ul > li:nth-child(3) > a");

            if (spazaOne.Element.Text == spazaName)
                Helpers.Instance.ClickButton(browserInstance, spazaOne);
            else
                if (spazaTwo.Element.Text == spazaName)
                    Helpers.Instance.ClickButton(browserInstance, spazaTwo);
        }
        public void AddSpecialToBasket(Classes.Browser browserInstance)
        {
            //Add an item to the basket
            Helpers.Instance.AddSpecialToBasket(browserInstance);
        }

        public void VerifySpazaNameForReturnToApp(Classes.Browser browserInstance)
        {
            FluentAutomation.ElementProxy spazaName = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName > a");
            string currentSpaza = spazaName.Element.Text;

            browserInstance.Navigate(new Uri("https://www.wikipedia.org/"));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("https://www.wikipedia.org/"), TimeSpan.FromMinutes(30));
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/main"), TimeSpan.FromMinutes(30));
            spazaName = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName > a");
            string returnSpazaName = spazaName.Element.Text;
            browserInstance.Instance.Assert.True(() => currentSpaza == returnSpazaName);
        }

        public void SwitchSpazaAndCheckBasket(Classes.Browser browserInstance, Interfaces.IAccessingApplicationActions accessingApplicationAction)
        {
            // 4. Verify the following changes basket switches to the outlet specific basket,orders needs to 
            //   switch to the outlet specific orders (including invoices),catalogues, favourites and Messages

            // 4. The basket  switches to the outlet specific basket,orders needs to switch to the outlet specific 
            //   orders (including invoices),catalogues, favourites and Messages remain the same
            //Select first spaza
            accessingApplicationAction.SelectSpaza(browserInstance, "10 City Tuck Shop");
            FluentAutomation.ElementProxy spazaName = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName > a");
            browserInstance.Instance.Assert.Text("10 City Tuck Shop").In(spazaName);
            string currentSpazaName = spazaName.Element.Text;
            accessingApplicationAction.AddSpecialToBasket(browserInstance);
            //Check number of items in basket
            var basketCount = Helpers.Instance.GetProxy(browserInstance, " body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.itemCount.ng-binding");
            string count = basketCount.Element.Text.Replace(" Items", "").Replace(" ", "");

            //Select the next spaza
            accessingApplicationAction.SelectSpaza(browserInstance, "16 Tuck Shop");
            // 3. The selected spaza is displayed and  is only valid for the session
            accessingApplicationAction.VerifySpazaName(browserInstance);
            browserInstance.Instance.Assert.Text("16 Tuck Shop").In(spazaName);
            string secondSpazaName = spazaName.Element.Text;
            browserInstance.Instance.Assert.False(() => currentSpazaName == secondSpazaName);

            var secondBasketCount = Helpers.Instance.GetProxy(browserInstance, " body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.itemCount.ng-binding");
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.True(() => basketCount.Element.Text == "0 Items"));
            string secondCount = basketCount.Element.Text.Replace(" Items", "").Replace(" ", "");
            browserInstance.Instance.Assert.False(() => count == secondCount);
        }
    }
}
