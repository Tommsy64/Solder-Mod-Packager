using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolderModPackager
{
    static class Unpackager
    {
        public static void Unpackage(bool chooseLatest = false)
        {
            ModFolder[] modFolders = toModFolder(Directory.GetDirectories(FileSystemUtils.packagedDir));

            foreach (ModFolder modFolder in modFolders)
            {
                if (modFolder.getContents().Length == 0)
                {
                    Console.WriteLine(modFolder + " was skipped because it was empty");
                    continue;
                }

                ModFile modFile = chooseModFile(modFolder, chooseLatest);
                modFile.Save();
                modFile.Unpack();
            }
        }

        private static ModFile chooseModFile(ModFolder modFolder, bool chooseLatest)
        {
            if (modFolder.getContents().Length == 1)
                return modFolder.getContents()[0];
            else
            {
                Console.WriteLine("There are " + modFolder.getContents().Length + " versions of " + modFolder.ToString());
                if (chooseLatest)
                    return modFolder.getContents()[0];
                else
                    return chooseModVersion(modFolder);
            }
        }

        // Asks the user for a file until the user inputs a correct one.
        private static ModFile chooseModVersion(ModFolder modFolder)
        {
            while (true)
            {
                Console.WriteLine("Choose one: ");
                foreach (ModFile modVersion in modFolder.getContents()) { Console.WriteLine(modVersion.ToString()); }

                string input = Console.ReadLine();
                foreach (ModFile modVersion in modFolder.getContents())
                {
                    if (modVersion.ToString() == input)
                    {
                        return new ModFile(modFolder.ToString(), input.Replace(new DirectoryInfo(modFolder.getFolderPath()).Name, string.Empty));
                    }
                }
            }
        }

        private static ModFolder[] toModFolder(string[] modStringFolders)
        {
            ModFolder[] modFolders = new ModFolder[modStringFolders.Length];
            for (int i = 0; i < modStringFolders.Length; i++)
            {
                modFolders[i] = new ModFolder(modStringFolders[i]);
            }
            return modFolders;
        }
    }
}
