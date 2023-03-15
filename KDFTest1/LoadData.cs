using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDFTest1
{
    public  interface ILoadData
    {

        string[] Read(string path);

        void ExecuteTestCases(string[] lines);
    }
}
