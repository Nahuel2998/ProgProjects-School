using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ModSelector.classes;

namespace ModSelector
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] paths = {
                @"TheList.txt", // TheList Path
                @"unusedMods", // Unused Mods Folder
                @"mods"  // Used Mods Folder
            };
            ref string theListPath = ref paths[0];
            ref string unusedModsFolder = ref paths[1];
            ref string usedModsFolder = ref paths[2];
            
            for (int i = 0; i < args.Length; i++)
            { paths[i] = args[i]; }
            
            Dictionary<string, Mod> mods = new();
            HashSet<Mod>[] usedCategories = new HashSet<Mod>[10];
            for (int i = 0; i < usedCategories.Length; i++)
            { usedCategories[i] = new HashSet<Mod>(); }
            
            // TheList format: ID : ModName : Tag1, Tag2, Tag3
            foreach (string modString in File.ReadAllLines(theListPath))
            {
                string[] modData = modString.Split(":", StringSplitOptions.TrimEntries);
                mods.Add(modData[0],
                    new Mod(modData[0], modData[1], modData[2].Split(",", StringSplitOptions.TrimEntries)));
            }

            // Ensure the number of folders equals the number of mods on TheList
            if (Directory.GetDirectories(unusedModsFolder).Length + Directory.GetDirectories(usedModsFolder).Length !=
                mods.Count)
            { throw new Exception("Mismatching mod counts (folders and TheList lines)."); }

            // Set already enabled mods as enabled
            // Could throw a KeyNotFoundException
            foreach (string dir in Directory.GetDirectories(usedModsFolder).Select(d => new DirectoryInfo(d).Name))
            { ToggleMod(mods[dir], ref usedCategories); }
            
            CheckForConflicts(usedCategories);

            // Debug
            foreach (var mod in mods.Values)
            { Console.WriteLine($"{mod.Id} : {mod.InConflict}"); }

            // TODO: The main menu GUI
            // You enable or disable values with certain keys (numbers)
            // Could be implemented with OrderedDictionary<KeyCode, Mod>
            // If you have two of the same category, warn about conflicts
            // Append a !! after a mod to indicate it's in conflict
            // Show conflicts per category
            
            // TODO: The main functionality
            // Copy the selected mods to the mods folder, delete unselected ones from the folder
            // no
            // Move instead
            // (Maybe compare if they're already there before copying?)
            // (Use a kind of changes list and do the changes on saveOption)

            // TODO: Make a search function
            // Regex
        }

        static void ToggleMod(Mod mod, ref HashSet<Mod>[] usedCategories)
        {
            mod.Enabled = !mod.Enabled;
            foreach (short category in mod.Categories)
            {
                if (mod.Enabled)
                { usedCategories[category].Add(mod); }
                else
                { usedCategories[category].Remove(mod); }
            }
        }

        static void CheckForConflicts(HashSet<Mod>[] usedCategories)
        {
            for (int i = 0; i < usedCategories.Length - 1; i++)
            {
                foreach (Mod mod in usedCategories[i])
                { mod.InConflict = usedCategories[i].Count > 1; }
            }
        }
    }
}
