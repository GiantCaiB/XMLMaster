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
        public const string SERVER_NAME = @"MELINTTSTSQL61";
        public const string DB_NAME = @"Inteflow";
        public const string USR_NAME = "sa";
        public const string PASSWORD = "5kr5qlB0x!";
        public const string SCHEMA = "INTEFLOW";
        public const string COMPANY_ID = "LFS";
        public const string INPUT_PATH = @"input\Input_applications.csv";
        public const string SCHEMA_PATH = "LFS_Application_Schema.xsd";
        public const string XSLT_PATH = @"xslt\LFS Application Retrieve - No Processing or Bureau.xslt";
        public static readonly int[] HEADER = new int[] { 088, 077, 076, 032, 077, 065, 083, 084, 069, 082, 013, 010, 087, 114, 105, 116, 101, 114, 058, 032, 068, 097, 110, 110, 121, 032, 089, 117,013,010 };
    }
}
