using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestProj.Classes;
using TestProj.Tests.Common;

namespace TestProj.Tests.Alerts
{
    public class AlertsActions : Interfaces.IAlertsActions
    {
        // Test Case: 1. Verify that the Vodacom logo and the red banner are displayed on the activation screen
        // Test Output: 1. The Vodacom banner logo and banner are displayed
        public void VerifyVodacomLogoAndBanner(Classes.Browser browserInstance)
        {
            Helpers.Instance.CheckLogoAndBanner(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.headerLogo.left > a > img", "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed");
        }

        // Test Case: 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
        // Test Output: 2. The online/offline indicator is displayed on the top left hand corner of the screen
        public void VerifyOnlineOffilineIndicator(Classes.Browser browserInstance)
        {
            LogWriter.Instance.Log("TESTCASE:ISSUE 120: TEST CASE: _01_AlertsLandingPageVerfication -> Test step cannot be implemented. '2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen' - Cannot test this since we do not have the online class/indicator anymore.", LogWriter.eLogType.Error);
        }

        // Test Case: 3. Verify that contact us and help me hyperlinks are displayed
        // Test Output: 3. The contact us and help me hyperlinks are displayed
        public void VerifyContactUsHelpMeLinks(Classes.Browser browserInstance)
        {
            Helpers.Instance.CheckPageLinks(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.contactUsContainer", "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.helpMeContainer");
        }

        // Test Case: 5. Verify that the preferred alias name is displayed on top right hand corner of the app with the spaza owner's alias name and spaza name  
        // Test Output: 5. The preferred alias name is displayed on the top right hand corner of the app, with the name spaza owner's alias name and the spaza name    
        public void VerifySpazaAliasAndName(Classes.Browser browserInstance)
        {
            Helpers.Instance.CheckSingleSpazaPreferedAlias(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.aliasName.ng-binding");
            Helpers.Instance.CheckSingleSpazaName(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName > a");
        }

        // Test Case: 6. Verify that the Marbil add is displayed     
        // Test Output: 6. The Marbil add is displayed  
        public void VerifyMarbilAd(Classes.Browser browserInstance)
        {
            LogWriter.Instance.Log("ISSUE 122: TEST CASE: _01_AlertsLandingPageVerfication -> Test step spelling incorrect, should be Marbil ad. '6. Verify that the Marbil add is displayed' - Update test case", LogWriter.eLogType.Error);
            Helpers.Instance.CheckMarbilAd(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.headerAdd.normalHeaderWidth > img");
        }

        // Test Case: 8. Verify that the catalogue , basket, orders and favourites blocks are displayed
        // Test Output: 8. The catalogue, basket, orders and favourites blocks are displayed on the bottom of the bottom of the page    
        public void VerifyBottomNavigationBlocks(Classes.Browser browserInstance)
        {
            Helpers.Instance.CheckBottomNav(browserInstance, "body > div.ui-footer.ng-scope > ul > li:nth-child(1) > div", "body > div.ui-footer.ng-scope > ul > li:nth-child(2) > div",
                                           "body > div.ui-footer.ng-scope > ul > li:nth-child(3) > div", "body > div.ui-footer.ng-scope > ul > li:nth-child(4) > div");
        }

        // Test Case: 9. Verify that the Alert Notification and label are displayed
        // Test Output: 9. The Alert Notification and label are displayed 
        public void VerifyNotificationAndLabel(Classes.Browser browserInstance)
        {
            Helpers.Instance.CheckAlertNotification(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.alertStatus > a > div.alerticon",
                                                    "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.alertStatus > a > div");
        }

        public void VerifyBasketTotal(Classes.Browser browserInstance)
        {
            // Test Case: 10. Verify that the basket total value field is displayed
            // Test Output: 10. The basket total value field is displayed
            Helpers.Instance.Exists(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.itemCount.ng-binding");

            // Test Case: 11. Verify that the basket label is displayed
            // Test Output: 11. The basket label is displayed 
            Helpers.Instance.Exists(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.basketLinkContainer > div.basketicon");
            var backetLabel = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.basketLinkContainer > div.basketicon");
            browserInstance.Instance.Assert.True(() => backetLabel.Element.Text.Contains("Basket"));

            // Test Case: 12. Verify that the basket total amount of items field is displayed
            // Test Output: 12. The basket total amount field is displayed
            Helpers.Instance.Exists(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.basketValue.ng-binding");
        }

        public void VerifySearchField(Classes.Browser browserInstance)
        {
            // Test Case: 13. Verify that the search field is displayed on the top right hand corner of the screen  
            // Test Output: 13. The Search field is displayed     
            Helpers.Instance.Exists(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.searchInputContainer > div > div > input");

            // Test Case: 14. Verify the text in the search field, it states that I am looking for    
            // Test Output: 14. The text that is displayed within the field I am looking for 
            var searchInput = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.searchInputContainer > div > div > input");

            LogWriter.Instance.Log("TESTCASE:_01_AlertsLandingPageVerfication -> Test step the value with the search is 'I 'm looking for ...'. '14. Verify the text in the search field, it states that I am looking for' - Please update the test case.", LogWriter.eLogType.Error);

            browserInstance.Instance.Assert.True(() => searchInput.Element.Attributes.Get("placeholder") == "I'm looking for...");

            // Test Case: 15. Verify that the search text field is editable 
            // Test Output: 15. The search text field is editable 
            Helpers.Instance.FieldInput(browserInstance, searchInput, "TEST");
            browserInstance.Instance.Assert.Value("TEST").In(searchInput);
        }

        // Test Case: 16. Verify that the notification section is displayed
        // Test Output: 16. The Notification section is displayed 
        public void VerifyNotificationSection(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.contentHeader > div");
            var notificationSection = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentHeader > div");
            browserInstance.Instance.Assert.True(() => notificationSection.Element.Text.Contains("NOTIFICATIONS"));
        }

        // Test Case: 17. Verify that actions section in the screen is displayed with an notification exclamation   
        // Test Output: 17. Verify that actions are displayed section and label is displayed  
        public void VerifyActionSection(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.subtitle > ul.alerts > li");

            var actionsSection = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.subtitle > ul.alerts > li");
            browserInstance.Instance.Assert.True(() => actionsSection.Element.Text.Contains("ACTIONS"));

            Helpers.Instance.Exists(browserInstance, "#alertsView > div.subtitle > span.alerticon");
        }

        public void VerifyOrderAlerts(Classes.Browser browserInstance)
        {
            // Test Case: 18. Verify that  Order alerts label is displayed 
            // Test Output: 18. The order label is displayed     
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > span");

            var orderAlerts = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > span");
            browserInstance.Instance.Assert.True(() => orderAlerts.Element.Text.Contains("Order alerts"));

            // Test Case: 19. Verify that the Order Alerts label, has sub labels namely You have received new invoices, you have an unconfirmed order, your catalogue is out of sync  
            // Test Output: 19. The order Alerts label, has sub labels namely You have received new invoices, you have an unconfirmed order, your catalogue is out of sync    
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(1) > label");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(2) > label");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(3) > label");

            LogWriter.Instance.Log("TESTCASE:_01_AlertsLandingPageVerfication -> Test step the labels do not match the test case. '19. Verify that the Order Alerts label, has sub labels namely You have received new invoices, you have an unconfirmed order, your catalogue is out of sync' - Labels on the page different to the test case labels", LogWriter.eLogType.Error);

            var newInovoicesLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(1) > label");
            var unconfirmedOrderLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(2) > label");
            var catalogueOutOfOrderLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(3) > label");

            //browserInstance.Instance.Assert.True(() => newInovoicesLabel.Element.Text.Contains("You have received new invoices"));
            //browserInstance.Instance.Assert.True(() => unconfirmedOrderLabel.Element.Text.Contains("you have an unconfirmed order"));
            //browserInstance.Instance.Assert.True(() => catalogueOutOfOrderLabel.Element.Text.Contains("your catalogue is out of sync"));

            //browserInstance.Instance.Assert.Value("You have received new invoices").In(newInovoicesLabel);
            //browserInstance.Instance.Assert.Value("you have an unconfirmed order").In(unconfirmedOrderLabel);
            //browserInstance.Instance.Assert.Value("your catalogue is out of sync").In(catalogueOutOfOrderLabel);
        }

        public void VerifySystemAlerts(Classes.Browser browserInstance)
        {
            // Test Case: 20. Verify that the system alerts label is displayed
            // Test Output: 20. The system alerts label is displayed  
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > span");

            var systemAlerts = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > span");
            browserInstance.Instance.Assert.True(() => systemAlerts.Element.Text.Contains("System alerts"));

            // Test Case: 21.Veriy that the system alerts label, has sub labels namely manage your catalogue, you have connection issues and change active spaza label is displayed  
            // Test Output: 21. The system alerts label is displayed, has sub labels namely manage your catalogue, you have connection issues and change active spaza label is displayed     
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(1) > label");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(2) > label");

            LogWriter.Instance.Log("ISSUE 124: TEST CASE: _01_AlertsLandingPageVerfication -> Test step we only have to sub items here and the labels do not match the test case. '21.Veriy that the system alerts label, has sub labels namely manage your catalogue, you have connection issues and change active spaza label is displayed' - Update test case", LogWriter.eLogType.Error);

            var manageYourCatalogueLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(1) > label");
            var connactionIssuesOrderLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(2) > label");
            //var changeActiveSpazaLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(3) > label");

            //browserInstance.Instance.Assert.True(() => manageYourCatalogueLabel.Element.Text.Contains("manage your catalogue"));
            //browserInstance.Instance.Assert.True(() => connactionIssuesOrderLabel.Element.Text.Contains("you have connection issues"));
            //browserInstance.Instance.Assert.True(() => changeActiveSpazaLabel.Element.Text.Contains("change active spaza"));
        }

        public void VerifySideButtons(Classes.Browser browserInstance)
        {
            // Test Case: 22. Verify that the following buttons are displayed on the screen, view invoices button, confirm now button, sync now button, manage button, diagnose button and change now  
            // Test Output: 22. The notifications buttons are displayed
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(1) > button");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(2) > button");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(3) > button");

            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(1) > button");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(2) > button");

            var viewInvoicesButton = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(1) > button");
            var confirmNowButton = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(2) > button");
            var syncNowButton = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(3) > button");

            var manageButton = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(1) > button");
            var diagnoseButton = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(2) > button");

            LogWriter.Instance.Log("ISSUE 125: TEST CASE: _01_AlertsLandingPageVerfication -> Test step do not have the Change Now button. '21.Veriy that the system alerts label, has sub labels namely manage your catalogue, you have connection issues and change active spaza label is displayed' - Update test case", LogWriter.eLogType.Error);

            //browserInstance.Instance.Assert.Value("VIEW INVOICES").In(viewInvoicesButton);
            //browserInstance.Instance.Assert.Value("CONFIRM NOW").In(confirmNowButton);
            //browserInstance.Instance.Assert.Value("SYNC NOW").In(syncNowButton);

            //browserInstance.Instance.Assert.Value("MANAGE").In(manageButton);
            //browserInstance.Instance.Assert.Value(" DIAGNOSE").In(diagnoseButton);
        }

        public void VerifyUrgentActions(Classes.Browser browserInstance, Interfaces.IOrdersActions ordersActions, Interfaces.IBasketActions basketActions)
        {
            ordersActions.PlaceUnConfirmedOrder(browserInstance, basketActions);
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.alertStatus > a > div.alertredicon"), TimeSpan.FromMinutes(30));
        }

        public void VerifyTextHighlightedRed(Classes.Browser browserInstance, string labelPath)
        {
           // Helpers.Instance.CheckClass(browserInstance, "", Helpers.Instance.GetProxy(browserInstance, labelPath));
        }

        // Test:   2. Click on the <confirm now> button
        // Output: 2. The notification confirm now page is displayed
        public void VerifyConfirmNowButtonClick(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(2) > button");
            ClickAlertButton(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(2) > button");
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Not.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/alerts"));
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody:nth-child(1) > tr:nth-child(1) > td");
        }

        public void VerifyAsyncNow(Classes.Browser browserInstance)
        {
            //Test:   3. Click on the <sync now> button
            //Output: 3. The progress bar is  displayed  to show that an update to the catalogue is in progress    and the application becomes in-active 
            Helpers.Instance.CheckButtonEnabled(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(3) > button");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(3) > button"));
            Helpers.Instance.Exists(browserInstance, "#loading-wating-messages");
        }

        public void ClickAlertButton(Classes.Browser browserInstance, string buttonPath)
        {
            Helpers.Instance.CheckButtonEnabled(browserInstance, buttonPath);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, buttonPath));
        }

        public void VerifyManageButtonClick(Classes.Browser browserInstance)
        {
            // 2. Click on the <manage> button 
            // 2. The manage your catalogue actions page is displayed
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(1) > button");
            ClickAlertButton(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(1) > button");
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-managecatalogue"), TimeSpan.FromMinutes(30));
        }

        // Test Case: 3.1 Enter the allowable wholesaler <Makro>  in search field which  give any results and verify the user interface
        // Test Output: 3.1 The wholesaler records which are found are displayed as catalogue – outlet name  on the screen, with a location list sorted by group and grouped into groups on increments 25km
        public void VeriftyManageCatalogueSearch(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#searchCatalog");
            var searchInput = Helpers.Instance.GetProxy(browserInstance, "#searchCatalog");

            Helpers.Instance.FieldInput(browserInstance, searchInput, "Makro");
            browserInstance.Instance.Assert.Value("Makro").In(searchInput);

            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock.managecatalogue.width862px > form.ng-valid.ng-dirty > div > div.formRow.catalogsearch > button"));
            Helpers.Instance.Exists(browserInstance, "#accordion > div > div.title.rightarrow");

            var recordsFound = browserInstance.Instance.FindMultiple("#accordion > div > div.collapse > ul > li.ng-scope > span:nth-child(2)");
            browserInstance.Instance.Assert.True(() => recordsFound.Elements.Count > 0);

            recordsFound.Elements.ForEach((elementTuple) =>
            {
                FluentAutomation.ElementProxy recordfound = new FluentAutomation.ElementProxy(elementTuple.Item1, elementTuple.Item2);
                var cataloguOutletName = recordfound.Element.Text.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                browserInstance.Instance.Assert.True(() => cataloguOutletName.Length > 1);
            });

            var locations = browserInstance.Instance.FindMultiple("#accordion > div > div.title.rightarrow");
            browserInstance.Instance.Assert.True(() => locations.Elements.Count > 0);

            locations.Elements.ForEach((elementTuple) =>
            {
                FluentAutomation.ElementProxy location = new FluentAutomation.ElementProxy(elementTuple.Item1, elementTuple.Item2);
                var locationDistances = location.Element.Text.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                browserInstance.Instance.Assert.True(() => locationDistances.Length > 1);

                int startPoint = Convert.ToInt32(locationDistances[0].Trim());
                int endPoint = Convert.ToInt32(locationDistances[1].Trim().Substring(0, locationDistances[1].Trim().Length - 2));

                browserInstance.Instance.Assert.True(() => endPoint - startPoint == 25);
            });

        }
        // Test Case: 4.1 Select 0 - 25km arrow  and expand it         
        // Test Output: 4.1 The 0 - 25km is expanded and also displaying stores within that distance proximity   
        // Test Case: 4.2 Select 25 - 50km arrow and expand it 
        // Test Output: 4.2 The 25 - 50km is expanded and also displaying stores within that distance proximity   
        // Test Case: 4.3 Select  50 - 75km arrow and expand it  
        // Test Output: 4.3 The 50 - 75km is expanded and also displaying stores within that distance proximity  
        // Test Case: 4.4 Select  75 - 100km arrow and expand it  
        // Test Output: 4.4 The 75 - 100km is expanded and also displaying stores within that distance proximity 
        public void VeriftyManageExpandableArrows(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#searchCatalog");
            var searchInput = Helpers.Instance.GetProxy(browserInstance, "#searchCatalog");

            Helpers.Instance.FieldInput(browserInstance, searchInput, "");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock.managecatalogue.width862px > form.ng-valid.ng-dirty > div > div.formRow.catalogsearch > button"));
            Thread.Sleep(3000);

            var locationGroupings = browserInstance.Instance.FindMultiple("#accordion > div.catalog.ng-scope");
            browserInstance.Instance.Assert.True(() => locationGroupings.Elements.Count == 4);

            locationGroupings.Elements.ForEach((elementTuple) =>
            {
                FluentAutomation.ElementProxy locationGroup = new FluentAutomation.ElementProxy(elementTuple.Item1, elementTuple.Item2);

                browserInstance.Instance.Assert.True(() => locationGroup.Children.Count > 1);

                var location = locationGroup.Children[0]();
                var locationResults = locationGroup.Children[1]();

                Helpers.Instance.ClickButton(browserInstance, location);
                Helpers.Instance.CheckClass(browserInstance, "in", locationResults);
            });
        }

        public void VeriftyManageExpandableWholesalerSelect(Classes.Browser browserInstance)
        {
            // 5. Select one store from each range     
            Helpers.Instance.Exists(browserInstance, "#searchCatalog");
            var searchInput = Helpers.Instance.GetProxy(browserInstance, "#searchCatalog");

            Helpers.Instance.FieldInput(browserInstance, searchInput, "");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock.managecatalogue.width862px > form:nth-child(2) > div > div.formRow.catalogsearch > button"));
            Thread.Sleep(3000);

            // Test Case: 5.1 Select 0 - 25km  and select one wholesaler under that range by checkbox  
            // Test Output: 5.1 The selected wholesaler is displayed with a checkbox next to it
            // Test Case: 5.2 Select 25 - 50km  and select one wholesaler under that range by checkbox 
            // Test Output: 5.2  Select 25 - 50km  and select one wholesaler under that range by checkbox
            // Test Case: 5.3 Select 50 - 75km  and select one wholesaler under that range by checkbox 
            // Test Output: 5.3  Select 50 - 75km  and select one wholesaler under that range by checkbox
            // Test Case: 5.4 Select 75 - 100km  and select one wholesaler under that range by checkbox  
            // Test Output: 5.4  Select 50 - 75km  and select one wholesaler under that range by checkbox

            var locationGroupings = browserInstance.Instance.FindMultiple("#accordion > div.catalog.ng-scope");
            browserInstance.Instance.Assert.True(() => locationGroupings.Elements.Count == 4);

            locationGroupings.Elements.ForEach((elementTuple) =>
            {
                FluentAutomation.ElementProxy locationGroup = new FluentAutomation.ElementProxy(elementTuple.Item1, elementTuple.Item2);

                browserInstance.Instance.Assert.True(() => locationGroup.Children.Count > 1);

                var location = locationGroup.Children[0]();
                var locationResults = locationGroup.Children[1]();
                Helpers.Instance.ClickButton(browserInstance, location);
                Helpers.Instance.CheckClass(browserInstance, "in", locationResults);

                browserInstance.Instance.Assert.True(() => locationResults.Children.Count > 0);

                var locationResultItems = locationResults.Children[0]();

                locationResultItems.Children.ForEach((locationResultItem) =>
                {
                    if (locationResultItem().Children.Count > 0)
                    {
                        var checkbox = locationResultItem().Children[0]();
                        Helpers.Instance.ClickButton(browserInstance, checkbox);
                    }
                });

            });
        }

        // Test Case: 2. Click on the <manage> button 
        // Test Output: 2. The manage your catalogue actions page is displayed
        public void VerifyDiagnoseButtonClick(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(2) > button");
            ClickAlertButton(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(2) > button");
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/alerts-diagnose"), TimeSpan.FromMinutes(30));
        }

        // Test Case: 3.1 Verify Checking Connection label is available, checking vital communication to the ordering process
        // Test Output: 3.1 The Checking Connection label is available, checking vital communication to the ordering service, connection speed, test your speed connection now  are displayed on the diagnose connection screen
        public void VerifyDiagnoseCheckingNotificationLabels(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > div:nth-child(1) > span");
            var checkingConnectionLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > div:nth-child(1) > span");
            browserInstance.Instance.Assert.True(() => checkingConnectionLabel.Element.Text.Contains("Checking Connection"));

            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > div:nth-child(1) > ul > li > label");
            var checkingVitalCommunicationLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > div:nth-child(1) > ul > li > label");
            browserInstance.Instance.Assert.True(() => checkingVitalCommunicationLabel.Element.Text.Contains("checking vital communication to the ordering service"));

            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > div > span");
            var connectionSpeedLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > div > span");
            browserInstance.Instance.Assert.True(() => connectionSpeedLabel.Element.Text.Contains("connection speed"));

            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > div > ul > li > label");
            var testYourConnectionSpeedLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > div > ul > li > label");
            browserInstance.Instance.Assert.True(() => testYourConnectionSpeedLabel.Element.Text.Contains("test your speed connection now"));
        }

        // Test Case: 3.2 Verify that the test now button for checking connection is available   
        // Test Output: 3.2  The test now checking connection button is displayed on the screen
        public void VerifyDiagnoseCheckingNotificationTestButton(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > div:nth-child(1) > ul > li > button");
        }

        // Test Case: 3.3 Verify that the test now button for checking connection speed is available
        // Test Output: 3.3 The test now button for checking connection speed is available
        public void VerifyDiagnoseConnectionSpeedTestButton(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > div > ul > li > button");
        }

        // Test Case: 4.1 Click on check connection <test now> button
        // Test Output: 4.1 An indicator is displayed, that determines whether the connection was good or bad 
        public void VerifyDiagnoseCheckingNotificationTestButtonClick(Classes.Browser browserInstance)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > div:nth-child(1) > ul > li > button"));
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > div:nth-child(1) > ul > li > span.pull-right.connection.online");
        }

        // Test Case: 4.2 Click on the on the connection speed <test now>button  
        // Test Output: 4.2 Displayed results are determined by the application whether the connection speed is poor or not
        public void VerifyDiagnoseConnectionSpeedTestButtonClick(Classes.Browser browserInstance)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > div > ul > li > button"));
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(3) > div.resultSection");
        }

        // Test Case: 3.4 Verify that the results place holder label is available, displaying the results of the test 
        // Test Output: 3.4 The results place holder label is available 
        public void VerifyDiagnoseResultPlaceholder(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(3) > div > span");
        }
    }
}
