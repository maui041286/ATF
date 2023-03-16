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
    public class LoadCSV : ILoadData
    {
        private List<Reporte> final_report = new List<Reporte>();
        private Reporte reporte;

        public List<Reporte> Final_report { get => final_report; set => final_report = value; }

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
            bool aux = true;
            foreach (string line in lines)
            {
                string[] tokens = line.Split(',');
                string keyword = tokens[0];
                string data = tokens[1];
                string type_selector = tokens[2];
                string unique_id = tokens[3];
                string high_level_key_word_desc = tokens[6];
                string high_level_key_word = tokens[7];

                try
                {
                    if (aux == false && keyword == "open_browser")
                    {
                        aux = !aux;
                    }
                     if (aux)
                    {
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
                                SetReporte(Status.Pass, high_level_key_word, high_level_key_word_desc);
                                driver.Quit(); // close the browser
                                break;
                            case "assert_text":
                                string label_website = FindElement(driver, type_selector, unique_id).Text;
                                if (!data.Equals(label_website))
                                {

                                    SetReporte(Status.Fail, high_level_key_word, high_level_key_word_desc);
                                    driver.Quit();
                                    throw new Exception($"expected label {data} found: {label_website}");

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
                    Console.WriteLine(e.Message);
                    aux = !aux;
                    driver.Quit();
                    SetReporte(Status.Fail,high_level_key_word, high_level_key_word_desc);
                }
            }
        }

        public void SetReporte(Status status, string high_level_key_word, string high_level_key_word_desc)
        {
            reporte.status = status;
            reporte.high_level_key_word = high_level_key_word;
            reporte.High_level_key_word_desc = high_level_key_word_desc;
            Final_report.Add(reporte);
        }
        public IWebElement FindElement(IWebDriver driver, string typeSelector, string unique_id)
        {
            IWebElement elementBrowser = null;

                switch (typeSelector)
                {
                    case "id":
                        {
                        // return driver.FindElement(By.Id(unique_id));
                        elementBrowser = driver.FindElement(By.Id(unique_id));
                        break;
                    }
                    case "css":
                        {
                        //return driver.FindElement(By.CssSelector(unique_id));
                        elementBrowser = driver.FindElement(By.CssSelector(unique_id));
                        break;
                    }
                    case "name": {
                            //return driver.FindElement(By.Name(unique_id));
                            elementBrowser= driver.FindElement(By.Name(unique_id));
                        break;
                    }
                    case "class_name":
                        {
                        //return driver.FindElement(By.ClassName(unique_id));
                        elementBrowser = driver.FindElement(By.ClassName(unique_id));
                        break;
                    }
                    case "tag_name":
                        {
                        // return driver.FindElement(By.TagName(unique_id));
                        elementBrowser = driver.FindElement(By.TagName(unique_id));
                        break;
                    }
                    case "link_text":
                        {
                        //return driver.FindElement(By.LinkText(unique_id));
                        elementBrowser = driver.FindElement(By.LinkText(unique_id));
                        break;
                    }
                    case "partial_text":
                        {
                        //return driver.FindElement(By.PartialLinkText(unique_id));
                        elementBrowser = driver.FindElement(By.PartialLinkText(unique_id));
                        break;
                    }
                    case "css_selector":
                        {
                            //return driver.FindElement(By.CssSelector(unique_id));
                            elementBrowser = driver.FindElement(By.CssSelector(unique_id));
                        break;
                    }

                    default:
                        //return driver.FindElement(By.XPath(unique_id));
                        elementBrowser = driver.FindElement(By.XPath(unique_id));
                        break;
            }
            if (elementBrowser == null)
            {
                throw new Exception($"Web element not found type_selector {typeSelector}, unique id : {unique_id}" );
            }
            return elementBrowser;
        }
    }
}
