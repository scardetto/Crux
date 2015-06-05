using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Crux.Caching.Logging;

namespace Crux.Caching
{
    public class BackupFileInterceptor : ICacheInterceptor
    {
        private readonly ICacheFilePathBuilder _pathBuilder;
        private readonly ILog _log;

        public BackupFileInterceptor(ICacheFilePathBuilder pathBuilder)
        {
            _pathBuilder = pathBuilder;
            _log = LogProvider.GetCurrentClassLogger();
        }

        public void OnSuccess(string key, object value)
        {
            try {
                var backupFile = GetBackupFile(key);

                EnsurePathExists(backupFile);

                WriteValueToFile(value, backupFile);
            } catch (Exception e) {
                // Since the backup is not required for normal operation, we do 
                // not throw exceptions, just log warnings.
                _log.WarnException("Error backing up caches value", e);
            }
        }

        private void WriteValueToFile(object value, FileInfo backupFile)
        {
            try {
                using (var stream = GetFileStreamForWriting(backupFile)) {
                    SerializeValue(stream, value);
                }
            } catch (Exception e) {
                throw new CachingException(String.Format("Error saving backup file {0}", backupFile.FullName), e);
            }
        }

        private void SerializeValue(Stream stream, object value)
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, value);
            stream.Flush();
        }

        private Stream GetFileStreamForWriting(FileInfo backupFile)
        {
            // Since the cache framework typically supports a tolerable race condition, 
            // open the file with FileShare.None to ensure exclusive access to the file.  
            // If this fails, we can ignore the Exception and assume the competing 
            // thread is already writing the file.
            return new FileStream(backupFile.FullName, FileMode.Create, FileAccess.Write, FileShare.None);
        } 

        private void EnsurePathExists(FileInfo backupFile)
        {
            var directory = backupFile.Directory;
            if (directory == null || directory.Exists) return;

            try {
                directory.Create();
            } catch (Exception e) {
                throw new CachingException(String.Format("Error creating backup directory {0}", directory.FullName), e);
            }
        }

        public object OnError(string key, Exception e)
        {
            var backupFile = GetBackupFile(key);

            return !backupFile.Exists ? null : DeserializeBackup(backupFile);
        }

        private object DeserializeBackup(FileInfo backupFile)
        {
            using (FileStream stream = backupFile.OpenRead()) {
                var formatter = new BinaryFormatter();
                return formatter.Deserialize(stream);
            }
        }

        private FileInfo GetBackupFile(string key)
        {
            var path = _pathBuilder.GetPathForKey(key);

            try {
                return new FileInfo(path);
            } catch (Exception e) {
                throw new CachingException(String.Format("Invalid Path {0}", path), e);
            }
        }
    }
}