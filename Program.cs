using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dotployed
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string source;
            string target;

            if (args.Length == 2)
            {
                source = args[0];
                target = args[1];
            }

            string[] types = { ".dll", ".exe", ".pdb" };

            IEnumerable<string> files = Directory.GetFiles(target, "*.*", SearchOption.AllDirectories).Where(f => types.Any(t => f.EndsWith(t)));

            DirectoryInfo dir = new DirectoryInfo(source);

            foreach (string file in files)
            {
                string assembly = Path.GetFileName(file);

                IEnumerable<FileInfo> fileList = dir.GetFiles("*" + assembly, SearchOption.AllDirectories);

                if (fileList.Any())
                {
                    FileInfo targetInfo = new FileInfo(file);

                    fileList = fileList.Where(f => f.CreationTime < targetInfo.CreationTime);

                    if (fileList.Any())
                    {
                        string newestFile = fileList.Last().FullName;

                        if (File.Exists(file))
                        {
                            File.Delete(file);
                        }

                        File.Copy(newestFile, file, true);

                        File.SetCreationTime(file, DateTime.Now);
                        File.SetLastAccessTime(file, DateTime.Now);
                        File.SetLastWriteTime(file, DateTime.Now);

                        Console.WriteLine(assembly);
                    }
                }
                else
                {
                    Console.WriteLine("Couldn't find: " + assembly + " on the source machine.");
                }
            }

            Console.WriteLine("Done.");
        }
    }
}