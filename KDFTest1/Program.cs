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

            ILoadData loadCSV = new LoadCSV();
            Login loginWebPages = new  Login(loadCSV);
            loginWebPages.LoginWithValidCredentials();
            //loginWebPages.LoginWithInvalidCredentials();
            loadCSV.Final_report.ForEach(report =>
            {
                Console.WriteLine($"High Level Keyword Description: {report.high_level_key_word} High Level Keyword:{report.High_level_key_word_desc} Status: {report.status}");
            });
            Console.WriteLine("Application Ended...");
            Console.ReadLine();
        }
    }
}
