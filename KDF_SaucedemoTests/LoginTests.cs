using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using KDFTest1;
using Moq;

namespace KDF_SaucedemoTests
{
    [TestClass]
    public class LoginTests
    {
      
        private Mock<ILoadData> mockLoadData;
        private Mock<IWebDriver> mockDriver;


        [SetUp]
        public void SetUp()
        {
         
            mockLoadData = new Mock<ILoadData>();
            mockDriver = new Mock<IWebDriver>();
           
        }

        [TestMethod]
        public void LoginWithValidCredentials_ShouldSucceed()
        {
            SetUp();
            // Arrange
            mockLoadData.Setup(x => x.Read(It.IsAny<string>())).Returns(new string[] { "open_browser,,", "maximize_window,,", "launch_website,https://example.com,", "type_input,username,myusername", "type_input,password,mypassword", "click_button,login_button,", "wait,,", "close_browser,," });
            var WebAppTests = new FetchData(mockLoadData.Object);
            // Act
            WebAppTests.ExecuteTasks();

            // Assert
            mockLoadData.Verify(x => x.ExecuteTestCases(It.IsAny<string[]>()), Times.Once);

        }

        [TestMethod]
        public void LoginWithInvalidCredentials_ShouldFail()
        {
            SetUp();
            // Arrange
            mockLoadData.Setup(x => x.Read(It.IsAny<string>())).Returns(new string[] { "open_browser,,", "maximize_window,,", "launch_website,https://example.com,", "type_input,username,invalidusername", "type_input,password,invalidpassword", "click_button,login_button,", "wait,,", "close_browser,," });
            var WebAppTests = new FetchData(mockLoadData.Object);

            // Act
            WebAppTests.ExecuteTasks();

            // Assert
            mockLoadData.Verify(x => x.ExecuteTestCases(It.IsAny<string[]>()), Times.Once);
        }

       
    }
}
