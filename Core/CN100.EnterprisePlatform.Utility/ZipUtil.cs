using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using System.IO;

namespace CN100.EnterprisePlatform.Utility
{
    public class ZipUtil
    {
        #region EventArgs

        public class clsZipSingleFileEventArgs : System.EventArgs
        {
            public string CurrentFile { get; set; }
            public int TotalFileCount { get; set; }
            public int CompletedFileCount { get; set; }

            public clsZipSingleFileEventArgs(string strFile, int intTotalFileCount, int intCompletedFileCount)
            {
                this.CurrentFile = strFile;
                this.TotalFileCount = intTotalFileCount;
                this.CompletedFileCount = intCompletedFileCount;
            }
        }

        public class clsZipProcessingEventArgs : System.EventArgs
        {
            public string[] Files { get; set; }
            public string ZipFile { get; set; }

            public clsZipProcessingEventArgs(string[] strFiles, string strZipFile)
            {
                this.Files = strFiles;
                this.ZipFile = strZipFile;
            }
        }

        public class clsZipProcessedEventArgs : System.EventArgs
        {
            public string[] Files { get; set; }
            public string ZipFile { get; set; }
            public TimeSpan TimeSpan { get; set; }

            public clsZipProcessedEventArgs(string[] strFiles, string strZipFile, TimeSpan ts)
            {
                this.Files = strFiles;
                this.ZipFile = strZipFile;
                this.TimeSpan = ts;
            }
        }

        public class clsExtractZipProcessingEventArgs : System.EventArgs
        {
            public string ZipFile { get; set; }
            public string TargetDirectory { get; set; }
            public string FileFilter { get; set; }

            public clsExtractZipProcessingEventArgs(string strZipFile, string strTargetDirectory, string strFileFilter)
            {
                this.ZipFile = strZipFile;
                this.TargetDirectory = strTargetDirectory;
                this.FileFilter = strFileFilter;
            }
        }

        public class clsExtractZipProcessedEventArgs : System.EventArgs
        {
            public string ZipFile { get; set; }
            public string TargetDirectory { get; set; }
            public string FileFilter { get; set; }
            public TimeSpan TimeSpan { get; set; }

            public clsExtractZipProcessedEventArgs(string strZipFile, string strTargetDirectory, string strFileFilter, TimeSpan ts)
            {
                this.ZipFile = strZipFile;
                this.TargetDirectory = strTargetDirectory;
                this.FileFilter = strFileFilter;
                this.TimeSpan = ts;
            }
        }

        #endregion

        #region Event
        public delegate void ZipSingleFileCompletedEventHandler(object sender, clsZipSingleFileEventArgs e);

        public event ZipSingleFileCompletedEventHandler ZipSingleFileCompleted;

        public delegate void ZipProcessingEventHandler(object sender, clsZipProcessingEventArgs e);

        public event ZipProcessingEventHandler ZipProcessing;

        public delegate void ZipProcessedEventHandler(object sender, clsZipProcessedEventArgs e);

        public event ZipProcessedEventHandler ZipProcessed;

        public delegate void ExtractZipProcessingEventHandler(object sender, clsExtractZipProcessingEventArgs e);

        public event ExtractZipProcessingEventHandler ExtractZipProcessing;

        public delegate void ExtractZipProcessedEventHandler(object sender, clsExtractZipProcessedEventArgs e);

        public event ExtractZipProcessedEventHandler ExtractZipProcessed;

        private void OnZipSingleFileCompleted(object sender, clsZipSingleFileEventArgs e)
        {
            if (ZipSingleFileCompleted != null)
            {
                this.ZipSingleFileCompleted(sender, e);
            }
        }

        private void OnZipProcessing(object sender, clsZipProcessingEventArgs e)
        {
            if (ZipProcessing != null)
            {
                this.ZipProcessing(sender, e);
            }
        }

        private void OnZipProcessed(object sender, clsZipProcessedEventArgs e)
        {
            if (ZipProcessed != null)
            {
                this.ZipProcessed(sender, e);
            }
        }

        private void OnExtractZipProcessing(object sender, clsExtractZipProcessingEventArgs e)
        {
            if (this.ExtractZipProcessing != null)
            {
                this.ExtractZipProcessing(sender, e);
            }
        }

        private void OnExtractZipProcessed(object sender, clsExtractZipProcessedEventArgs e)
        {
            if (this.ExtractZipProcessed != null)
            {
                this.ExtractZipProcessed(sender, e);
            }
        }
        #endregion

        #region Function

