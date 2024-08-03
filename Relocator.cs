using Sons.Crafting.Structures;
using SonsSdk;
using SonsSdk.Attributes;
using SUI;
using static Relocator.Utils;

namespace Relocator;

public class Relocator : SonsMod, IOnGameActivatedReceiver
{
    protected override void OnSdkInitialized()
    {
        SettingsRegistry.CreateSettings(this, null, typeof(Config));
        Config.ToggleKey.Notify(() =>
        {
            Config.Enabled.Value = !Config.Enabled.Value;
            Apply();

            if (IsInGame) 
                SonsTools.ShowMessage("Relocation " + (Config.Enabled.Value ? "enabled" : "disabled"));
        });
    }

    public void OnGameActivated()
    {
        Config.BackupOriginalValues();
        Apply();
    }

    public static void Apply(StructureRecipe recipe, bool allowRelocate)
    { 
        recipe._relocateMode = allowRelocate ? StructureRecipe.RelocateModeType.Relocate : Config.OriginalValues[recipe._id];
    }

    public static void Apply()
    {
        if (!IsInGame)
            return;

        ForRecipe(x=>Apply(x, Config.Enabled.Value && Config.Entries[x._category].Value));
    }
}