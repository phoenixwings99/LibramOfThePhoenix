using BlueprintCore.Utils;
using Kingmaker.Localization;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.Utilities;


namespace LibramOfThePhoenix
{
    internal class RefData
    {

        public static LocalizedString OneMinutePerLevelDuration => BlueprintTools.GetBlueprint<BlueprintAbility>("a5e23522eda32dc45801e32c05dc9f96").LocalizedDuration;
        public static LocalizedString RefHalf => BlueprintTools.GetBlueprint<BlueprintAbility>("645558d63604747428d55f0dd3a4cb58").LocalizedSavingThrow;

        public static LocalizedString OneRoundPerLevelDuration => BlueprintTools.GetBlueprint<BlueprintAbility>("62888999171921e4dafb46de83f4d67d").LocalizedDuration;


        public static LocalizedString ReflexPartial => BlueprintTools.GetBlueprint<BlueprintAbility>("0a2f7c6aa81bc6548ac7780d8b70bcbc").LocalizedSavingThrow;
        public static LocalizedString WillPartial => BlueprintTools.GetBlueprint<BlueprintAbility>("dd2a5a6e76611c04e9eac6254fcf8c6b").LocalizedSavingThrow;
        public static LocalizedString TenMinutePerLevelDuration => BlueprintTools.GetBlueprint<BlueprintAbility>("72d9f5adda6387a40a63c49d7781bbbf").LocalizedDuration;
        public static void Load()
        {
            BlueprintTool.AddGuidsByName(("ChallengeEvil", "57aae1aa36b8022479e1cd39f3a85ef9"));
            BlueprintTool.AddGuidsByName(("HideousLaughter", "fd4d9fd7f87575d47aafe2a64a6e2d8d"));
            BlueprintTool.AddGuidsByName(("GoodHope", "a5e23522eda32dc45801e32c05dc9f96"));
            
            BlueprintTool.AddGuidsByName(("FreedomOfMovement", "4c349361d720e844e846ad8c19959b1e"));
            BlueprintTool.AddGuidsByName(("BreakEnchantment", "7792da00c85b9e042a0fdfc2b66ec9a8"));
            BlueprintTool.AddGuidsByName(("ElementalAssessor", "6303b404df12b0f4793fa0763b21dd2c"));
            BlueprintTool.AddGuidsByName(("BrilliantInspiration", "a5c56f0f699daec44b7aedd8b273b08a"));
            BlueprintTool.AddGuidsByName(("ProtectionFromSpells", "42aa71adc7343714fa92e471baa98d42"));
            BlueprintTool.AddGuidsByName(("HeroicInvocation", "43740dab07286fe4aa00a6ee104ce7c1"));
            BlueprintTool.AddGuidsByName(("Dodge", "97e216dbb46ae3c4faef90cf6bbe6fd5"));
            BlueprintTool.AddGuidsByName(("ImprovedInitiative", "797f25d709f559546b29e7bcb181cc74"));
            BlueprintTool.AddGuidsByName(("IronWill", "175d1577bb6c9a04baf88eec99c66334"));
            BlueprintTool.AddGuidsByName(("SpellFocusEnchantment", "c5bf645f128c39b40850cde005b8538f"));
            BlueprintTool.AddGuidsByName(("Mobility", "2a6091b97ad940943b46262600eaeaeb"));
            BlueprintTool.AddGuidsByName(("WeaponFinesse", "90e54424d682d104ab36436bd527af09"));
            BlueprintTool.AddGuidsByName(("FencingGrace", "47b352ea0f73c354aba777945760b441"));


            BlueprintTool.AddGuidsByName(("SorcererClass", "b3a505fb61437dc4097f43c3f8f9a4cf"));
            BlueprintTool.AddGuidsByName(("MagusClass", "45a4607686d96a1498891b3286121780"));
            BlueprintTool.AddGuidsByName(("BloodragerClass", "d77e67a814d686842802c9cfd8ef8499"));
            BlueprintTool.AddGuidsByName(("EldritchScionArchetype", "d078b2ef073f2814c9e338a789d97b73"));
            
        }
    }
}
