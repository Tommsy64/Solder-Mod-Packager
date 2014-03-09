using System.IO;

namespace SolderModPackager
{
    class ModFolder
    {
        private string name;
        private string folderPath;

        public ModFolder(string name)
        {
            this.name = name;
            this.folderPath = Path.Combine(FileSystemUtils.packagedDir, name);
        }

        public ModFile[] getContents()
        {
            string[] modStringFiles = Directory.GetFiles(this.folderPath);
            ModFile[] modFiles = new ModFile[modStringFiles.Length];

            for (int i = 0; i < modStringFiles.Length; i++)
            {
                string modFileName = new FileInfo(modStringFiles[i]).Name.ToLower();
                string modFolderName = new DirectoryInfo(this.folderPath).Name.ToLower();
                modFiles[i] = new ModFile(modFolderName, modFileName.Replace(modFolderName, string.Empty));
            }
            return modFiles;
        }

        public string getFolderPath()
        {
            return this.folderPath;
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
