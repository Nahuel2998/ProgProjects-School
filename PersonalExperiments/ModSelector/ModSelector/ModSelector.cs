using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ModSelector.classes;

namespace ModSelector
{
    public sealed class ModSelector
    {
        public const char SortById = '1';
        public const char SortByName = '2';
        public const char SortByCategory = '3';
        public const char SortByEnabled = '4';
        
        public OrderedDictionary Mods = new();
        public readonly HashSet<Mod>[] UsedCategories = new HashSet<Mod>[Mod.GetAmountOfTotalCategories() - 2];
        public readonly HashSet<Mod> Changed = new();

        public static readonly ModSelector Instance = new();

        private ModSelector()
        {
            for (int i = 0; i < UsedCategories.Length; i++)
            { UsedCategories[i] = new HashSet<Mod>(); }
        }

        public void InitMods(string theListPath, string unusedModsFolder, string usedModsFolder)
        {
            if (Mods.Count > 0)
            {
                Mods.Clear();
                ClearUsedCategories();
                ClearChanged();
            }

            try
            {
                // TheList format: ID : ModName : Tag1, Tag2, Tag3
                string[] modStrings = File.ReadAllLines(theListPath);
                IList<string[]> splitModStrings =
                    modStrings.Select(m => m.Split(':', StringSplitOptions.TrimEntries)).ToList();
                int longestName = splitModStrings.Max(m=> m[1].Length);
                foreach (string[] modData in splitModStrings)
                {
                    Mods.Add(modData[0],
                        new Mod(modData[0], modData[1].PadRight(longestName), modData[2].Split(",", StringSplitOptions.TrimEntries)));
                }

                // Ensure the number of folders equals the number of mods on TheList
                if (Directory.GetDirectories(unusedModsFolder).Length +
                    Directory.GetDirectories(usedModsFolder).Length !=
                    Mods.Count)
                { throw new Exception("Mismatching mod counts (folders and TheList lines)."); }
            }
            catch (IOException)
            { throw new IOException("what where"); }
            
            // Set already enabled mods as enabled
            // Could throw a KeyNotFoundException
            foreach (string dir in Directory.GetDirectories(usedModsFolder).Select(d => new DirectoryInfo(d).Name))
            { ToggleMod(Mods[dir] as Mod, false, false); }
            
            CheckForConflicts();
        }

        public void ToggleMod(int index)
        { ToggleMod(Mods[index: index] as Mod); }
        
        public void ToggleMod(string modId)
        { ToggleMod(Mods[modId] as Mod); }
        
        public void ToggleMod(Mod mod, bool checkForConflicts = true, bool toggleChanged = true)
        {
            short mapCategoryId = Mod.GetCategoryId("Map");
            short otherCategoryId = Mod.GetCategoryId("Other");
            
            mod.Enabled = !mod.Enabled;
            foreach (short category in mod.Categories)
            {
                if (category == mapCategoryId || category == otherCategoryId)
                { continue; }
                
                if (mod.Enabled)
                { UsedCategories[category].Add(mod); }
                else
                {
                    UsedCategories[category].Remove(mod);
                    // mod.InConflict = false;
                    mod.ConflictingCategories.Clear();
                }
            }

            if (toggleChanged)
            { ToggleChanged(mod); }
            if (checkForConflicts)
            { CheckForConflicts(mod); }
        }

        public void ToggleChanged(string modId)
        { ToggleChanged(Mods[modId] as Mod); }
        
        public void ToggleChanged(Mod mod)
        {
            if (!Changed.Add(mod))
            { Changed.Remove(mod); }
        }
        
        public void CheckForConflicts()
        {
            for (short i = 0; i < UsedCategories.Length; i++)
            {
                foreach (Mod mod in UsedCategories[i])
                {
                    if (UsedCategories[i].Count > 1)
                    { mod.ConflictingCategories.Add(i); }
                    else
                    { mod.ConflictingCategories.Remove(i); }
                }
            }
        }

        public void CheckForConflicts(Mod mod)
        {
            foreach (short category in mod.Categories)
            {
                foreach (Mod modInCategory in UsedCategories[category])
                {
                    if (UsedCategories[category].Count > 1)
                    { modInCategory.ConflictingCategories.Add(category); }
                    else
                    { modInCategory.ConflictingCategories.Remove(category); }
                }
            }
        }
        
        public void ClearChanged()
        { Changed.Clear(); }

        private void ClearUsedCategories()
        {
            foreach (HashSet<Mod> category in UsedCategories)
            { category.Clear(); }
        }

