using System.Collections.Immutable;
using RedLoader;
using RedLoader.Preferences;
using Sons.Crafting.Structures;
using SonsSdk.Attributes;
using TheForest.Utils;

namespace Relocator;

public static class Config
{
    public static Dictionary<StructureRecipe.CategoryType, ConfigEntry<bool>> Entries = new();
    public static Dictionary<int, StructureRecipe.RelocateModeType> OriginalValues = new();
    
    public static ConfigCategory Category { get; set; }
        = ConfigSystem.CreateFileCategory("Relocator", "Relocator", "Relocator.cfg");

    [SettingsUiHeader("Choose which categories allow relocating (moving)")]
    public static readonly ConfigEntry<bool> Traps = Make(StructureRecipe.CategoryType.Traps, true);
    public static readonly ConfigEntry<bool> Furniture = Make(StructureRecipe.CategoryType.Furniture);
    public static readonly ConfigEntry<bool> Decoration = Make(StructureRecipe.CategoryType.Decoration);
    public static readonly ConfigEntry<bool> Shelters = Make(StructureRecipe.CategoryType.Shelters);
    public static readonly ConfigEntry<bool> Utility = Make(StructureRecipe.CategoryType.Utility);

    [SettingsUiHeader("Global toggle for the mod")]
    public static readonly ConfigEntry<bool> Enabled 
        = Category.CreateEntry("enabled", true, "Enabled");
    
    public static readonly KeybindConfigEntry ToggleKey =
        Category.CreateKeybindEntry("toggle_key", EInputKey.p, "Toggle key");
    

    public static void OnSettingsUiClosed()
    {
        Relocator.Apply();
    }

    private static ConfigEntry<bool> Make(StructureRecipe.CategoryType category, bool defaultVal = false)
    {
        var entry = Category.CreateEntry("move_" + category, defaultVal, "Move " + category);
        Entries[category] = entry;
        return entry;
    }

    public static void BackupOriginalValues()
    {
        if (OriginalValues.Count == 0)
            Utils.ForRecipe(x=> OriginalValues[x._id] = x._relocateMode);
    }
}