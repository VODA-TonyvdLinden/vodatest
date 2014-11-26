using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Classes
{
    public sealed class TestData
    {
        static TestData _instance;
        public static TestData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TestData();
                }
                return _instance;
            }
        }
        TestData()
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Properties.Settings.Default.TestDataPath);
            //string path = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, Properties.Settings.Default.TestDataPath);
            DefaultData = XMLSeriallizer.Deserialize<TestDataClasses.Defaults>(path);
        }

        public TestDataClasses.Defaults DefaultData { get; set; }
    }
}
