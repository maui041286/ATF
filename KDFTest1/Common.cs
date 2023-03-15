using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace KDFTest1
{
    public class Common
    {
        public static string Basepath()
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin","").Replace("\\Debug","");
            FilePath = $"{FilePath}{ConfigurationManager.AppSettings["FilePath"]}";

            return FilePath;
        }

    }
}
