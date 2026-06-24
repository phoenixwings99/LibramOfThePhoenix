using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using LibramOfThePhoenix.Modified_Content.Archetypes;
using LibramOfThePhoenix.New_Content.Bloodlines;
using LibramOfThePhoenix.New_Content.ClassFeatures;
using LibramOfThePhoenix.New_Content.Spells;
using System.Reflection;
using System.Text;
using TabletopTweaks.Core.NewEvents;
using UnityModManagerNet;

namespace LibramOfThePhoenix;

public static class Main
{
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Main));
    internal static Harmony HarmonyInstance;

    internal static LotPModContext LotPContext;

    public static bool Load(UnityModManager.ModEntry modEntry)
    {

        
        modEntry.OnGUI = OnGUI;
        HarmonyInstance = new Harmony(modEntry.Info.Id);
        LotPContext = new(modEntry);
#if DEBUG
        LotPContext.Debug = true;
        LotPContext.Blueprints.Debug = true;
#endif
        try
        {
            EventBus.Subscribe(new BlueprintCacheInitHandler());
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
        }
        catch
        {
            HarmonyInstance.UnpatchAll(HarmonyInstance.Id);
            throw;
        }
        return true;
    }

    class BlueprintCacheInitHandler : IBlueprintCacheInitHandler
    {
        private static bool Initialized = false;
        private static bool InitializeDelayed = false;

        public void AfterBlueprintCacheInit()
        {
            
        }

        public void AfterBlueprintCachePatches()
        {
            try
            {
                if (InitializeDelayed)
                {
                    Logger.Log("Already initialized blueprints cache.");
                    return;
                }
                InitializeDelayed = true;
                Modified_Content.Improved_MultiarchtypeAccess.MA_Magus.FreeUpHexcrafterLast();
                Modified_Content.Improved_MultiarchtypeAccess.MA_Magus.FreeUpArcaneRiderLate();

            }
            catch (Exception e)
            {
                Logger.LogException("Delayed blueprint configuration failed.", e);
            }
        }

        public void BeforeBlueprintCacheInit()
        {
            
        }

        public void BeforeBlueprintCachePatches()
        {
            
        }
    }
    private static bool? TTCInstalled;
    public static bool IsTTCInstalled()
    {
        return TTCInstalled ??= UnityModManager.modEntries.Exists(x => x.Info.Id.Equals("TabletopTweaks-Core"));
    }

    internal static bool IsCharOpsPlusEnabled()
    {
        return UnityModManager.modEntries.Where(
            mod => mod.Info.Id.Equals("CharacterOptionsPlus") && mod.Enabled && !mod.ErrorOnLoading)
          .Any();
    }

    internal static bool IsExpandedContentEnabled()
    {
        return UnityModManager.modEntries.Where(
            mod => mod.Info.Id.Equals("ExpandedContent") && mod.Enabled && !mod.ErrorOnLoading)
          .Any();
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
                    Logger.Log("Already initialized blueprints cache.");
                    return;
                }
                Initialized = true;
                LocalizationTool.LoadEmbeddedLocalizationPacks(
                  "LibramOfThePhoenix.Localization.Settings.json",
                  "LibramOfThePhoenix.Localization.Bloodlines.json",
                  "LibramOfThePhoenix.Localization.Kinetics.json",
                  "LibramOfThePhoenix.Localization.Modifications.json",
                  "LibramOfThePhoenix.Localization.Spells.json"
                  );
                Logger.Log("Patching blueprints.");
                Settings.Init();
                RefData.Load();
                Bugfixes.Classes.Magus.EScionSanityCheck();
                Modified_Content.Bloodlines.BuffedElementalStrikes.Do();
                New_Content.Features.KineticistInternalBuffer.Make();
                Modified_Content.Improved_MultiarchtypeAccess.MA_Magus.FreeUpHexcrafter();
                Modified_Content.Improved_MultiarchtypeAccess.MA_Magus.FreeUpArcaneRider();
                AzataBloodline.Make();
                BloodHavoc.Setup();
                BurstOfRadiance.Make();
                WitchPatrons.Make();
                StigmatizedWitch.ReturnAccursedPatrons();
                // Insert your mod's patching methods here
                // Example
                // SuperAwesomeFeat.Configure()
            }
            catch (Exception e)
            {
                Logger.Log(string.Concat("Failed to initialize.", e));
            }
        }
    }



    
}