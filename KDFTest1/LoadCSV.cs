using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace KDFTest1
{
    public class LoadCSV : ILoadData
    {

        public string[] Read(string path)
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

        public void ExecuteTestCases(string[] lines)
        {

            IWebDriver driver = null;
            foreach (string line in lines)
            {
                string[] tokens = line.Split(',');
                string keyword = tokens[0];
                string data = tokens[1];
                string type_selector = tokens[2];
                string unique_id = tokens[3];
     

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
                    case "type_input":
                        FindElement(driver, type_selector, unique_id).SendKeys(data);
                        break;
                    case "click_button":
                        FindElement(driver, type_selector, unique_id).Click(); // clicking any button
                        break;
                    case "wait":
                        System.Threading.Thread.Sleep(2000); // wait for 10 seconds
                        break;
                    case "close_browser":
                        driver.Quit(); // close the browser
                        break;
                    case "assert_text":
                        if(!data.Equals(FindElement(driver, type_selector, unique_id).Text))
                        {
                            new ThrowsExceptionConstraint();
                            //Console.WriteLine("Assert Text fail");
                        };
                        break;
                    default:
                        Console.WriteLine("Invalid keyword: " + keyword); // handle invalid keyword
                        break;
                }
            }
        }


        public IWebElement FindElement(IWebDriver driver, string typeSelector, string unique_id)
        {
            IWebElement EmptyElement = null;

                switch (typeSelector)
                {
                    case "id":
                        {
                        // return driver.FindElement(By.Id(unique_id));
                        EmptyElement = driver.FindElement(By.Id(unique_id));
                        break;
                    }
                    case "css":
                        {
                        //return driver.FindElement(By.CssSelector(unique_id));
                        EmptyElement = driver.FindElement(By.CssSelector(unique_id));
                        break;
                    }
                    case "name": {
                            //return driver.FindElement(By.Name(unique_id));
                            EmptyElement= driver.FindElement(By.Name(unique_id));
                        break;
                    }
                    case "class_name":
                        {
                        //return driver.FindElement(By.ClassName(unique_id));
                        EmptyElement = driver.FindElement(By.ClassName(unique_id));
                        break;
                    }
                    case "tag_name":
                        {
                        // return driver.FindElement(By.TagName(unique_id));
                        EmptyElement = driver.FindElement(By.TagName(unique_id));
                        break;
                    }
                    case "link_text":
                        {
                        //return driver.FindElement(By.LinkText(unique_id));
                        EmptyElement = driver.FindElement(By.LinkText(unique_id));
                        break;
                    }
                    case "partial_text":
                        {
                        //return driver.FindElement(By.PartialLinkText(unique_id));
                        EmptyElement = driver.FindElement(By.PartialLinkText(unique_id));
                        break;
                    }
                    case "css_selector":
                        {
                            //return driver.FindElement(By.CssSelector(unique_id));
                            EmptyElement = driver.FindElement(By.CssSelector(unique_id));
                        break;
                    }

                    default:
                        //return driver.FindElement(By.XPath(unique_id));
                        EmptyElement = driver.FindElement(By.XPath(unique_id));
                        break;
            }
            if (EmptyElement == null)
            {
                throw new Exception("Web element not found.." + unique_id);
            }
            return EmptyElement;
        }
    }
}
