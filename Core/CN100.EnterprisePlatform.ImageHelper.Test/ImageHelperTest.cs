using CN100.EnterprisePlatform.Web.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CN100.EnterprisePlatform.ImageHelper.Test
{
    
    
    /// <summary>
    ///This is a test class for ImageHelperTest and is intended
    ///to contain all ImageHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ImageHelperTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ImagePathHelper
        ///</summary>
        [TestMethod()]
        public void ImagePathHelperTest()
        {
            string imgPath = "00220120721002e2f2f4a9609a4a1c899130331a47cf43.jpg"; // TODO: Initialize to an appropriate value
            string expected = "http://img190.com/00220120721002e2f2f4a9609a4a1c899130331a47cf43.jpg"; // TODO: Initialize to an appropriate value
            string actual;
            actual = ImageHelper.ImagePathHelper(imgPath);
            Assert.AreEqual(expected, actual);
        }
    }
}
