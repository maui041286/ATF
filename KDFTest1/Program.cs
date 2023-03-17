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

            // print the test report to the console
            loadCSV.Final_report.ForEach(report =>
            {
                Console.WriteLine($"High Level Keyword Description: {report.high_level_key_word} High Level Keyword:{report.High_level_key_word_desc} Status: {report.status}");
            });

            Console.WriteLine("Application Ended...");
            Console.ReadLine();
        }
    }
}
