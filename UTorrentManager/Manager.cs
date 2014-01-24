using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTorrentAPI;
using log4net;
using System.Configuration;
using System.Timers;
using System.IO;

namespace UTorrentManager
{
    public class Manager
    {
        Uri webApiUri;
        string username;
        string password;
        int interval;
        UTorrentClient client;
        ILog log;
        Timer timer;

        public Manager()
        {
            log4net.Config.XmlConfigurator.Configure();
            this.log = LogManager.GetLogger(typeof(Manager));

            this.log.Info("Loading...");

            this.webApiUri = new Uri(ConfigurationManager.AppSettings["uri"]);
            this.username = ConfigurationManager.AppSettings["username"];
            this.password = ConfigurationManager.AppSettings["password"];
            this.interval = 1000 * 60 * Convert.ToInt32(ConfigurationManager.AppSettings["interval"]);

            this.log.Info("Uri: " + this.webApiUri.ToString());
            this.log.Info("Username: " + this.username);
            this.log.Info("Password: " + this.password);
            this.log.Info("Interval: " + this.interval);

            this.client = new UTorrentClient(this.webApiUri, this.username, this.password);

            timer = new Timer();
            timer.Interval = 5000;
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(onTimer);
            timer.Enabled = false;

            this.log.Info("Loaded");

        }

        public void start()
        {
            timer.Start();
            timer.Enabled = true;
        }

        public void stop()
        {
            timer.Stop();
            timer.Enabled = false;
        }

        private void onTimer(object source, ElapsedEventArgs e)
        {
            this.log.Info("Timer triggered at: " + e.SignalTime);

            this.cleanupProcess();

            this.timer.Interval += this.interval;
            this.timer.Enabled = true;
            this.log.Info("Sleeping");
        }

        public void cleanupProcess()
        {

            this.log.Info("Check started");
            this.log.Info("Number of torrents init: " + client.Torrents.Count.ToString());

            Boolean remove = false;

            foreach (Torrent t in client.Torrents)
            {
                this.log.Debug("Checking " + t.Name + ". Status: " + t.Status);

                remove = (
                    (t.Status.HasFlag(TorrentStatus.FinishedOrStopped) && t.RatioInMils > 1500)  
                    || t.Status.HasFlag(TorrentStatus.Error)
                    || t.Status == TorrentStatus.Stopped
                    ) ;
                       
                if (remove)
                {
                    this.log.Info("Removing: " + t.Name);
                    this.client.Torrents.Remove(t, TorrentRemovalOptions.TorrentFileAndData);
                }
            }

            this.log.Info("Check ended");

        }

        public void uTorrentCopyRequest(string dir, string file, string kind)
        {

            this.log.Info("----uTorrentCopyRequest triggered----");

            string targetPath;
            string destFile;
            string sourceFile;
            Boolean isMulti;

            isMulti = (kind == "multi");

            targetPath = ConfigurationManager.AppSettings["targetPath"];

            this.log.Debug("dir: " + dir);
            this.log.Debug("file: " + file);
            this.log.Debug("isMulti: " + isMulti.ToString());
            this.log.Debug("targetPath: " + targetPath);

            FileManager.checkAndCreateFolder(targetPath);

            if (isMulti)
            {
                targetPath = System.IO.Path.Combine(targetPath, new DirectoryInfo(dir).Name);
                FileManager.CopyFolder(dir, targetPath, this.log);
            }
            else
            {
                sourceFile = System.IO.Path.Combine(dir, file);
                destFile = System.IO.Path.Combine(targetPath, file);
                FileManager.CopyFile(sourceFile, destFile, this.log);
            }

            this.log.Info("----End of uTorrentCopyRequest----");
        }

        public void uTorrentTestRequest(string dir, string file, string kind)
        {
            this.log.Info("----uTorrentTestRequest triggered----");

            string targetPath;
            string destFile;
            string sourceFile;
            Boolean isMulti;

            isMulti = (kind == "multi");

            targetPath = ConfigurationManager.AppSettings["targetPath"];

            this.log.Debug("dir: " + dir);
            this.log.Debug("file: " + file);
            this.log.Debug("isMulti: " + isMulti.ToString());
            this.log.Debug("targetPath: " + targetPath);

            this.log.Info("----End of uTorrentTestRequest----");
        }
        
    }
}
