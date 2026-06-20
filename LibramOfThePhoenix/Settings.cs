using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Localization;
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

            settings.AddToggle(MakeToggle("CleanupEldritchScion", true,true));
            settings.AddToggle(MakeToggle("BuffElementalStrikes", true,true));
            settings.AddToggle(MakeToggle("InternalBuffer", true,true));


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
            return $"{RootKey}.{partialKey}";
        }

        private static Toggle MakeToggle(string keyStub, bool defaultVal, bool hasDesc)
        {

            var toggle = Toggle.New($"{RootKey}.{keyStub.ToLower()}", defaultVal, LocalizationTool.GetString($"{RootStringKey}.{keyStub}"));
            if (hasDesc)
                toggle.WithLongDescription(LocalizationTool.GetString($"{RootStringKey}.{keyStub}.Desc"));

            return toggle;
        }
    }
}
