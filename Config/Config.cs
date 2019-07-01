using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLMaster.Config
{
    static class Config
    {
        public const string DIR_PATH = @"C:\Users\Public\XMLMaster";
        public const string SERVER_NAME = @"xxxxxxx";
        public const string DB_NAME = @"xxxxx";
        public const string USR_NAME = "xxxxx";
        public const string PASSWORD = "xxxxx";
        public const string SCHEMA = "xxxxxx";
        public const string COMPANY_ID = "xxxx";
        public const string INPUT_PATH = @"input\Input_applications.csv";
        public const string SCHEMA_PATH = "xxxx.xsd";
        public const string XSLT_PATH = @"xslt\xxxxx.xslt";
        public static readonly int[] HEADER = new int[] { 088, 077, 076, 032, 077, 065, 083, 084, 069, 082, 013, 010, 087, 114, 105, 116, 101, 114, 058, 032, 068, 097, 110, 110, 121, 032, 089, 117,013,010 };
    }
}
