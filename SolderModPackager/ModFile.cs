using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolderModPackager
{
    class ModFile
    {
        public String name;
        public String version;
        public String fullName;
        public String filePath;

        public ModFile(String name, String version)
        {
            this.name = name;
            this.version = version;
            this.fullName = name + version;
            this.filePath = FileSystemUtils.combine(FileSystemUtils.packagedDir, this.name + @"\" + this.fullName);
        }

        public ModFile(String name, String version, String filePath)
        {
            this.name = name;
            this.version = version;
            this.fullName = name + "-" + version;
            this.filePath = filePath;
        }

        public void unpack()
        {
            unpackage(ExtractExistingFileAction.DoNotOverwrite);
        }

        public void unpackage(ExtractExistingFileAction existingFileAction)
        {
            using (ZipFile modZip = ZipFile.Read(this.filePath))
            {
                foreach (ZipEntry zipEntry in modZip)
                {
                    zipEntry.Extract(FileSystemUtils.unPackagedDir, existingFileAction);
                }
            }
        }
    }
}
