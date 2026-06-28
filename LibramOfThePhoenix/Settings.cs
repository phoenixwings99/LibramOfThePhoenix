using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Localization;
using Kingmaker.UI.SettingsUI;
using ModMenu.Settings;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityModManagerNet;
using Menu = ModMenu.ModMenu;

namespace LibramOfThePhoenix
{
    

    internal class Settings
    {
        private static readonly string RootKey = "lotp.settings";
        private static readonly string RootStringKey = "LOTP.Settings";
        private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Settings));

        internal static bool IsEnabled(string key)
        {
            return Menu.GetSettingValue<bool>(GetKey(key.ToLower()));
        }

        internal static T GetDD<T>(string key) where T : Enum
        {
            return Menu.GetSettingValue<T>(GetKey(key));
        }

        internal static bool IsDisabled(string key)
        {
            return !Menu.GetSettingValue<bool>(GetKey(key.ToLower()));
        }
        internal static void Init()
        {
            Logger.Log("Initializing settings.");
            SettingsBuilder settings =
              SettingsBuilder.New(RootKey, GetString("Title"))
                .AddDefaultButton(OnDefaultsApplied);
            //settings.AddSubHeader(GetString("Archetypes.Title"), startExpanded: true);
            settings.AddSubHeader(GetString("Bugfixes.Title"), true);
            settings.AddToggle(MakeToggle("FixWitchSpellIcons", true, true));

            settings.AddSubHeader(GetString("RestoreMissingFeatures.Title"), true);
            settings.AddToggle(MakeToggle("InternalBuffer", true, true));

            settings.AddSubHeader(GetString("ModifyArchetypes.Title"), true);
            settings.AddToggle(MakeToggle("CleanupEldritchScion", true,true));
            settings.AddToggle(MakeToggle("HexcrafterArcanaSelection", true, true));
            settings.AddToggle(MakeToggle("ArcaneRiderFeatSelection", true, true));
            settings.AddToggle(MakeToggle("WitchRestoreStigmatizedPatron", true, true));

            settings.AddSubHeader(GetString("ModifyBloodlines.Title"), true);
            settings.AddToggle(MakeToggle("BuffElementalStrikes", true,true));

            settings.AddToggle(MakeToggle("CombineSorcererDragonClaws", true, true));
            settings.AddToggle(MakeToggle("UnlimitedSorcererBloodlineClaws", true, true));


            settings.AddSubHeader(GetString("NewBloodlines.Title"), true);
            settings.AddToggle(MakeToggle("AzataSorcererBloodline", true, true));


            settings.AddSubHeader(GetString("NewFeatures.Title"), true);
            settings.AddToggle(MakeToggle("BloodHavoc", true,false));
            settings.AddSubHeader(GetString("NewFeats.Title"), true);
            settings.AddToggle(MakeToggle("ArmorOfThePit", true, true));

            settings.AddSubHeader(GetString("NewPatrons.Title"), true);
            settings.AddToggle(MakeToggle("WitchPatronAnimal", true, true));
            settings.AddToggle(MakeToggle("WitchPatronDeath", true, true ));
            settings.AddToggle(MakeToggle("WitchPatronDeathL2replace", true, true));
            settings.AddToggle(MakeToggle("WitchPatronLight", true,true).IsModificationAllowed(() => Settings.GetDD<EmberPatron>("WitchEmberPatron") != EmberPatron.Light));
            settings.AddToggle(MakeToggle("WitchPatronPlague", true, true));
            settings.AddToggle(MakeToggle("PlaguePerniciousPoison", false, false));
            settings.AddToggle(MakeToggle("WitchPatronProtection", true, true));


            settings.AddSubHeader(GetString("NewSpells.Title"), true);
            settings.AddToggle(MakeToggle("BurstOfRadiance", true,true));
            settings.AddToggle(MakeToggle("GreaterShockingGrasp", true,true));
            settings.AddToggle(MakeToggle("StormingGraspLine", true,true));

            settings.AddToggle(MakeToggle("WinterWitchPatronProgression", true, true));

            settings.AddSubHeader(GetString("ModifyCharacters.Title"), true);
            settings.AddDropdown<EmberPatron>(MakeDropdown<EmberPatron>("WitchEmberPatron", EmberPatron.Endurance, UnityEngine.ScriptableObject.CreateInstance<EmberUnityEnumEnum>()).OnValueChanged(x => {

                if (x == EmberPatron.Light)
                    ModMenu.ModMenu.SetSetting(GetKey("WitchPatronLight"), true);
                
            }));

            Menu.AddSettings(settings);
        }



        private static void OnDefaultsApplied()
        {
            Logger.Log($"Default settings restored.");
        }

        private static LocalizedString GetString(string key, bool usePrefix = true)
        {
            var fullKey = usePrefix ? $"{RootStringKey}.{key}" : key;
            return LocalizationTool.GetString(fullKey);
        }

        private static string GetKey(string partialKey)
        {
            return $"{RootKey}.{partialKey}".ToLower();
        }



        private static Toggle MakeToggle(string keyStub, bool defaultVal, bool hasDesc)
        {

            var toggle = Toggle.New($"{RootKey}.{keyStub.ToLower()}", defaultVal, LocalizationTool.GetString($"{RootStringKey}.{keyStub}"));
            if (hasDesc)
                toggle.WithLongDescription(LocalizationTool.GetString($"{RootStringKey}.{keyStub}.Desc"));

            return toggle;
        }

        private static Dropdown<T> MakeDropdown<T>(string keyStub, T defaultVal, UISettingsEntityDropdownEnum<T> dd, bool desc = false) where T : Enum
        {

            var dropper = Dropdown<T>.New(GetKey(keyStub), defaultVal, LocalizationTool.GetString($"{RootStringKey}.{keyStub}"), dd);
            if (desc)
                dropper.WithLongDescription(LocalizationTool.GetString($"{RootStringKey}.{keyStub}.Desc"));
            return dropper;

        }

        public enum EmberPatron
        {
            Elements,
            Healing,
            Endurance,
            Light
        }
        // Declare a non-generic class which inherits from the generic type
        private class EmberUnityEnumEnum : UISettingsEntityDropdownEnum<EmberPatron>
        { }

    }
}
