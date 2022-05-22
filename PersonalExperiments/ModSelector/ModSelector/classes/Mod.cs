using System;

namespace ModSelector.classes
{
    public class Mod
    {
        public short[] Categories { get; }
        public string Id { get; }
        public string Name { get; }
        public bool Enabled { get; set; }
        
        public Mod(string name, string id, bool enabled = false, params short[] category)
        {
            Categories = category;
            Id = id;
            Name = name;
            Enabled = enabled;
        }

        public static short GetCategoryId(string name)
        {
            return name switch
            {
                "Erina" => 0,
                "Ribbon" => 1,
                "Carrot" => 2,
                "VNSprites" => 3,
                "Menu" => 4,
                "UI" => 5,
                "Cicini" => 6,
                "Cocoa" => 7,
                "Sounds" => 8,
                "Map" => 9,
                _ => throw new ArgumentException(name)
            };
        }
    }
}
