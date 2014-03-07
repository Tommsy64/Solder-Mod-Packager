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
        private static String mainDir = Settings.StorageLocation;

        public static String settingsDir = combine("settings");
        public static String unPackagedDir = combine("unpackaged");
        public static String packagedDir = combine("packaged");

        public static void initialize()
        {
            createDir("");
            createDir(settingsDir);
            createDir(unPackagedDir);
            createDir(combine(unPackagedDir, "config"));
            createDir(combine(unPackagedDir, "mods"));

            createDir(packagedDir);
        }

        public static void createDir(String dir)
        {
            if (!System.IO.Directory.Exists(combine(dir)))
                System.IO.Directory.CreateDirectory(combine(dir));
        }

        public static String combine(String dir, String dir2)
        {
            return Path.Combine(dir, dir2);
        }

        public static String combine(String dir)
        {
            return Path.Combine(mainDir, dir);
        }
    }
}
