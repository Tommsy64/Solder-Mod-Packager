using Ionic.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolderModPackager
{
    class ModFile
    {
        public string name;
        public string version;
        public string fullName;
        public string location;
        public ConfigFile[] configs;

        public ModFile(string name, string version, params ConfigFile[] configs)
        {
            this.name = name;
            this.fullName = name + version;
            this.version = version.Replace(".zip", String.Empty).Replace(".jar", String.Empty);
            this.location = Path.Combine(FileSystemUtils.packagedDir, this.name + @"\" + this.fullName);
            if (configs.Length != 0)
                this.configs = configs;
            else
                GenerateConfigs();
        }

        public void Unpack()
        {
            Unpack(ExtractExistingFileAction.DoNotOverwrite);
        }

        public void Unpack(ExtractExistingFileAction existingFileAction)
        {
            using (ZipFile modZip = ZipFile.Read(this.location))
            {
                foreach (ZipEntry zipEntry in modZip)
                {
                    zipEntry.Extract(FileSystemUtils.unPackagedDir, existingFileAction);
                }
            }
        }

        private void GenerateConfigs()
        {
            using (ZipFile modZip = ZipFile.Read(this.location))
            {
                string tmpDir = FileSystemUtils.GetTmpDir();

                foreach (ZipEntry zipEntry in modZip)
                {
                    zipEntry.Extract(tmpDir, ExtractExistingFileAction.OverwriteSilently);
                }

                FileSystemUtils.Delete(tmpDir + @"\mods");

                string[] allFiles = Directory.GetFiles(tmpDir, "*.*", SearchOption.AllDirectories);
                ConfigFile[] configs = new ConfigFile[allFiles.Length];

                for (int i = 0; i < allFiles.Length; i++)
                {
                    configs[i] = new ConfigFile(allFiles[i].ToString().Replace(tmpDir, String.Empty));
                }

                FileSystemUtils.Delete(tmpDir);
                this.configs = configs;
            }
        }

        public void Save()
        {
            FileSystemUtils.CreateDir(FileSystemUtils.dataDir);
            File.WriteAllText(Path.Combine(FileSystemUtils.dataDir, this.name + ".json"), JsonConvert.SerializeObject(this, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                }));
        }
    }
}
