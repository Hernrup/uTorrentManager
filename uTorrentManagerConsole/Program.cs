using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTorrentManager;

namespace uTorrentManagerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager();
            manager.cleanupProcess();

            Console.ReadLine();
        }
    }
}