        public static bool CreateEmptyZipFile(string strWhereToSave)
        {
            try
            {
                using (FileStream fs = File.Create(strWhereToSave))
                {
                    byte[] byteEmptyZip = new byte[] { 80, 75, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    fs.Write(byteEmptyZip, 0, byteEmptyZip.Length);
                    fs.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                //LogClass.WriteLog("FunCreateEmptyZipFile Exception: " + ex.Message);
                return false;
            }
        }

        public bool ZipFiles(string[] strFiles, string strWhereToSave, bool blnOverwriteZipFile)
        {
            try
            {
                TimeSpan tsBegin = new TimeSpan(DateTime.Now.Ticks);

                if (blnOverwriteZipFile)
                {
                    if (File.Exists(strWhereToSave))
                    {
                        File.Delete(strWhereToSave);
                    }
                }

                ZipFile zf = ZipFile.Create(strWhereToSave);

                OnZipProcessing(null, new clsZipProcessingEventArgs(strFiles, strWhereToSave));

                zf.BeginUpdate();

                foreach (string strFile in strFiles)
                {
                    zf.Add(strFile, Path.GetFileName(strFile));
                }

                zf.CommitUpdate();

                zf.Close();

                TimeSpan tsEnd = new TimeSpan(DateTime.Now.Ticks);

                TimeSpan tsResult = tsEnd.Subtract(tsBegin);

                OnZipProcessed(null, new clsZipProcessedEventArgs(strFiles, strWhereToSave, tsResult));

                return true;
            }
            catch (Exception ex)
            {
                //LogClass.WriteLog("FunAppendZipFiles Exception: " + ex.Message);
                return false;
            }
        }               

        public bool ZipFolder(string strFolder, string strWhereToSave)
        {
            try
            {
                if (!Directory.Exists(strFolder))
                {
                    return false;
                }

                string[] strFiles = Directory.GetFiles(strFolder);

                return ZipFiles(strFiles, strWhereToSave, true);

            }
            catch (Exception ex)
            {
                //LogClass.WriteLog("FunZipFolder Exception: " + ex.Message);
                return false;
            }
        }

        public bool UnZipFiles(string strZipFile, string strTargetDirectory, string strFileFilter)
        {
            try
            {
                TimeSpan tsBegin = new TimeSpan(DateTime.Now.Ticks);

                FastZip fz = new FastZip();

                OnExtractZipProcessing(null, new clsExtractZipProcessingEventArgs(strZipFile, strTargetDirectory, strFileFilter));

                fz.ExtractZip(strZipFile, strTargetDirectory, strFileFilter);

                TimeSpan tsEnd = new TimeSpan(DateTime.Now.Ticks);

                TimeSpan tsResult = tsEnd.Subtract(tsBegin);

                OnExtractZipProcessed(null, new clsExtractZipProcessedEventArgs(strZipFile, strTargetDirectory, strFileFilter, tsResult));

                return true;
            }
            catch (Exception ex)
            {
                //LogClass.WriteLog("FunExtractZipFiles Exception: " + ex.Message);
                return false;
            }
        }

        public static long GetUnzipFileSize(string strZipFile)
        {
            long lngUnzipFileSize = 0;

            try
            {
                ZipFile zf = new ZipFile(strZipFile);

                System.Collections.IEnumerator ie = zf.GetEnumerator();

                while (ie.MoveNext())
                {
                    ZipEntry ze = (ZipEntry)ie.Current;
                    lngUnzipFileSize += ze.Size;
                }

                zf.Close();

                return lngUnzipFileSize;
            }
            catch (Exception ex)
            {
                //LogClass.WriteLog("GetUnzipFileSize Exception: " + ex.Message);
                lngUnzipFileSize = 0;
                return lngUnzipFileSize;
            }
        }

        //public static bool FunZipFiles(string[] strFiles,string strWhereToSave)
        //{
        //    try
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            ZipOutputStream zos = new ZipOutputStream(ms);
        //            byte[] byteWhereToSave;

        //            foreach (string strFile in strFiles)
        //            {
        //                byte[] byteFile;
        //                using (FileStream fs = File.OpenRead(strFile))
        //                {
        //                    byteFile = new byte[fs.Length];
        //                    fs.Read(byteFile, 0, byteFile.Length);
        //                    ZipEntry entry = new ZipEntry(strFile);
        //                    zos.PutNextEntry(entry);
        //                    zos.Write(byteFile, 0, byteFile.Length);
        //                }
        //            }

        //            zos.Finish();
        //            zos.Close();


        //            byteWhereToSave = ms.ToArray();

        //            using (FileStream fs = File.Create(strWhereToSave))
        //            {
        //                fs.Write(byteWhereToSave, 0, byteWhereToSave.Length);
        //                fs.Close();
        //            }
        //            byteWhereToSave = null;
        //            ms.Close();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogClass.WriteLog("FunAppendZipFiles Exception: " + ex.Message);
        //        return false;
        //    }
        //}

        #endregion

    }

}
