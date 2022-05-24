using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ModSelector.classes
{
    public class Mod
    {
        public short[] Categories { get; }
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
                "map" => 9,
                _ => throw new ArgumentException(name)
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
                9 => "Map",
                _ => throw new ArgumentException(id.ToString())
            };
        }

        public bool NameMatches(string regex)
        { return NameMatches(regex, this); }
        
        public static bool NameMatches(string regex, Mod mod)
        { return new Regex(regex, RegexOptions.IgnoreCase).Match(mod.Name).Success; }

        public override string ToString() =>
            $"[{(Enabled ? "X" : " ")}] | ({Id}) : {Name} : {string.Join(", ", Categories.Select(GetCategoryName))}{(ConflictingCategories.Any() ? " | [!!]" : "")}";
            // $"[{(Enabled ? "X" : " ")}] | ({Id}) : {Name} : {string.Join(", ", Categories.Select(GetCategoryName))}{(InConflict ? " | [!!]" : "")}";
    }
}
