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
using System.Collections;

namespace KDFTest1
{
    public class LoadCSV : ILoadData // Define LoadCSV class that implements ILoadData interface
    {
        private List<Reporte> final_report = new List<Reporte>(); // Declare a list to hold final reports
        private Reporte reporte; // Declare a Reporte object

        public List<Reporte> Final_report { get => final_report; set => final_report = value; } // Define a property to access final report

        public string[] Read(string path) // Define a method to read data from a file
        {
            string[] lines = { }; // Declare an empty array to store lines

            try
            {
                lines = File.ReadAllLines(path); // Try to read all lines from the specified file
            }
            catch (Exception e) // Catch any exception and print error message
            {
                Console.WriteLine(e.Message);
            }

            return lines; // Return the read lines

        }

        public void ExecuteTestCases(string[] lines) // Define a method to execute test cases
        {

            IWebDriver driver = null; // Declare a null WebDriver object
            bool aux = true; // Declare a boolean variable to check if the browser is open or not
            foreach (string line in lines) // Loop through each line of the input data
            {
                string[] tokens = line.Split(','); // Split the line by comma to get individual tokens
                string keyword = tokens[0]; // Get the keyword from the tokens
                string data = tokens[1]; // Get the data from the tokens
                string type_selector = tokens[2]; // Get the type selector from the tokens
                string unique_id = tokens[3]; // Get the unique ID from the tokens
                string high_level_key_word_desc = tokens[6]; // Get the high level keyword description from the tokens
                string high_level_key_word = tokens[7]; // Get the high level keyword from the tokens

                try
                {
                    if (aux == false && keyword == "open_browser") // Check if browser is already open and keyword is "open_browser"
                    {
                        aux = !aux; // Toggle the boolean variable
                    }
                    if (aux) // If browser is open
                    {
                        switch (keyword) // Check the keyword and execute the corresponding action
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
                                FindElement(driver, type_selector, unique_id).SendKeys(data); // find the element and enter the data
                                break;
                            case "click_button":
                                FindElement(driver, type_selector, unique_id).Click(); // find the element and click it
                                break;
                            case "wait":
                                System.Threading.Thread.Sleep(2000); // wait for 2 seconds
                                break;
                            case "close_browser":
                                SetReporte(Status.Pass, high_level_key_word, high_level_key_word_desc); // set the report status to pass
                                driver.Quit(); // close the browser
                                break;
                            case "assert_text":

                                string label_website = FindElement(driver, type_selector, unique_id).Text; // get the text of the element
                                if (!data.Equals(label_website)) // if the expected text and actual text are not equal
                                {

                                    SetReporte(Status.Fail, high_level_key_word, high_level_key_word_desc); // set the report status to fail
                                    driver.Quit(); // close the browser
                                    throw new Exception($"expected label {data} found: {label_website}"); // throw an exception with an error message

                                };
                                break;
                            default:
                                Console.WriteLine("Invalid keyword: " + keyword); // handle invalid keyword
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message); // print the error message
                    aux = !aux;
                    driver.Quit(); // close the browser
                    SetReporte(Status.Fail, high_level_key_word, high_level_key_word_desc); // set the report status to fail
                }
            }
        }

        public void SetReporte(Status status, string high_level_key_word, string high_level_key_word_desc)
        {
            reporte.status = status; // set the status of the report
            reporte.high_level_key_word = high_level_key_word; // set the high level keyword of the report
            reporte.High_level_key_word_desc = high_level_key_word_desc; // set the high level keyword description of the report
            Final_report.Add(reporte); // add the report to the list of final reports
        }

        public IWebElement FindElement(IWebDriver driver, string typeSelector, string unique_id)
        {
            IWebElement WebElement = null; // declare a null WebElement object

            switch (typeSelector) // check the type selector and return the corresponding WebElement object
            {
                case "id":
                    {
                        WebElement = driver.FindElement(By.Id(unique_id)); // find element by ID
                        break;
                    }
                case "css":
                    {
                        WebElement = driver.FindElement(By.CssSelector(unique_id)); // find element by CSS selector
                        break;
                    }
                case "name":
                    {
                        WebElement = driver.FindElement(By.Name(unique_id)); // find element by name attribute
                        break;
                    }
                case "class_name":
                    {
                        WebElement = driver.FindElement(By.ClassName(unique_id)); // find element by class name
                        break;
                    }
                case "tag_name":
                    {
                        WebElement = driver.FindElement(By.TagName(unique_id)); // find element by tag name
                        break;
                    }
                case "link_text":
                    {
                        WebElement = driver.FindElement(By.LinkText(unique_id)); // find element by exact link text
                        break;
                    }
                case "partial_text":
                    {
                        WebElement = driver.FindElement(By.PartialLinkText(unique_id)); // find element by partial link text
                        break;
                    }
                case "css_selector":
                    {
                        WebElement = driver.FindElement(By.CssSelector(unique_id)); // find element by CSS selector (again)
                        break;
                    }
                default:
                    WebElement = driver.FindElement(By.XPath(unique_id));
                    break;
            }

            if (WebElement == null)
            {
                throw new Exception("Web element not found.." + unique_id);
            }

            return WebElement; // return the WebElement object


        }
    }
}
