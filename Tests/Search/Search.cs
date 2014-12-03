using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestProj.Classes;

namespace TestProj.Tests.Search
{
    [TestFixture, Description("GeneralApplicationNavigation"), Category("GeneralApplicationNavigation")]
    public class Search
    {
        Classes.Browser browserInstance;
        IUnityContainer container = new UnityContainer();

        [TestFixtureSetUp]
        public void Initialise()
        {
            ProcessKiller.Instance.Kill();
            Thread.Sleep(500);

            browserInstance = new Classes.Browser(Classes.Browser.eBrowser.Chrome);
            browserInstance.Config.ScreenshotPath(Properties.Settings.Default.ScreenshotPath);
            browserInstance.Instance.Wait(5);

            container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interfaces.ISearchActions, Tests.Search.SearchActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());

        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            browserInstance.Instance.Dispose();
            container.RemoveAllExtensions();
            container.Dispose();
        }

        /// <summary>
        /// TEST: SEARCH FIELD  VALIDATIONS
        /// Test Case ID: 5_FRS_Ref_5.1.9
        /// Category: Search
        /// Feature: Search
        /// Pre-Condition: None
        /// Environment: Application landing page
        /// TEST STEPS:
        /// 1. Verify that the Vodacom logo and the red banner are displayed on the activation screen
        /// 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. Verify that contact us and help me hyperlinks are displayed
        /// 4. See spelling, grammar and alignment  
        /// 5. Verify that the preferred alias name is displayed on top right hand corner of the app with the spaza owner's alias name and spaza name 
        /// 6. Verify that the Marbil add is displayed   
        /// 7. Verify that the catalogue , basket, orders and favourites blocks are displayed
        /// 8. Verify that the Alert Notification and label are displayed  
        /// 9. Verify that the basket total value field is displayed      
        /// 10. Verify that the basket label is displayed
        /// 11. Verify that the basket total amount of items field is displayed     
        /// 12. Verify that the search field is displayed on the top right hand corner of the screen
        /// 13. Verify the text in the search field, it states that I am looking for  
        /// 14. Verify that the search text field is editable
        /// TEST OUTPUT:
        /// 1. The Vodacom banner logo and banner are displayed 
        /// 2. The online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. The contact us and help me hyperlinks are displayed
        /// 4. Spelling, Grammar and alignment correct (Screen should resize on all devices also able to rotate from Portrait to landscape)   
        /// 5. The preferred alias name is displayed on the top right hand corner of the app, with the name spaza owner's alias name and the spaza name   
        /// 6. The Marbil add is displayed 
        /// 7. The catalogue, basket, orders and favourites blocks are displayed on the bottom of the bottom of the page    
        /// 8. The Alert Notification and label are displayed   
        /// 9. The basket total value field is displayed 
        /// 10. The basket label is displayed 
        /// 11 The basket total amount field is displayed
        /// 12. The Search field is displayed    
        /// 13. The text that is displayed within the field I am looking for    
        /// 14. The search text field is editable 
        /// </summary>
        [Test, Description("_01_SearchFieldValidations"), Category("Search"), Repeat(1)]
        public void _01_SearchFieldValidations()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.ISearchActions generalApplicationNavigationActions = container.Resolve<Interfaces.ISearchActions>();
            //TODO
        }

        /// <summary>
        /// TEST:  GRID VIEW SEARCH OFFLINE/ONLINE
        /// Test Case ID: 5_FRS_Ref_5.1.9
        /// Category: Search
        /// Feature: Search
        /// Pre-Condition: None
        /// Environment: Application landing page
        /// TEST STEPS:
        /// 1. Alerts (search only within user’s catalogues)                                                                          
        /// 1.1 Please enter <product name)  and press <enter>         
        /// 1.2 Please enter <SKU code> and press <enter>   
        /// 1.3 Select any icon displayed on the view 
        /// 2. Landing page (search only within user’s catalogues)                                                             
        /// 2.1 Please enter <product name)  and press <enter> 
        /// 2.2 Please enter <SKU code> and press <enter>    
        /// 2.3 Select any icon displayed on the view    
        /// 3. Catalogues (search only within user’s catalogues)                                                                 
        /// 3.1 Please add a product to wholesaler list  and make sure you save that product name, sku name   
        /// 3.2 Please enter <product name) that you have added above  and press <enter>  
        /// 3.3 Please enter <SKU code> and press <enter>
        /// 3.4  Select any icon displayed on the view   
        /// 4. Favourites (search only within user’s favourites)                                                                    
        /// 4.1 Please add an item in your favourites  and make sure that product name, sku name are saved
        /// 4.2 Please enter <product name) that you have added above  and press <enter>  
        /// 4.3 Please enter <SKU code> and press <enter>   
        /// 4.4  Select any icon displayed on the view   
        /// TEST OUTPUT:
        /// 1.                                                                                                                                                                                                                 
        /// 1.1 A list of wholesalers are displayed as icons 
        /// 1.2 A list of wholesalers are displayed as icons  
        /// 1.3  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within 
        /// 2.                                                                                                                                                                                                                                      
        /// 2.1 A list of wholesalers are displayed as icons   
        /// 2.2 A list of wholesalers are displayed as icons 
        /// 2.3  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within
        /// 3.                                                                                                                                                                                                                 
        /// 3.1 The product to use for the test is added on the wholesaler list  
        /// 3.2 A list of wholesalers are displayed as icons   
        /// 3.3 A list of wholesalers are displayed as icons  
        /// 3.4  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within  
        /// 4                                                                                                                                                                                                              
        /// 4.1 The product to use for the test is added on the wholesaler list    
        /// 4.2 A list of wholesalers are displayed as icons 
        /// 4.3 A list of wholesalers are displayed as icons   
        /// 4.4  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within  
        /// </summary>
        [Test, Description("_02_GridviewSearchOfflineOnline"), Category("Search"), Repeat(1)]
        public void _02_GridviewSearchOfflineOnline()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.ISearchActions generalApplicationNavigationActions = container.Resolve<Interfaces.ISearchActions>();
            //TODO
        }

        /// <summary>
        /// TEST: LIST VIEW SEARCH OFFLINE/ONLINE
        /// Test Case ID: 5_FRS_Ref_5.1.9
        /// Category: Search
        /// Feature: Search
        /// Pre-Condition: None
        /// Environment: Application landing page
        /// TEST STEPS:
        /// 1. Alerts (search only within user’s catalogues)                                                                          
        /// 1.1 Please enter <product name)  and press <enter>   
        /// 1.2 Please enter <SKU code> and press <enter> 
        /// 1.3 Select any icon displayed on the view 
        /// 2. Landing page (search only within user’s catalogues)                                                             
        /// 2.1 Please enter <product name)  and press <enter>   
        /// 2.2 Please enter <SKU code> and press <enter>
        /// 2.3 Select any icon displayed on the view  
        /// 3. Catalogues (search only within user’s catalogues)                                                                 
        /// 3.1 Please add a product to wholesaler list  and make sure you save that product name, sku name 
        /// 3.2 Please enter <product name) that you have added above  and press <enter>   
        /// 3.3 Please enter <SKU code> and press <enter>
        /// 3.4  Select any icon displayed on the view 
        /// 4. Favourites (search only within user’s favourites)                                                                    
        /// 4.1 Please add an item in your favourites  and make sure that product name, sku name are saved   
        /// 4.2 Please enter <product name) that you have added above  and press <enter>  
        /// 4.3 Please enter <SKU code> and press <enter>       
        /// 4.4  Select any icon displayed on the view   
        /// TEST OUTPUT:
        /// 1.                                                                                                                                                                                                                 
        /// 1.1 Results of Wholesalers products are displayed as a  List view      
        /// 1.2 Results of Wholesalers products are displayed as a  List view 
        /// 1.3  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within
        /// 2.                                                                                                                                                                                                                                      
        /// 2.1 Results of Wholesalers products are displayed as a  List view   
        /// 2.2 Results of Wholesalers products are displayed as a  List view 
        /// 2.3  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within   
        /// 3.                                                                                                                                                                                                                 
        /// 3.1 The product to use for the test is added on the wholesaler list
        /// 3.2 Results of Wholesalers products are displayed as a  List view
        /// 3.3 Results of Wholesalers products are displayed as a  List view     
        /// 3.4  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within 
        /// 4                                                                                                                                                                                                              
        /// 4.1 The product to use for the test is added on the wholesaler list  
        /// 4.2 Results of Wholesalers products are displayed as a  List view  
        /// 4.3 Results of Wholesalers products are displayed as a  List view 
        /// 4.4  Results are displayed and grouped per catalogue, with an indicator of the amount of products contained within  
        /// </summary>
        [Test, Description("_03_ListViewSearchOfflineOnline"), Category("Search"), Repeat(1)]
        public void _03_ListViewSearchOfflineOnline()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.ISearchActions generalApplicationNavigationActions = container.Resolve<Interfaces.ISearchActions>();
            //TODO
        }
    }
}
