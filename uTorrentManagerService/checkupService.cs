using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using UTorrentManager;

namespace uTorrentManagerService
{
    public partial class checkupService : ServiceBase
    {
        public Manager manager;

        public checkupService()
        {
            InitializeComponent();
            this.manager = new Manager();
            
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            this.manager.start();

        }

        protected override void OnStop()
        {
            base.OnStop();
            this.manager.stop();
        }
    }
}
