using System;

namespace ModSelector
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Load data from TheList.txt
            // TheList format: ID : ModName : Tag1, Tag2, Tag3
            // Tags are consts in Mod.cs
            // Use GetCategoryId to initialize mod categories
            // Check which mods are enabled by reading the folders on the mod folder
            // como que on
            
            // TODO: The main menu GUI
            // You enable or disable values with certain keys (numbers)
            // Could be implemented with OrderedDictionary<KeyCode, Mod>
            // TODO: The main functionality
            // Copy the selected mods to the mods folder, delete unselected ones from the folder
            // (Maybe compare if they're already there before copying?)
            // (Use a kind of changes list and do the changes on saveOption)
            // TODO: Check for conflicts
            // If you have two of the same category, warn about conflicts
            // Possible implementation:
            // Have a dict with categories as keys and insert {category, mod} when enabling
            // If the key already exists, then there's a conflict
            
            // TODO: Make a search function
            // Regex
        }
    }
}
