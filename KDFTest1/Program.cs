using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace KDFTest1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LoadCSV loadCSV = new LoadCSV();
            string[] lines = loadCSV.ReadFile(@"C:\Users\jvergara\Desktop\Quality assurance Processes\automated testing framework\testdata.csv");
            IWebDriver driver = new ChromeDriver(); // create a new instance of ChromeDriver
            loadCSV.ExecuteTestCases(lines,driver);

           
        }
    }
}
