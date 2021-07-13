using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core
{
    public class BaseService
    {
        public static DataConnection GetDBConnection(string conStr="default")
        {
            var conn = new DataConnection(conStr);
            //conn.OnClosed += new EventHandler((o, e) => { });
            DB = conn;
            return conn;
        }
        public static DataConnection DB { get; private set; }
    }
}
