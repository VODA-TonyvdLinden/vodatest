using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Tests.GeneralApplicationNavigation
{
    [TestFixture, Description("GeneralApplicationNavigation"), Category("GeneralApplicationNavigation")]
    public class GeneralApplicationNavigation
    {
        Classes.Browser browserInstance;
        IUnityContainer container = new UnityContainer();

        [TestFixtureSetUp]
        public void Initialise()
        {
            browserInstance = new Classes.Browser(Classes.Browser.eBrowser.Chrome);
            browserInstance.Config.ScreenshotPath(Properties.Settings.Default.ScreenshotPath);
            browserInstance.Instance.Wait(5);

            container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interfaces.IGeneralApplicationNavigationActions, Tests.GeneralApplicationNavigation.GeneralApplicationNavigationActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());

        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            browserInstance.Instance.Dispose();
            container.RemoveAllExtensions();
            container.Dispose();
        }


        /// <summary>
        /// TEST STEPS:
        /// 1. Verify that the vodacom logo and the red banner are displayed on the activation screen
        /// 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. Verify that contact us and help me hyperlinks are displayed
        /// 4. See spelling, grammar and alignment
        /// 5. Verify that the preferred alias name is displayed on top right hand corner of the app with the spaza 
        ///   owner's alias name and spaza name
        /// 6. Verify that the marbil add is displayed
        /// 7. Verify that the catalogue , basket, orders and favourites blocks are displayed
        /// 8. Verify that the Alert Notification and label are displayed
        /// 9. Verify that the basket total value field is displayed
        /// 10. Verify that the basket label is displayed
        /// 11. Verify that the basket total amount of items field is displayed
        /// 12. Verify that the search field is displayed on the top right hand corner of the screen
        /// 13. Verify the text in the search field, it states that i am looking for
        /// 14. Verify that the search text field is editable
        /// TEST OUTPUT:
        /// 1. The vodacom banner logo and banner are displayed 
        /// 2. The online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. The contact us and help me hyerlinks are displayed
        /// 4. Spelling, Grammar and alignment correct (Screen should resize on all devices also able to rotate from Portrait to landscape)
        /// 5. The preferred alias name is displayed on the top right hand corner of the app, with the name spaza owner's 
        ///   alias name and the spaza name
        /// 6. The marbil add is displayed                                                                                                                                                                     
        /// 7. The catalogue, basket, orders and favourites blocks are displayed on the bottom of the bottom of the page
        /// 8. The Alert Notification and label are displayed
        /// 9. The basket total value field is displayed
        /// 10. The basket label is displayed
        /// 11 The basket total amount fied is displayed
        /// 12. The Search field is displayed
        /// 13. The text that is displayed within the field i am looking for
        /// 14. The search text field is editable
        /// </summary>
        [Test, Description("SearchFieldValidations"), Repeat(1)]
        public void SearchFieldValidations()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IGeneralApplicationNavigationActions generalApplicationNavigationActions = container.Resolve<Interfaces.IGeneralApplicationNavigationActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Alerts (search only within user’s catalogues)
        ///   1.1 Please enter <product name)  and press <enter>
        ///   1.2 Please enter <SKU code> and press <enter>
        ///   1.3 Select any icon displayed on the view
        /// 2. Landing page (search only within user’s catalogues)
        ///   2.1 Please enter <product name)  and press <enter>
        ///   2.2 Please enter <SKU code> and press <enter>
        ///   2.3 Select any icon displayed on the view
        /// 3. Catalogues (search only within user’s catalogues)
        ///   3.1 Please add a product to wholesaler list  and make sure you save that product name,sku name
        ///   3.2 Please enter <product name) that you have added above  and press <enter>
        ///   3.3 Please enter <SKU code> and press <enter>
        ///   3.4  Select any icon displayed on the view
        /// 4. Favourites (search only within user’s favourites)
        ///   4.1 Please add an item in your favourites  and make sure that product name, sku name are saved
        ///   4.2 Please enter <product name) that you have added above  and press <enter>
        ///   4.3 Please enter <SKU code> and press <enter>
        ///   4.4  Select any icon displayed on the view
        /// 5. Orders Search Offline
        /// NB. Orders online can be searched within last 30 days, so -1, 0, 31 days will be invalid values, between 1 - 30 valid days
        ///   5.1 Please place an order
        ///   5.2 Please Enter  Please enter <product name)  and press <enter>
        ///   5.3 Please enter <SKU code> and press <enter>
        ///   5.4 Select any icon displayed on the view
        /// 6.  Orders search online default 90 days
        ///   6.1 Please select a date range  and verify that mas retrieves data according to that date range  select period
        /// 7. Orders general search
        ///   7.1 Please enter <product name)  and press <enter>
        ///   7.2 Please enter <SKU code> and press <enter>
        ///   7.3 Select any icon displayed on the view
        /// TEST OUTPUT:
        /// 1.
        ///   1.1 A list of wholesalers are displayed as icons
        ///   1.2 A list of wholesalers are displayedas icons
        ///   1.3  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within
        /// 2.
        ///   2.1 A list of wholesalers are displayed as icons
        ///   2.2 A list of wholesalers are displayedas icons
        ///   2.3  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within
        /// 3.
        ///   3.1 The product to use for the test is added on the wholesaler list
        ///   3.2 A list of wholesalers are displayed as icons
        ///   3.3 A list of wholesalers are displayedas icons
        ///   3.4  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within
        /// 4.
        ///   4.1 The product to use for the test is added on the wholesaler list
        ///   4.2 A list of wholesalers are displayed as icons
        ///   4.3 A list of wholesalers are displayedas icons
        ///   4.4  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within
        /// 5.
        /// NB. Orders online can be searched within last 30 days, so -1,0, 31 days will be invalid values, between 1- 30 valid days
        ///   5.1 An order is placed
        ///   5.2 A list of wholesalers are displayed as icons
        ///   5.3 A list of wholesalers are displayedas icons
        ///   5.4  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within
        /// 6.
        ///   6.1 The results  layout displayed a tabular list with the option to toggle between Products, Brand, Order 
        ///     number or Invoice number results.
        /// 7.
        ///   7.1 A list of wholesalers are displayed as icons
        ///   7.2 A list of wholesalers are displayedas icons
        ///   7.3  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within
        /// </summary>
        [Test, Description("GridviewSearchOfflineOnline"), Repeat(1)]
        public void GridviewSearchOfflineOnline()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IGeneralApplicationNavigationActions generalApplicationNavigationActions = container.Resolve<Interfaces.IGeneralApplicationNavigationActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Alerts (search only within user’s catalogues)
        ///   1.1 Please enter <product name)  and press <enter>
        ///   1.2 Please enter <SKU code> and press <enter>
        ///   1.3 Select any icon displayed on the view
        /// 2. Landing page (search only within user’s catalogues)
        ///   2.1 Please enter <product name)  and press <enter>
        ///   2.2 Please enter <SKU code> and press <enter>
        ///   2.3 Select any icon displayed on the view
        /// 3. Catalogues (search only within user’s catalogues)
        ///   3.1 Please add a product to wholesaler list  and make sure you save that product name,sku name
        ///   3.2 Please enter <product name) that you have added above  and press <enter>
        ///   3.3 Please enter <SKU code> and press <enter>
        ///   3.4  Select any icon displayed on the view
        /// 4. Favourites (search only within user’s favourites)
        ///   4.1 Please add an item in your favourites  and make sure that product name, sku name are saved
        ///   4.2 Please enter <product name) that you have added above  and press <enter>
        ///   4.3 Please enter <SKU code> and press <enter>
        ///   4.4  Select any icon displayed on the view
        /// 5. Orders Search Offline
        /// NB. Orders online can be searched within last 30 days, so -1, 0, 31 days will be invalid values, between 1 - 30 valid days
        ///   5.1 Please place an order
        ///   5.2 Please Enter  Please enter <product name)  and press <enter>
        ///   5.3 Please enter <SKU code> and press <enter>
        ///   5.4 Select any icon displayed on the view
        /// 6.  Orders search online default 90 days
        ///   6.1 Please select a date range  and verify that mas retrieves data according to that date range  select period
        /// 7. Orders general search
        ///   7.1 Please enter <product name)  and press <enter>
        ///   7.2 Please enter <SKU code> and press <enter>
        ///   7.3 Select any icon displayed on the view
        /// TEST OUTPUT:
        /// 1.
        ///   1.1 Results of Wholesalers products are displayed as a  List view
        ///   1.2 Results of Wholesalers products are displayed as a  List view
        ///   1.3  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within
        /// 2.
        ///   2.1 Results of Wholesalers products are displayed as a  List view
        ///   2.2 Results of Wholesalers products are displayed as a  List view
        ///   2.3  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within
        /// 3.
        ///   3.1 The product to use for the test is added on the wholesaler list
        ///   3.2 Results of Wholesalers products are displayed as a  List view
        ///   3.3 Results of Wholesalers products are displayed as a  List view
        ///   3.4  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within
        /// 4.
        ///   4.1 The product to use for the test is added on the wholesaler list
        ///   4.2 Results of Wholesalers products are displayed as a  List view
        ///   4.3 Results of Wholesalers products are displayed as a  List view
        ///   4.4  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within
        /// 5.
        /// NB. Orders online can be searched within last 30 days, so -1,0, 31 days will be invalid values, between 1- 30 valid days
        ///   5.1 An order is placed
        ///   5.2 Results of Wholesalers products are displayed as a  List view
        ///   5.3 Results of Wholesalers products are displayed as a  List view
        ///   5.4  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within
        /// 6.
        ///   6.1 The results  layout displayed a tabular list with the option to toggle between Products, Brand, Order 
        ///     number or Invoice number results.
        /// 7.
        ///   7.1 Results of Wholesalers products are displayed as a  List view
        ///   7.2 Results of Wholesalers products are displayed as a  List view
        ///   7.3  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within
        /// </summary>
        [Test, Description("ListViewSearchOfflineOnline"), Repeat(1)]
        public void ListViewSearchOfflineOnline()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IGeneralApplicationNavigationActions generalApplicationNavigationActions = container.Resolve<Interfaces.IGeneralApplicationNavigationActions>();
            //TODO
        }
    }
}
