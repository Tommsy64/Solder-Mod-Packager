using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolderModPackager
{
    public static class FileSystemUtils
    {
        private static string mainDir = Settings.StorageLocation;

        public static string settingsDir = GetFullPath("settings");
        public static string unPackagedDir = GetFullPath("unpackaged");
        public static string packagedDir = GetFullPath("packaged");
        public static string tmpDir = GetFullPath(@"settings\tmp");
        public static string dataDir = GetFullPath(@"settings\data");

        public static void Initialize()
        {
            CreateDir(String.Empty);
            CreateDir(settingsDir);
            CreateDir(dataDir);
            CreateDir(unPackagedDir);
            CreateDir(Path.Combine(unPackagedDir, "config"));
            CreateDir(Path.Combine(unPackagedDir, "mods"));

            CreateDir(packagedDir);
        }

        public static void CreateDir(string dir)
        {
            if (!System.IO.Directory.Exists(GetFullPath(dir)))
                System.IO.Directory.CreateDirectory(GetFullPath(dir));
        }

        public static string GetFullPath(string dir)
        {
            return Path.Combine(mainDir, dir);
        }

        public static void Delete(string item)
        {
            if (Directory.Exists(item))
                Directory.Delete(item, true);

            if (File.Exists(item))
                File.Delete(item);
        }

        public static String GetTmpDir()
        {
            string dir = Path.Combine(tmpDir, Guid.NewGuid().ToString().Normalize());
            CreateDir(dir);
            return dir;
        }
    }
}
