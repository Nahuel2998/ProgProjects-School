using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using ModSelector.classes;

namespace ModSelector
{
    public sealed class UiHandler
    {
        private readonly List<Mod> _slots = new();
        private short _currentPage = 1; // min = 1
        // PageSize?
        private bool _canGoNext;
        public string UnusedModsFolder { get; set; }
        public string UsedModsFolder { get; set; }
        private OrderedDictionary _source = ModSelector.Instance.Mods;
        private string _currentSearch = "";
        private string _currentCategory = "";
        public readonly SoundPlayer TrackDosPlayer = new(Assembly.GetExecutingAssembly().GetManifestResourceStream("ModSelector.Track_dos.wav"));
        public readonly SoundPlayer PureFuriasPlayer = new(Assembly.GetExecutingAssembly().GetManifestResourceStream("ModSelector.Pure_furias.wav"));
        
        public static readonly UiHandler Instance = new();

        private UiHandler()
        { }

        public void BuildMenu()
        {
            while (true)
            {
                MainMenu();
                
                char key = Console.ReadKey().KeyChar;
                if (Char.IsDigit(key))
                {
                    try
                    { ModSelector.Instance.ToggleMod(_slots[(key - 49) % 10]); }
                    catch (ArgumentOutOfRangeException)
                    { }
                    continue;
                }

                switch (key)
                {
                    case 'h':
                        PreviousPage();
                        break;
                    case 'l':
                        NextPage();
                        break;
                    case 's':
                        Search();
                        break;
                    case 'c':
                        Category();
                        break;
                    case 'd':
                        ModSelector.Instance.DiscardChanges();
                        break;
                    case 'w':
                        SaveChanges();
                        break;
                    case 'q':
                        if (ShouldExit())
                        { return; }
                        break;
                }
            }
        }

        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine($"[Mod Selector]: {_currentSearch}");
            Console.WriteLine("- - - - - - - -");
            BuildCurrentPage();
            Console.WriteLine(GetCurrentPageInfo());
            Console.Write($"{(_currentPage > 1 ? "h> Previous Page\n" : "")}{(_canGoNext ? "l> Next Page\n" : "")}");
            Console.WriteLine($"s> Search [\"{_currentSearch}\"]");
            Console.WriteLine($"c> Filter By Category [{_currentCategory}]");
            Console.WriteLine("- - - - - - - -");
            Console.WriteLine("d> Discard Changes");
            Console.WriteLine($"w> Save Changes [{ModSelector.Instance.Changed.Count}]");
            Console.WriteLine("q> Exit");
        }

        private string GetCurrentPageInfo()
        {
            StringBuilder res = new StringBuilder();
            for (var i = 0; i < _slots.Count; i++)
            { res.Append($"{i + 1 % 10}> {_slots[i]}\n"); }
            
            return res.ToString();
        }

        private void BuildCurrentPage()
        {
            _slots.Clear();
            for (var i = (int) Math.Pow(10, _currentPage) - 10; i < (int) Math.Pow(10, _currentPage + 1) - 10; i++)
            {
                try
                { _slots.Add(_source[index: i] as Mod); }
                catch (ArgumentOutOfRangeException)
                {
                    _canGoNext = false;
                    return;
                }
            }
            _canGoNext = true;
        }

        private void NextPage()
        {
            if (_canGoNext)
            { _currentPage++; }
        }

        private void PreviousPage()
        {
            if (_currentPage <= 1)
            { return; }

            _currentPage--;
        }

        private bool ShouldExit()
        {
            if (!ModSelector.Instance.Changed.Any())
            { return true; }
           
            TrackDosPlayer.Stop();
            PureFuriasPlayer.PlayLooping();
            Console.Clear();
            Console.WriteLine("] You have changes without saving:");
            Console.WriteLine("- - - - - - - - - - - - - - -\n");
            Console.WriteLine(ModSelector.Instance.GetUnsavedChangesAsString());
            Console.WriteLine("- - - - - - - - - - - - - - -");
            Console.WriteLine("w> Save and Exit");
            Console.WriteLine("q> Discard and Exit");
            Console.WriteLine("Any other key> Fuck go back");

            char key = Console.ReadKey().KeyChar;
            PureFuriasPlayer.Stop();
            TrackDosPlayer.PlayLooping();
            switch (key)
            {
                case 'w':
                    SaveChanges();
                    return true;
                case 'q':
                    return true;
                default:
                    return false;
            }
        }

        private void SaveChanges()
        {
            List<Mod> conflicts = ModSelector.Instance.GetConflictingMods().ToList();
            if (conflicts.Any())
            {
                Console.Clear();
                Console.WriteLine("] There are conflicting mods:");
                Console.WriteLine("- - - - - - - - - - - - - - -\n");
                foreach (Mod mod in conflicts)
                { Console.WriteLine(mod); }
                Console.WriteLine();
                Console.WriteLine("- - - - - - - - - - - - - - -");
                Console.WriteLine("w> I don't care");
                Console.WriteLine("Any other key> Fuck go back");

                if (Console.ReadKey().KeyChar != 'w')
                { return; }
            }
            ModSelector.Instance.SaveChanges(UnusedModsFolder, UsedModsFolder);
        }

        private void Search()
        {
            Console.WriteLine("\b- - - - - - - -");
            Console.Write("Search> ");
            _currentSearch = Console.ReadLine();
            bool isNull = string.IsNullOrEmpty(_currentSearch);
            _source = !isNull
                ? ModSelector.GetMatchingModsWhenSearchingByNameAsOrderedDict(_currentSearch, _source)
                : ModSelector.Instance.Mods;
            if (isNull)
            { _currentCategory = ""; }
        }

        private void Category()
        {
            Console.WriteLine("\b- - - - - - - -");
            Console.Write("Category> ");
            try
            {
                string tempCategory = Console.ReadLine();
                bool isNull = string.IsNullOrEmpty(tempCategory);
                _source = !string.IsNullOrEmpty(tempCategory)
                    ? ModSelector.GetModsInCategoryAsOrderedDict(Mod.GetCategoryId(tempCategory), _source)
                    : ModSelector.Instance.Mods;
                _currentCategory = tempCategory;
                if (isNull)
                { _currentSearch = ""; }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Category.");
                Console.ReadKey();
            }
        }
    }
}
