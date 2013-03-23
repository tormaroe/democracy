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
                var hqPath = ConfigurationManager.AppSettings["MONGOHQ_URL"];
                return hqPath.Substring(0, hqPath.LastIndexOf('/'));
            }
        }

        public static string Db
        {
            get
            {
                var hqPath = ConfigurationManager.AppSettings["MONGOHQ_URL"];
                return hqPath.Substring(hqPath.LastIndexOf('/') +1);
            }
        }


        public static int NumberOfVotesPrUser
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["number_of_votes_pr_user"]);
            }
        }

    }
}