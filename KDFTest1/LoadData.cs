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

        List<Reporte> Final_report { get; set; }
        string[] Read(string path);

        void ExecuteTestCases(string[] lines);
    }

    public struct Reporte
    {
        public string High_level_key_word_desc;
        public string high_level_key_word;
        public Status status;

    }


   public enum Status
    {
      Pass,
      Fail
    }
}
