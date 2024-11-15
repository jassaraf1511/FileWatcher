using System;
using System.IO;
using System.IO.Compression;
using CFSB.LoggerWriter.Service;

namespace CFSB.DataFileManagement.Services
{
    public class DataFileCompression
    {
        readonly string zipFileExtension;
        readonly string gzFileExtension;
        LogWriter loggerInstance;
        public DataFileCompression()
        {
            zipFileExtension = ".zip";
            gzFileExtension = ".gz";
            loggerInstance = LogWriter.Instance;
        }


        /// <summary>
        /// Compress File in GZ Format
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="tragetDirectory"></param>
        /// <returns></returns>
        public string compressFile(string sourceFile, string tragetDirectory)
        {
            loggerInstance.WriteToLog("Source File sourceFile " + sourceFile);
            try
            {
                string fileZip = getZipFileName(sourceFile, tragetDirectory);
                loggerInstance.WriteToLog("zip file " + fileZip);
                string entryName = Path.GetFileName(sourceFile);

                using (var zip = ZipFile.Open(fileZip, ZipArchiveMode.Create))
                    zip.CreateEntryFromFile(sourceFile, entryName);
                

                return fileZip;
            }
            catch (Exception ex)
            {
                loggerInstance.WriteToLog(ex);
                return null;
            }
        }


        private string getZipFileName(string sourceFile, string targetDirectory)
        {
            string targetZipPath = targetDirectory == null ? Path.GetFullPath(sourceFile) : targetDirectory;
            return (targetZipPath + @"\" + Path.GetFileNameWithoutExtension(sourceFile) + zipFileExtension);
        }
        /// <summary>
        /// create gz file should be static
        /// </summary>
        /// <param name="fi"></param>
        public string gzFileCompress(FileInfo fi)
        {
            string zipFileName = "";
            try
            {
                // Get the stream of the source file.
                using (FileStream inFile = fi.OpenRead())
            {
                // Prevent compressing hidden and 
                // already compressed files.
                if ((File.GetAttributes(fi.FullName)
                    & FileAttributes.Hidden)
                    != FileAttributes.Hidden & fi.Extension != ".gz")
                {
                    // Create the compressed file.
                    using (FileStream gzFile =
                                File.Create(fi.FullName + ".gz"))
                    {
                        using (GZipStream Compress =
                            new GZipStream(gzFile,
                            CompressionMode.Compress))
                        {
                            // Copy the source file into 
                            // the compression stream.
                            inFile.CopyTo(Compress);

                                zipFileName = gzFile.Name;
                        }
                    }
                }
            }
                return zipFileName;
            }
            catch (Exception ex)
            {
                loggerInstance.WriteToLog(ex);
                return null;
            }
        }

    }
}

