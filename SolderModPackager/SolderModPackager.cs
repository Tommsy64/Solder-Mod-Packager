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
                unpackage(false);
            }

            Console.ReadKey();
        }

        private static void repackage()
        {

        }

        private static void unpackage(Boolean chooseLatest)
        {
            String[] modStringFolders = Directory.GetDirectories(FileSystemUtils.packagedDir);
            ModFolder[] modFolders = new ModFolder[modStringFolders.Length];

            for (int i = 0; i < modStringFolders.Length; i++)
            {
                modFolders[i] = new ModFolder(modStringFolders[i]);
            }

            foreach (ModFolder modFolder in modFolders)
            {
                if (modFolder.getContents().Length == 0)
                {
                    Console.WriteLine(modFolder + " was skipped because it was empty");
                    continue;
                }

                ModFile modFile = null;

                if (modFolder.getContents().Length != 1)
                {
                    Console.WriteLine("There are " + modFolder.getContents().Length + " versions of " + modFolder.ToString());

                    if (chooseLatest)
                    {
                        modFile = modFolder.getContents()[0];
                        Console.WriteLine("Choosing " + modFile.ToString());
                    }
                    else
                    {
                        // Ask user for a file until the user inputs a correct one.
                        Boolean choosing = true;
                        while (choosing)
                        {
                            Console.WriteLine("Choose one: ");
                            foreach (ModFile modVersion in modFolder.getContents()) { Console.WriteLine(modVersion.ToString()); }
                            String input = Console.ReadLine();

                            foreach (ModFile modVersion in modFolder.getContents())
                            {
                                if (modVersion.ToString() == input) { choosing = false; break; }
                            }

                            modFile = new ModFile(modFolder.ToString(), input.Replace(modFolder.ToString(), String.Empty));
                        }
                    }
                }

                if (modFolder.getContents().Length == 1)
                    modFile = modFolder.getContents()[0];

                modFile.unpack();
            }
        }

        private static void initialize()
        {
            FileSystemUtils.initialize();
        }
    }
}
