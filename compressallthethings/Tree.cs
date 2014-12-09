using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace compressallthethings
{
    class Tree
    {
        public static void Traverse(string root)
        {
            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(root))
            {
                throw new ArgumentException();
            }
            dirs.Push(root);

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                string[] files = null;
                try
                {
                    files = System.IO.Directory.GetFiles(currentDir);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                // Push the subdirectories onto the stack for traversal. 
                foreach (string str in subDirs) {
                    dirs.Push(str);
                }
                // Perform the required action on each file
                Parallel.ForEach(files, new ParallelOptions{MaxDegreeOfParallelism=20}, file => {
                    System.IO.FileInfo fi = new System.IO.FileInfo(file);
                    switch (fi.Extension)
                    {
                        case ".pdf":
                            Fileutils.CompressPDF(fi.FullName, fi.DirectoryName + fi.Name + "compressed" + fi.Extension, 10);
                            break;
                        case ".jpg":
                            //Fileutils.CompressImage(fi.FullName, fi.DirectoryName + fi.Name + "compressed" + fi.Extension, 10);
                            break;
                    }
                }); 
            }
        }
    }
}
