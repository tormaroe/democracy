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
                return ConfigurationManager.AppSettings["connection"];
            }
        }

        public static string Db
        {
            get
            {
                return ConfigurationManager.AppSettings["db"];
            }
        }

    }
}