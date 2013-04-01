using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace SystemInfoCollector
{
    /// <summary>
    /// DirSize
    /// 
    /// Usage
    /// 
    /// Instantiate the class
    /// Call the function WalkDirectoryTree with 
    /// full Directory Path as parameter
    /// get the property _number to see the size
    /// </summary>
    class DirSize
    {
        long _number;
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

        //from http://msdn.microsoft.com/en-us/library/bb513869.aspx

        /// <summary>
        /// recursive function to 
        /// WalkDirectoryTree
        /// sets totalDirSize to property _number
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

        /// <summary>
        /// SystemDriveFreeSpace
        /// </summary>
        /// <returns></returns>
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


    }
}
