using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace KDFTest1
{
    public class LoadCSV
    {

        public string[] ReadFile(string path)
        {
            string[] lines = { };

            try {
                lines = File.ReadAllLines(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }    

            return lines;

        }

        public void ExecuteTestCases(string[] lines, IWebDriver driver)
        {
            foreach (string line in lines)
            {
                string[] tokens = line.Split(',');
                string keyword = tokens[0];
                string data = tokens[1];

                switch (keyword)
                {
                    case "open_browser":
                        driver = new ChromeDriver(); // create a new instance of ChromeDriver
                        break;
                    case "maximize_window":
                        driver.Manage().Window.Maximize(); // maximize the window
                        break;
                    case "launch_website":
                        driver.Navigate().GoToUrl(data); // navigate to the specified URL
                        break;
                    case "enter_username":
                        driver.FindElement(By.Id("user-name")).SendKeys(data); // enter the username
                        break;
                    case "enter_password":
                        driver.FindElement(By.Id("password")).SendKeys(data); // enter the password
                        break;
                    case "click_login":
                        driver.FindElement(By.Id("login-button")).Click(); // click the login button
                        break;
                    case "wait":
                        System.Threading.Thread.Sleep(2000); // wait for 10 seconds
                        break;
                    case "close_browser":
                        driver.Quit(); // close the browser
                        break;
                    default:
                        Console.WriteLine("Invalid keyword: " + keyword); // handle invalid keyword
                        break;
                }
            }
        }
    }
}