        public void DiscardChanges()
        {
            foreach (Mod mod in Changed)
            {
                ToggleMod(mod, false);
                CheckForConflicts();
            }
        }

        public void SaveChanges(string unusedModsFolder, string usedModsFolder)
        {
            foreach (Mod mod in Changed)
            {
                string modPathIfUsed = $@"{usedModsFolder}\{mod.Id}";
                string modPathIfUnused = $@"{unusedModsFolder}\{mod.Id}";
                
                if (mod.Enabled)
                { Directory.Move(modPathIfUnused, modPathIfUsed); }
                else
                { Directory.Move(modPathIfUsed, modPathIfUnused); }
            }
            ClearChanged();
        }

        public string GetUnsavedChangesAsString()
        {
            StringBuilder res = new StringBuilder();
            foreach (var mod in Changed)
            { res.Append($" {mod/*.ToString(namePadding/*, categoriesPadding)*/}\n"); }

            return res.ToString();
        }
        
        // public string GetUnsavedChangesAsString(int namePadding, int categoriesPadding)
        // {
            // StringBuilder res = new StringBuilder();
            // foreach (var mod in Changed)
            // { res.Append($" {mod/*.ToString(namePadding, categoriesPadding)*/}\n"); }

            // return res.ToString();
        // }

        public IEnumerable<Mod> GetModsInCategory(short categoryId) =>
            GetModsInCategory(categoryId, Mods.Values.Cast<Mod>());
        
        public static IEnumerable<Mod> GetModsInCategory(short categoryId, IEnumerable<Mod> mods) =>
            mods.Where(mod => mod.Categories.Contains(categoryId));

        public OrderedDictionary GetModsInCategoryAsOrderedDict(short categoryId)
        { return GetModsInCategoryAsOrderedDict(categoryId, Mods); }

        public static OrderedDictionary GetModsInCategoryAsOrderedDict(short categoryId, OrderedDictionary mods)
        {
            OrderedDictionary res = new OrderedDictionary();
            foreach (Mod mod in GetModsInCategory(categoryId, mods.Values.Cast<Mod>()))
            { res[mod.Id] = mod; }
            
            return res;
        }

        public IEnumerable<Mod> GetMatchingModsWhenSearchingByName(string name) =>
            GetMatchingModsWhenSearchingByName(name, Mods.Values.Cast<Mod>());

        public static IEnumerable<Mod> GetMatchingModsWhenSearchingByName(string name, IEnumerable<Mod> mods) =>
            mods.Where(mod => mod.NameMatches(name));

        public static OrderedDictionary GetMatchingModsWhenSearchingByNameAsOrderedDict(string name,
            OrderedDictionary mods)
        {
            OrderedDictionary res = new OrderedDictionary();
            foreach (Mod mod in GetMatchingModsWhenSearchingByName(name, mods.Values.Cast<Mod>()))
            { res[mod.Id] = mod; }
            
            return res;
        }
        
        public OrderedDictionary GetMatchingModsAsOrderedDict(string regex)
        { return GetMatchingModsAsOrderedDict(regex, Mods); }
        
        public static OrderedDictionary GetMatchingModsAsOrderedDict(string regex, OrderedDictionary mods)
        {
            OrderedDictionary res = new OrderedDictionary();
            foreach (Mod mod in mods.Values.Cast<Mod>().Where(mod => new Regex(regex, RegexOptions.IgnoreCase).Match(mod.ToString()).Success))
            { res[mod.Id] = mod; }
            
            return res;
        }

        public ImmutableHashSet<Mod> GetConflictingMods() => 
            UsedCategories.Where(x => x.Count > 1).SelectMany(y => y).ToImmutableHashSet();

        public void OrderBy(char byWhat)
        { Instance.Mods = OrderBy(byWhat, Instance.Mods) ?? Instance.Mods; }
        
        public static OrderedDictionary OrderBy(char byWhat, OrderedDictionary dict)
        {
            List<Mod> newModsOrder = byWhat switch
            {
                SortById => dict.Values.Cast<Mod>().OrderBy(m => m.Id).ToList(),
                SortByName => dict.Values.Cast<Mod>().OrderBy(m => m.Name).ToList(),
                SortByCategory => dict.Values.Cast<Mod>().OrderBy(m => m.Categories[0]).ToList(),
                SortByEnabled => dict.Values.Cast<Mod>().OrderByDescending(m => m.Enabled).ToList(),
                _ => null
            };
            
            if (newModsOrder == null)
            { return null; }

            OrderedDictionary res = new(Instance.Mods.Count);
            foreach (Mod mod in newModsOrder)
            { res.Add(mod.Id, mod); }

            return res;
        }
    }
}
