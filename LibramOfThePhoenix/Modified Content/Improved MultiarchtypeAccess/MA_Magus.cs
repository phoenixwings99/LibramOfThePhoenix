using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.Utilities;

namespace LibramOfThePhoenix.Modified_Content.Improved_MultiarchtypeAccess
{
    public class MA_Magus
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(MA_Magus));
        public static void FreeUpHexcrafter()
        {
            ArchetypeConfigurator.For("79ccf7a306a5d5547bebd97299f6fc89")
                .RemoveFromAddFeatures(6, "ad6b9cecb5286d841a66e23cea3ef7bf")
                .RemoveFromAddFeatures(9, "ad6b9cecb5286d841a66e23cea3ef7bf")
                .RemoveFromAddFeatures(12, "ad6b9cecb5286d841a66e23cea3ef7bf")
                .RemoveFromAddFeatures(15, "ad6b9cecb5286d841a66e23cea3ef7bf")
                .RemoveFromAddFeatures(18, "ad6b9cecb5286d841a66e23cea3ef7bf")
                .RemoveFromRemoveFeatures(6, "e9dc4dfc73eaaf94aae27e0ed6cc9ada")
                .RemoveFromRemoveFeatures(9, "e9dc4dfc73eaaf94aae27e0ed6cc9ada")
                .RemoveFromRemoveFeatures(12, "e9dc4dfc73eaaf94aae27e0ed6cc9ada")
                .RemoveFromRemoveFeatures(15, "e9dc4dfc73eaaf94aae27e0ed6cc9ada")
                .RemoveFromRemoveFeatures(18, "e9dc4dfc73eaaf94aae27e0ed6cc9ada")
                .Configure();
            Logger.Log("Removed Hexcrafter special arcana picker");
            
        }

        public static void FreeUpHexcrafterLast()
        {


            FeatureSelectionConfigurator.For("e9dc4dfc73eaaf94aae27e0ed6cc9ada").AddToAllFeatures("ad6b9cecb5286d841a66e23cea3ef7bf").Configure();
            Logger.Log("Moved hexcrafter hexes into subfolder of main arcana picker");

            FeatureSelectionConfigurator.For("ad6b9cecb5286d841a66e23cea3ef7bf").AddPrerequisiteArchetypeLevel(archetype: "79ccf7a306a5d5547bebd97299f6fc89", characterClass: "45a4607686d96a1498891b3286121780", level: 4).SetGroup(FeatureGroup.WitchHex).SetGroup2(FeatureGroup.None).SetGroups(FeatureGroup.MagusArcana).Configure();
            Logger.Log("Added hexcrafter level requirement to arcana selection");
            
            
        }

        public static void FreeUpArcaneRider()
        {
            ArchetypeConfigurator.For("2dff80a045794cb9b5452fe7b66b79d7")
                .RemoveFromAddFeatures(5, "8e627812dc034b9db12fa396fdc9ec75")
                .RemoveFromAddFeatures(11, "8e627812dc034b9db12fa396fdc9ec75")
                .RemoveFromAddFeatures(17, "8e627812dc034b9db12fa396fdc9ec75")
                .RemoveFromRemoveFeatures(5, "66befe7b24c42dd458952e3c47c93563")
                .RemoveFromRemoveFeatures(11, "66befe7b24c42dd458952e3c47c93563")
                .RemoveFromRemoveFeatures(17, "66befe7b24c42dd458952e3c47c93563")
                .Configure();
        }

        public static void FreeUpArcaneRiderLate()
        {
            FeatureSelectionConfigurator.For("8e627812dc034b9db12fa396fdc9ec75").SetGroup(FeatureGroup.MountedCombatFeat).SetGroup2(FeatureGroup.None).AddPrerequisiteArchetypeLevel(archetype: "79ccf7a306a5d5547bebd97299f6fc89", characterClass: "45a4607686d96a1498891b3286121780").Configure();
            FeatureSelectionConfigurator.For("66befe7b24c42dd458952e3c47c93563").AddToAllFeatures("8e627812dc034b9db12fa396fdc9ec75").Configure();
        }
    }
}
