using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.NewComponents;
using UnityModManagerNet;

namespace LibramOfThePhoenix.Modified_Content.Classes
{
    internal class Kineticist
    {
        public static void PatchKineticistLate()
        {

            AddInternalBuffer();
            return;



            void AddInternalBuffer()
            {
                if (Settings.IsDisabled("InternalBuffer"))
                    return;

                BlueprintFeature buffer = BlueprintTool.Get<BlueprintFeature>("InternalBufferFeature");
                BlueprintFeature bufferEU = BlueprintTool.Get<BlueprintFeature>("InternalBufferExtraUseFeature");



                BlueprintProgression kinProgression = BlueprintTool.Get<BlueprintProgression>("b79e92dd495edd64e90fb483c504b8df");


                kinProgression.LevelEntries.FirstOrDefault(x => x.Level == 6).m_Features.Add(buffer.ToReference<BlueprintFeatureBaseReference>());
                kinProgression.LevelEntries.FirstOrDefault(x => x.Level == 11).m_Features.Add(bufferEU.ToReference<BlueprintFeatureBaseReference>());
                kinProgression.LevelEntries.FirstOrDefault(x => x.Level == 16).m_Features.Add(bufferEU.ToReference<BlueprintFeatureBaseReference>());


                BlueprintArchetype darkElementalist = BlueprintTool.Get<BlueprintArchetype>("f12f18ae8842425489d29f302e69134c");
                ArchetypeConfigurator.For(darkElementalist).AddToRemoveFeatures(6, buffer).AddToRemoveFeatures(11, bufferEU).AddToRemoveFeatures(16, bufferEU).Configure();


                BlueprintArchetype overwhelmingSoul = BlueprintTool.Get<BlueprintArchetype>("aa11888104d17f7459851e8d559ffa98");
                ArchetypeConfigurator.For(overwhelmingSoul).AddToRemoveFeatures(6, buffer).AddToRemoveFeatures(11, bufferEU).AddToRemoveFeatures(16, bufferEU).Configure();
            }

        }


       
    }
}

