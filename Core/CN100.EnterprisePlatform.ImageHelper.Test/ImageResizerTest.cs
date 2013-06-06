using CN100.EnterprisePlatform.ImageHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CN100.EnterprisePlatform.ImageHelper.Test
{
    
    
    /// <summary>
    ///This is a test class for ImageResizerTest and is intended
    ///to contain all ImageResizerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ImageResizerTest
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
        ///A test for ResizeImage
        ///</summary>
        [TestMethod()]
        public void ResizeImageTest()
        {
            string inFile = @"E:\Son\02062442_021.jpg"; // TODO: Initialize to an appropriate value
            string outFile = @"E:\Son\02062442_021_200.jpg"; // TODO: Initialize to an appropriate value
            double maxDimension = 200F; // TODO: Initialize to an appropriate value
            long level = 100; // TODO: Initialize to an appropriate value
            ImageResizer.ResizeImage(inFile, outFile, maxDimension, level);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
