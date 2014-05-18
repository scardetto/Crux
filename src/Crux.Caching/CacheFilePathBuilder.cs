using System;
using System.Configuration;

namespace Crux.Caching
{
    public interface ICacheFilePathBuilder
    {
        string GetPathForKey(string key);
    }

    public class CacheFilePathBuilder : ICacheFilePathBuilder
    {
        private const string BACKUP_PATH_KEY = "Caching.BackupPath";
        private const string DEFAULT_PATH = "~\\backup";

        public virtual string GetPathForKey(string key)
        {
            string backupPath = GetBackupFolder();

            string fullKey = String.Format("{0}.cache", key);

            return ConvertDottedKeyToFilePath(backupPath, fullKey);
        }

        private static string GetBackupFolder()
        {
            string path = ConfigurationManager.AppSettings[BACKUP_PATH_KEY] ?? DEFAULT_PATH;

            if (path.Contains("~\\")) {
                path = path.Replace("~\\", AppDomain.CurrentDomain.BaseDirectory);
            }

            return path;
        }

        private static string ConvertDottedKeyToFilePath(string basePath, string key)
        {
            // Convert dot's to slashes, but not file extension dots == /xx/yy/foo.gif
            int extensionIndex = key.LastIndexOf('.');

            if (extensionIndex >= 0) {
                key = key.Substring(0, extensionIndex).Replace('.', '\\')
                        + key.Substring(extensionIndex, key.Length - extensionIndex);
            }

            return String.Format("{0}\\{1}", basePath, key);
        }
    }
}