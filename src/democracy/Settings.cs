using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace democracy
{

    public static class Settings
    {
        public static string Connection
        {
            get
            {
                //return ConfigurationManager.AppSettings["connection"];
                var hqPath = ConfigurationManager.AppSettings["MONGOHQ_URL"];
                return hqPath.Substring(0, hqPath.LastIndexOf('/'));
            }
        }

        public static string Db
        {
            get
            {
                //return ConfigurationManager.AppSettings["db"];

                var hqPath = ConfigurationManager.AppSettings["MONGOHQ_URL"];
                return hqPath.Substring(hqPath.LastIndexOf('/') +1);
            }
        }

    }
}