using System;
using System.Collections.Generic;
using System.Text;

using System.IO;


namespace SystemInfoCollector
{
    /// <summary>
    /// DirSize
    /// </summary>
    class DirSize
    {
        long _number = uint.MinValue;
        public long Number
        {
            get
            {
                return this._number;
            }
            set
            {
                this._number = value;
            }
        }

        public DirSize()
        {
        }

        #region WalkDirectoryTreeReturnSize
        ///[url] 
        ///[http://msdn.microsoft.com/en-us/library/bb513869.aspx]

        /// <summary>
        /// WalkDirectoryTree
        /// <remarks
        /// sets _number = size of directory
        /// </remarks>
        /// <remarks>
        /// SampleUsage
        /// DirSize _dir = new DirSize();
        /// System.IO.DirectoryInfo path =
        /// new System.IO.DirectoryInfo(System.IO.Path.GetTempPath());
        /// _dir.WalkDirectoryTree(path);
        /// Console.WriteLine(Math.Round(
        /// (Convert.ToDouble(_dir.Number) / (1024 * 1024)),3) .ToString() 
        /// + " MB " + "\n")
        /// </remarks>
        /// </summary>
        /// <param name="root"></param>
        public void WalkDirectoryTree(System.IO.DirectoryInfo root)
        {
            long b = 0;

            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            // First, process all the files directly under this folder 
            try
            {
                files = root.GetFiles("*.*");
            }
            // This is thrown if even one of the files requires permissions greater 
            // than the application provides. 
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse. 
                // You may decide to do something different here. For example, you 
                // can try to elevate your privileges and access the file again.
                //log.Add(e.Message);
                //Console.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                // Console.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    // In this example, we only access the existing FileInfo object. If we 
                    // want to open, delete or modify the file, then 
                    // a try-catch block is required here to handle the case 
                    // where the file has been deleted since the call to TraverseTree().
                    //Console.WriteLine(fi.FullName);
                    FileInfo info = new FileInfo(fi.FullName);
                    //Console.WriteLine(root + info.Length.ToString());
                    b += info.Length;
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo);
                }


            }


            this.Number = this.Number + b;

        }

        #endregion

        #region SystemDriveFreeSpace
        /// <summary>
        /// SystemDriveFreeSpace
        /// </summary>
        /// <returns>
        /// Math.Round(percentFreeSpace, 3)
        /// </returns>
        public decimal SystemDriveFreeSpace()
        {
            DriveInfo d = new DriveInfo(Path.GetPathRoot(
            Environment.SystemDirectory));
            //System.Windows.Forms.MessageBox.Show(((long)d.TotalFreeSpace / (long)d.TotalSize)).ToString());
            //(long)d.TotalFreeSpace / (long)d.TotalSize);
            decimal totalSize =
                (decimal)d.TotalSize;
            decimal totalFreeSpace = (decimal)d.TotalFreeSpace;

            decimal percentFreeSpace = (totalFreeSpace / totalSize) * 100;

            return Math.Round(percentFreeSpace, 3);
        }
        #endregion

        #region recurseDeleteADir
        /// <summary>
        /// recurseDeleteADir
        /// will not delete the parent Dir
        /// </summary>
        /// <param name="directory"></param>
        /// <remarks>
        /// SampleUsage
        /// DirSize _dir = new DirSize();
        /// System.IO.DirectoryInfo path =
        /// new System.IO.DirectoryInfo(System.IO.Path.GetTempPath());
        /// _dir.recurseDeleteADir(path);
        /// </remarks>
        public void recurseDeleteADir(System.IO.DirectoryInfo directory)
        {
            foreach (System.IO.FileInfo file in directory.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                ///file in use
                catch (System.IO.IOException)
                {
                    System.Diagnostics.Debug.WriteLine(file.FullName + "\n");

                }
                ///no rights to access the file
                catch (System.UnauthorizedAccessException)
                {
                    System.Diagnostics.Debug.WriteLine(file.FullName + "\n");
                }
            }
            foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories())
            {
                try
                {
                    subDirectory.Delete(true);
                }

                catch (System.UnauthorizedAccessException)
                {
                    ///no rights to access the file
                    System.Diagnostics.Debug.WriteLine(subDirectory.FullName + "\n");
                }
            }
        }

        //public void recurseDeleteADir()
        //{
        //    System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(
        //        @"C:\Users\fRiv0l\AppData\Local\Temp");

        //    directory.Empty();
        //}

        #endregion


    }
}
