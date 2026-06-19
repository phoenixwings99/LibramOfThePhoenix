using HarmonyLib;
using System.Text;
using System.Reflection;
using UnityModManagerNet;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.JsonSystem;

namespace LibramOfThePhoenix;

public static class Main
{
    internal static Harmony HarmonyInstance;
    internal static UnityModManager.ModEntry.ModLogger Log;
    internal static LotPModContext LotPContext;

    public static bool Load(UnityModManager.ModEntry modEntry)
    {
        Log = modEntry.Logger;
        modEntry.OnGUI = OnGUI;
        HarmonyInstance = new Harmony(modEntry.Info.Id);
        LotPContext = new(modEntry);
        try
        {
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
        }
        catch
        {
            HarmonyInstance.UnpatchAll(HarmonyInstance.Id);
            throw;
        }
        return true;
    }

    public static void OnGUI(UnityModManager.ModEntry modEntry)
    {

    }

    [HarmonyPatch(typeof(BlueprintsCache))]
    public static class BlueprintsCaches_Patch
    {
        private static bool Initialized = false;

        [HarmonyPriority(Priority.First)]
        [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
        public static void Init_Postfix()
        {
            try
            {
                if (Initialized)
                {
                    Log.Log("Already initialized blueprints cache.");
                    return;
                }
                Initialized = true;
                LocalizationTool.LoadEmbeddedLocalizationPacks(
                  "LibramOfThePhoenix.Localization.Settings.json"
                  );
                Log.Log("Patching blueprints.");
                Settings.Init();
                Bugfixes.Classes.Magus.EScionSanityCheck();
                Modified_Content.Bloodlines.BuffedElementalStrikes.Do();
                New_Content.Features.KineticistInternalBuffer.Make();
                // Insert your mod's patching methods here
                // Example
                // SuperAwesomeFeat.Configure()
            }
            catch (Exception e)
            {
                Log.Log(string.Concat("Failed to initialize.", e));
            }
        }
    }

    [HarmonyPatch(typeof(BlueprintsCache), "Init")]
    static class BlueprintsCache_Init_Patch2
    {
        static bool Initialized;

        [HarmonyPriority(Priority.Last)]
        static void Postfix()
        {
            try
            {
                Modified_Content.Classes.Kineticist.PatchKineticistLate();
            }
            catch (Exception e)
            {

                Main.LotPContext.Logger.LogError(e, $"Error caught in Late patch");
            }
        }
    }
}