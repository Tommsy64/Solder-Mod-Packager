using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolderModPackager
{
    class ModFolder
    {
        private String name;
        private String folderPath;

        public ModFolder(String name)
        {
            this.name = name;
            this.folderPath = FileSystemUtils.combine(FileSystemUtils.packagedDir, name);
        }

        public ModFile[] getContents()
        {
            String[] modStringFiles = Directory.GetFiles(this.folderPath);
            ModFile[] modFiles = new ModFile[modStringFiles.Length];

            for (int i = 0; i < modStringFiles.Length; i++)
            {
                FileInfo modFileInfo = new FileInfo(modStringFiles[i]);
                DirectoryInfo modFolderInfo = new DirectoryInfo(this.folderPath);
                modFiles[i] = new ModFile(modFolderInfo.Name, modFileInfo.Name.Replace(modFolderInfo.Name, String.Empty));
            }
            return modFiles;
        }

        public String getFolderPath()
        {
            return this.folderPath;
        }

        public String ToString()
        {
            return this.name;
        }
    }
}
