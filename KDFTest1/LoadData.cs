using OpenQA.Selenium; // import the Selenium namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDFTest1
{
    public interface ILoadData
    {
        List<Reporte> Final_report { get; set; } // a list to store the final test report
        string[] Read(string path); // a method to read test data from a file and return it as a string array

        void ExecuteTestCases(string[] lines); // a method to execute test cases using the loaded test data
    }

    public struct Reporte
    {
        public string High_level_key_word_desc; // a description of the high-level keyword
        public string high_level_key_word; // the high-level keyword itself
        public Status status; // the status of the test (pass/fail)
    }

    public enum Status
    {
        Pass, // test passed
        Fail // test failed
    }
}