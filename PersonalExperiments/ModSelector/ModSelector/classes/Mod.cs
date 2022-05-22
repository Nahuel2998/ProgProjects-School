using System;
using System.Linq;

namespace ModSelector.classes
{
    public class Mod
    {
        public short[] Categories { get; }
        public string Id { get; }
        public string Name { get; }
        public bool Enabled { get; set; }
        public bool InConflict { get; set; }
        
        public Mod(string id, string name, params short[] categories)
        {
            Categories = categories;
            Id = id;
            Name = name;
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
    }
}
