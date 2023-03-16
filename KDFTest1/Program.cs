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
            Console.WriteLine("Application Started...");

            //Load datasource as Interface,
            //So it can be changed easily into another datasource
            ILoadData loadCSV = new LoadCSV();
            
            //FetchData for Keyword Driven Framework Web application Tests
            FetchData WebAppTests = new  FetchData(loadCSV);
            
            //Execute Low Level keywords in Data source
            WebAppTests.ExecuteTasks();

            Console.WriteLine("Application Ended...");
            Console.ReadLine();
        }
    }
}
