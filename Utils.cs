using Sons.Crafting.Structures;
using TheForest.Utils;

namespace Relocator;

public static class Utils
{
    public static bool IsInGame => LocalPlayer._instance;
    
    public static void ForRecipe(Action<StructureRecipe> callback)
    {
        foreach (var recipe in ScrewStructureManager._instance._database._recipes)
        {
            callback(recipe);
        }
    }
}