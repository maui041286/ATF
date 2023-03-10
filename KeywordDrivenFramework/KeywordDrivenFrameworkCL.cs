using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Threading;

namespace KeywordDrivenFramework
{
    public class KeywordDrivenFrameworkCL
    {
        private IWebDriver driver;

        public KeywordDrivenFrameworkCL(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ExecuteTestCases(string csvFilePath)
        {
            // Read test cases from CSV file
            List<Dictionary<string, string>> testCases = ReadTestCasesFromCSV(csvFilePath);

            // Execute each test case
            foreach (Dictionary<string, string> testCase in testCases)
            {
                string keyword = testCase["Keyword"];
                string data = testCase["Data"];

                switch (keyword)
                {
                    case "openBrowser":
                        OpenBrowser(data);
                        break;
                    case "maximizeWindow":
                        MaximizeWindow();
                        break;
                    case "enterUsername":
                        EnterUsername(data);
                        break;
                    case "enterPassword":
                        EnterPassword(data);
                        break;
                    case "clickLoginButton":
                        ClickLoginButton();
                        break;
                    case "wait":
                        Wait(data);
                        break;
                    case "closeBrowser":
                        CloseBrowser();
                        break;
                    default:
                        throw new ArgumentException($"Keyword '{keyword}' not recognized");
                }
            }
        }

        private List<Dictionary<string, string>> ReadTestCasesFromCSV(string csvFilePath)
        {
            // Read CSV file into list of dictionaries
            List<Dictionary<string, string>> testCases = new List<Dictionary<string, string>>();

            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                // Read headers
                string[] headers = reader.ReadLine().Split(',');

                // Read data rows
                while (!reader.EndOfStream)
                {
                    string[] dataValues = reader.ReadLine().Split(',');
                    Dictionary<string, string> testCase = headers.Zip(dataValues, (h, v) => new { Header = h, Value = v })
                                                                 .ToDictionary(x => x.Header, x => x.Value);
                    testCases.Add(testCase);
                }
            }

            return testCases;
        }

        private void OpenBrowser(string browser)
        {
            // Open browser
            switch (browser.ToLower())
            {
                case "chrome":
                    driver = new ChromeDriver();
                    break;
                // Add support for other browsers here
                default:
                    throw new ArgumentException($"Browser '{browser}' not recognized");
            }
        }

        private void MaximizeWindow()
        {
            // Maximize window
            driver.Manage().Window.Maximize();
        }

        private void EnterUsername(string username)
        {
            // Enter username
            driver.FindElement(By.Id("user-name")).SendKeys(username);
        }

        private void EnterPassword(string password)
        {
            // Enter password
            driver.FindElement(By.Id("password")).SendKeys(password);
        }

        private void ClickLoginButton()
        {
            // Click login button
            driver.FindElement(By.Id("login-button")).Click();

            // Verify login was successful
            if (!driver.Url.Contains("inventory.html"))
            {
                throw new Exception("Login failed");
            }
        }

        private void Wait(string seconds)
        {
            // Wait for specified number of seconds
            int waitTime = int.Parse(seconds);
            Thread.Sleep(waitTime * 1000);
        }

        private void CloseBrowser()
        {
            // Close browser
            driver.Quit();
        }
    }
}
