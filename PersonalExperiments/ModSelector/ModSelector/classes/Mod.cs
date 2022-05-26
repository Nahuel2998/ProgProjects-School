using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ModSelector.classes
{
    public class Mod
    {
        public short[] Categories { get; }
        private string CategoriesString { get; }
        public string Id { get; }
        public string Name { get; }
        public bool Enabled { get; set; }
        // public bool InConflict { get; set; }
        public bool NameContainsAru { get; }
        public HashSet<short> ConflictingCategories { get; } = new();
        
        public Mod(string id, string name, params short[] categories)
        {
            Categories = categories;
            Id = id;
            Name = name;
            CategoriesString = GetCategoriesAsString();
            NameContainsAru = NameMatches(@"\bAru\b");
        }
        
        public Mod(string id, string name, params string[] categories)
            : this(id, name, categories.Select(GetCategoryId).ToArray()) { }

        public static short GetCategoryId(string name)
        {
            return name.ToLower() switch
            {
                "erina" => 0,
                "ribbon" => 1,
                "carrot" => 2,
                "vnsprites" => 3,
                "menu" => 4,
                "ui" => 5,
                "cicini" => 6,
                "cocoa" => 7,
                "sounds" => 8,
                "hammer" => 9,
                "map" => 10,
                "other" => 11,
                _ => throw new ArgumentException("Invalid CategoryId: " + name)
            };
        }

        public static string GetCategoryName(short id)
        {
            return id switch
            {
                0 => "Erina",
                1 => "Ribbon",
                2 => "Carrot",
                3 => "VNSprites",
                4 => "Menu",
                5 => "UI",
                6 => "Cicini",
                7 => "Cocoa",
                8 => "Sounds",
                9 => "Hammer",
                10 => "Map",
                11 => "Other",
                _ => throw new ArgumentException("Invalid CategoryName : " + id)
            };
        }

        public static short GetAmountOfTotalCategories()
        { return 12; }

        public bool NameMatches(string regex)
        { return NameMatches(regex, this); }
        
        public static bool NameMatches(string regex, Mod mod)
        { return new Regex(regex, RegexOptions.IgnoreCase).Match(mod.Name).Success; }

        public string GetCategoriesAsString(string separator = ", ")
        { return string.Join(separator, Categories.Select(GetCategoryName)); }
        
        public override string ToString() =>
            $"[{(Enabled ? "X" : " ")}] | ({Id}) : {Name} : {CategoriesString}{(ConflictingCategories.Any() ? " | [!!]" : "")}";
            // $"[{(Enabled ? "X" : " ")}] | ({Id}) : {Name} : {string.Join(", ", Categories.Select(GetCategoryName))}{(InConflict ? " | [!!]" : "")}";
            
        // public string ToString(int namePadding) =>
            // $"[{(Enabled ? "X" : " ")}] | ({Id}) : {Name.PadRight(namePadding)} : {GetCategoriesAsString()}{(ConflictingCategories.Any() ? " | [!!]" : "")}";
        
        // public string ToString(int namePadding, int categoriesPadding) =>
            // $"[{(Enabled ? "X" : " ")}] | ({Id}) : {Name.PadRight(namePadding)} : {GetCategoriesAsString().PadRight(categoriesPadding)}{(ConflictingCategories.Any() ? " | [!!]" : "")}";
    }
}
