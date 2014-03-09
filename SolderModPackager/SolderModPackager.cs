using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolderModPackager
{
    class SolderModPackager
    {
        public static Version version = new Version("1.0.0");

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to ModPackager version " + version.ToString());
            initialize();

            Console.WriteLine("Unpackage, Repackage, CreateConfigLinks");
            if (Console.ReadLine().ToString().ToLower() == "unpackage")
            {
                FileSystemUtils.Delete(FileSystemUtils.unPackagedDir);
                FileSystemUtils.CreateDir(FileSystemUtils.unPackagedDir);
                Unpackager.Unpackage();
            }

            Console.ReadKey();
        }

        private static void repackage()
        {

        }

        private static void initialize()
        {
            FileSystemUtils.Initialize();
        }
    }
}
