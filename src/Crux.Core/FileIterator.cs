using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Crux.Core
{
    /// <summary>
    /// This class iterates through all files in a 
    /// specified folder including all sub-folders.
    /// </summary>
    public class FileIterator : IEnumerator<FileInfo>, IEnumerable<FileInfo>
    {
        private readonly DirectoryInfo _startDirectory;
        private readonly Stack<FileInfo> _fileStack;
        private readonly Stack<DirectoryInfo> _dirStack;

        private FileInfo _currentFile;

        public FileIterator(string path) : this(new DirectoryInfo(path)) { }

        public FileIterator(DirectoryInfo info)
        {
            if (!info.Exists) {
                throw new Exception("Directory does not exist.");
            }

            _startDirectory = info;
            _fileStack = new Stack<FileInfo>();
            _dirStack = new Stack<DirectoryInfo>();

            Init();
        }

        public FileInfo Current
        {
            get { return _currentFile; }
        }

        object IEnumerator.Current
        {
            get { return _currentFile; }
        }

        public bool MoveNext()
        {
            if (_fileStack.Count == 0) {
                // Attempt to load files from known dirs
                if (!SearchForFiles(_dirStack.Pop())) {
                    // We are done
                    _currentFile = null;
                    return false;
                }
            }

            // There are files left to process
            _currentFile = _fileStack.Pop();
            return true;
        }

        public void Reset()
        {
            Init();
        }

        public void Dispose()
        {
            // Not implemented
        }

        private void Init()
        {
            _fileStack.Clear();
            _dirStack.Clear();

            _dirStack.Push(_startDirectory);
            LoadFiles(_startDirectory);
        }

        private bool SearchForFiles(DirectoryInfo dir)
        {
            bool found = false;

            // load dirs onto stack
            LoadDirs(dir);

            if (_dirStack.Count != 0) {
                found = LoadFiles(_dirStack.Peek());

                while (!found) {
                    found = SearchForFiles(_dirStack.Pop());
                }
            }

            return found;
        }

        private bool LoadFiles(DirectoryInfo dir)
        {
            FileInfo[] fileList = dir.GetFiles();

            foreach (FileInfo info in fileList) {
                _fileStack.Push(info);
            }

            return (fileList.Length != 0);
        }

        private void LoadDirs(DirectoryInfo dir)
        {
            foreach (DirectoryInfo info in dir.GetDirectories()) {
                _dirStack.Push(info);
            }
        }

        #region IEnumerable Members
        public IEnumerator<FileInfo> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
        #endregion
    }
}
