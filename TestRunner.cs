﻿using Microsoft.Practices.Unity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj
{
    [TestFixture, Description("Test runner"), Category("Contains all test classes")]
    public class TestRunner
    {
        Classes.Browser automater;

        [TestFixtureSetUp]
        public void Initialise()
        {
            automater = new Classes.Browser(Classes.Browser.eBrowser.Chrome);
            automater.Instance.Wait(5);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            automater.Instance.Dispose();
        }

        [Test, Description("Start"), Repeat(2)]
        public void Go()
        {
            IUnityContainer container = new UnityContainer();
            container = Microsoft.Practices.Unity.Configuration.UnityContainerExtensions.LoadConfiguration(container);
            Interfaces.ITestUnit test1 = Microsoft.Practices.Unity.UnityContainerExtensions.Resolve<Interfaces.ITestUnit>(container);
            test1.TestMethod(automater);

        }
    }
}