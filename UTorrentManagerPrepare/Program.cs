using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using log4net;
using UTorrentManager;

namespace UTorrentManagerPrepare
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new UTorrentManager.Manager();
            manager.uTorrentCopyRequest(args[0], args[1], args[2]);
            //manager.uTorrentTestRequest(args[0], args[1], args[2]);
        }  
    }
}
