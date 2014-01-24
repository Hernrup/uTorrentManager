using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using log4net;
using System.IO;

namespace UTorrentManager
{
    public class FileManager
    {
        
        static public void CopyFile(string sourceFile, string destFile, ILog log)
        {
            try
            {
                log.Debug("Moving file: " + sourceFile + " to " + destFile);
                File.Copy(sourceFile, destFile, true);
            }
            catch (Exception e)
            {
                log.Debug("Move failed: " + sourceFile + " to " + destFile);
            }
        }

        static public void CopyFolder(string sourceFolder, string destFolder, ILog log)
        {
            try
            {
                log.Debug("Moving folder: " + sourceFolder + " to " + destFolder);

                if (!Directory.Exists(destFolder))
                    Directory.CreateDirectory(destFolder);
                string[] files = Directory.GetFiles(sourceFolder);
                foreach (string file in files)
                {
                    string name = Path.GetFileName(file);
                    string dest = Path.Combine(destFolder, name);
                    File.Copy(file, dest, true);
                }
                string[] folders = Directory.GetDirectories(sourceFolder);
                foreach (string folder in folders)
                {
                    string name = Path.GetFileName(folder);
                    string dest = Path.Combine(destFolder, name);
                    CopyFolder(folder, dest, log);
                }
            }
            catch (Exception e)
            {
                log.Debug("Move failed: " + sourceFolder + " to " + destFolder);
            }
        }

        static public void checkAndCreateFolder(string path){
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }
    }
}
