using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;



namespace SerialTerminal
{
    /// <summary>
    /// Logger class provides basic functionality for data logging into a text file. Data written is flushed after specified time of logging inactivity (InactivityPeriod) or periodically during long lasting data streams (ActivityPeriod).
    /// </summary>
    class Logger : IDisposable
    {        
        #region Privates


        private StreamWriter LogFile;

        private System.Timers.Timer InactivityFlusher;
        private System.Timers.Timer StreamFlusher;


        private int _InactivityPeriodMs = 10000;
        private int _ActivityPeriodMs = 100000;

        private bool _Enabled = false;
        private string _LogFilePath = "";


        #endregion Privates



        #region Properties
        /// <summary>
        /// Gets or sets time in milliseconds after last write whend buffer should be flushed.
        /// </summary>
        public int InactivityPeriodMs
        {
            get
            {
                return _InactivityPeriodMs;
            }
            set
            {
                try
                {
                    _InactivityPeriodMs = value;
                    InactivityFlusher.Interval = _InactivityPeriodMs;
                }
                catch
                {
                }
            }
        }

        
        /// <summary>
        /// Gets or sets time of continuous writing in milliseconds after which buffer should be flushed.
        /// </summary>
        public int ActivityPeriodMs
        {
            get
            {
                return _ActivityPeriodMs;
            }
            set
            {
                try
                {
                    _ActivityPeriodMs = value;
                    StreamFlusher.Interval = _InactivityPeriodMs;
                }
                catch
                {
                }
            }
        }


        /// <summary>
        /// Gets path to log file.
        /// </summary>
        public string FilePath
        {
            get
            {
                return _LogFilePath;
            }
            private set
            {
                _LogFilePath = value;
            }
        }


        /// <summary>
        /// Gets current status of logger.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _Enabled;
            }
            private set
            {
                _Enabled = value;
            }
        }

        #endregion Properties



        #region Constructor

        public enum OpenMode
        {
            Ask,
            Append,
            OverWrite,
        }

        /// <summary>
        /// Constructor of this class tries to initialize instances of flush timers and their elapsed events.
        /// </summary>
        public Logger()
        {


            InactivityFlusher = new System.Timers.Timer(InactivityPeriodMs);
            InactivityFlusher.AutoReset = false;
            InactivityFlusher.Elapsed += (s, e) =>
            {
                if (Enabled)
                {
                    try
                    {
                        //flush log file after some time of inactivity, also stop stream flush counter
                        LogFile.Flush();
                        StreamFlusher.Stop();
                    }
                    catch
                    {
                    }
                }
            };


            StreamFlusher = new System.Timers.Timer(ActivityPeriodMs);
            StreamFlusher.AutoReset = false;
            StreamFlusher.Elapsed += (s, e) =>
            {
                if (Enabled)
                {
                    try
                    {
                        //flush log file after long long lasting continuous reception
                        LogFile.Flush();
                        InactivityFlusher.Stop();
                    }
                    catch
                    {
                    }
                }
            };
        }



        // Flag: Has Dispose already been called? 
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here. 
                //
            }

            // Free any unmanaged objects here. 
            //
            disposed = true;
        }






        #endregion Constructor



        #region PublicMethods


        /// <summary>
        /// Opens file dialog to select file for writing and also opens selected file. Throws exception if file could not be openned.
        /// </summary>
        /// <param name="m">Member of Logger.OpenMode enum that specifies if data should be appended to file or overwrite previous data or if user should be asked.</param>
        /// <param name="pathToFile">Path to file. If its null browse dialog is openned.</param>
        public void Open(OpenMode m, string pathToFile)
        {
            try
            {
                //check if path to file has been specified in params and possibly open browse file dialog
                if (pathToFile != null)
                {
                    FilePath = pathToFile;
                }
                else
                {
                    SelectFile();
                }

                //If Filepath is still null user has closed browse dialog
                if (FilePath == null)
                {
                    return;
                }

                //open log file with specified mode
                if (m == OpenMode.Append)
                {
                    LogFile = new StreamWriter(FilePath, true);
                }
                else if (m == OpenMode.OverWrite)
                {
                    LogFile = new StreamWriter(FilePath, false);
                }
                else if(m == OpenMode.Ask)
                {
                    if (!IsFileEmpty())
                    {
                        DialogResult Result = MessageBox.Show("Selected file is not empty. Do you want to overwrite its content?\r\n(No will append text to existing.)", "Confirm overwrite", MessageBoxButtons.YesNoCancel);
                        if (Result == DialogResult.Yes)
                        {
                            LogFile = new StreamWriter(FilePath, false);
                        }
                        else if (Result == DialogResult.No)
                        {
                            LogFile = new StreamWriter(FilePath, true);
                        }
                        else
                        {
                            Enabled = false;
                            return;
                        }
                    }
                    else
                    {
                        LogFile = new StreamWriter(FilePath, true);
                    }
                }

                //set enabled flag
                Enabled = true;
            }
            catch
            {
                //if exception is thrown during log openning reset enable flag, stop flush timers and throw exception
                Enabled = false;
                throw new ApplicationException("Could not open file.");
            }
        }



        /// <summary>
        /// Closes file and flushes buffers. Throws exception if file could not be closed.
        /// </summary>
        public void Close()
        {
            //stop flush timers
            StreamFlusher.Stop();
            InactivityFlusher.Stop();

            //If filepath is null user has canceled browse dialog
            if (FilePath == null)
            {
                return;
            }

            try
            {
                //try to close log file and reset enabled flag
                LogFile.Flush();
                LogFile.Close();
                _Enabled = false;
            }
            catch
            {
                //if not possible throw exception and keep enabled flag set
                _Enabled = true;
                throw new ApplicationException("File could not be closed.");
            }
        }



        /// <summary>
        /// Writes data into log file. Exception is thrown if not possible.
        /// </summary>
        /// <param name="text">Text to write into file.</param>
        public void Write(string text)
        {
            //If filepath is null user has canceled browse dialog
            if (FilePath == null)
            {
                return;
            }

            try
            {
                //write data to log file
                LogFile.Write(text);            

                //restart inactivity flush timer (flushes data some time after last write)
                InactivityFlusher.Stop();
                InactivityFlusher.Start();

                //start stream flusher (flushes data after specified activity)
                if (!StreamFlusher.Enabled)
                {
                    StreamFlusher.Start();
                } 
            }
            catch
            {
                throw new ApplicationException("Could not write to file");
            }
        }



        /// <summary>
        /// Flushes content of buffers into file.
        /// </summary>
        public void Flush()
        {
            try
            {
                LogFile.Flush();
            }
            catch
            {
                throw new ApplicationException("Could not flush buffers to file.");
            }
        }



        /// <summary>
        /// Opens file dialog to select log file.
        /// </summary>
        private void SelectFile()
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.RestoreDirectory = true;
            FilePath = null;
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = FileDialog.FileName;
            }
        }



        /// <summary>
        /// Gets name of specified log file. Exception is thrown if not possible.
        /// </summary>
        private string GetFileName()
        {
            try
            {
                string[] Tokens = FilePath.Split('\\');
                return Tokens[Tokens.Length - 1];    //last token contains name and extension
            }
            catch
            {
                return null;
            }
        }

        private bool IsFileEmpty()
        {
            if (new FileInfo(FilePath).Length != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion PublicMethods
    }
}
