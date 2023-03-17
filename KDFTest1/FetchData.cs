using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDFTest1
{
    public class FetchData
    {

        private ILoadData loadData;
     

        public FetchData(ILoadData loadData)
        {
            this.loadData = loadData;
        }
        public void ExecuteTasks()
        {
            try
            {
                var lines = loadData.Read(Common.Basepath()); // Read the data from a specified file path

                loadData.ExecuteTestCases(lines); // Execute the test cases read from the file
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Catch any exceptions that occur and display the error message
            }
        }
    }
}
