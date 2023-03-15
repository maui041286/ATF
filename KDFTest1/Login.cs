﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDFTest1
{
    public class Login
    {

        private ILoadData loadData;
     

        public Login(ILoadData loadData)
        {
            this.loadData = loadData;
        }

        public void LoginWithValidCredentials()
        {
            try
            {
                // var lines = loadData.Read(Environment.GetEnvironmentVariable("PATH_DATA_SOURCE"));
                var lines = loadData.Read(Common.Basepath());

                loadData.ExecuteTestCases(lines);
            }catch(Exception ex) { 
                Console.WriteLine(ex.Message);  
            }
        }


        public void LoginWithInvalidCredentials()
        {
            try
            {
                //var lines = loadData.Read(Environment.GetEnvironmentVariable("PATH_DATA_SOURCE_INVALID_TEST"));
                var lines = loadData.Read(Common.Basepath());

                loadData.ExecuteTestCases(lines);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
    }
}
