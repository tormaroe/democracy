using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;

namespace democracy.DB
{
    public static class Database
    {
        public static MongoDatabase Instance
        {
            get
            {
                Console.WriteLine(Settings.Connection);
                Console.WriteLine(Settings.Db);
                return new MongoClient(Settings.Connection).GetServer().GetDatabase(Settings.Db);
            }
        }
    }
}
