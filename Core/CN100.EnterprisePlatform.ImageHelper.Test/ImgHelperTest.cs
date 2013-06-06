using CN100.EnterprisePlatform.ImageHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CN100.EnterprisePlatform.ImageHelper.Test
{
    
    
    /// <summary>
    ///This is a test class for ImgHelperTest and is intended
    ///to contain all ImgHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ImgHelperTest
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
        ///A test for GenerateTo
        ///</summary>
        [TestMethod()]
        public void GenerateToTest()
        {
            string imgPath = string.Empty; // TODO: Initialize to an appropriate value
            //ImgHelper.ConvertTo(imgPath);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ConvertTo
        ///</summary>
        [TestMethod()]
        public void ConvertToTest()
        {
            //string imgPath = @"E:\Son\02062442_021.jpg"; // TODO: Initialize to an appropriate value
            //string newImgPath = @"E:\Son\02062442_021_i_200.jpg"; // TODO: Initialize to an appropriate value
            //string size = "200"; // TODO: Initialize to an appropriate value
            //string expected = string.Empty; // TODO: Initialize to an appropriate value
            //string actual;
            //actual = ImgHelper.ConvertTo(imgPath, newImgPath, size);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
