using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Assert = NUnit.Framework.Assert;
using System.Threading;
using KDFTest1;

namespace KDF_SaucedemoTests
{
    [TestClass]
    public class LoginTests
    {
        private IWebDriver _driver;
        private LoadCSV LoadCSV;

        [SetUp]
        public void SetUp()
        {
            LoadCSV = new LoadCSV();
            
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        }

        [TestMethod]
        public void LoginWithValidCredentials_ShouldSucceed()
        {
            SetUp();
            string[] excelData = LoadCSV.ReadFile(@"C:\Users\jvergara\Desktop\Quality assurance Processes\automated testing framework\testdata.csv");
            
            // Arrange
            var usernameInput = _driver.FindElement(By.CssSelector("#user-name"));
            var passwordInput = _driver.FindElement(By.CssSelector("#password"));
            var loginButton = _driver.FindElement(By.CssSelector("#login-button"));
            var expectedUrl = excelData[3].Split(',')[1];

            // Act
            SetUp();
            usernameInput.SendKeys(excelData[5].Split(',')[1]);
            Thread.Sleep(2);
            passwordInput.SendKeys(excelData[7].Split(',')[1]);
            Thread.Sleep(2);
            loginButton.Click();

            // Assert
            Assert.That(_driver.Url, Is.EqualTo(expectedUrl));
        }

        [TestMethod]
        public void LoginWithInvalidCredentials_ShouldFail()
        {

            SetUp();

            // Arrange
            var usernameInput = _driver.FindElement(By.CssSelector("#user-name"));
            var passwordInput = _driver.FindElement(By.CssSelector("#password"));
            var loginButton = _driver.FindElement(By.CssSelector("#login-button"));
            

            // Act
            usernameInput.SendKeys("invalid_user");
            passwordInput.SendKeys("invalid_password");
            loginButton.Click();

            var errorMessage = _driver.FindElement(By.CssSelector("[data-test='error']"));
            var expectedErrorMessage = "Epic sadface: Username and password do not match any user in this service";

            // Assert
            Assert.That(errorMessage.Text, Is.EqualTo(expectedErrorMessage));
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
