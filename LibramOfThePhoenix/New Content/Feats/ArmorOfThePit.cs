using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibramOfThePhoenix.New_Content.Feats
{
    public class ArmorOfThePit
    {
        public static void Make()
        {
            

            FeatureConfigurator.New("ArmorOfThePitSSFeature", Main.LotPContext.Blueprints.GetGUID("ArmorOfThePitSSFeature").m_Guid.ToString())
                .SetHideInUI()
                .SetHideInCharacterSheetAndLevelUp()
                .Configure();
            FeatureConfigurator.New("ArmorOfThePitNoSSFeature", Main.LotPContext.Blueprints.GetGUID("ArmorOfThePitNoSSFeature").m_Guid.ToString())
                .SetHideInUI()
                .SetHideInCharacterSheetAndLevelUp()
                .Configure();

            if (Settings.IsEnabled("ArmorOfThePit"))
            {
                FeatureConfigurator.New("ArmorOfThePitFeature", Main.LotPContext.Blueprints.GetGUID("ArmorOfThePitFeature").m_Guid.ToString(), FeatureGroup.Feat, FeatureGroup.Racial)
                .SetIsClassFeature()

               .SetDisplayName(LocalizationTool.GetString("ArmorOfThePit"))
               .SetDescription(LocalizationTool.GetString("ArmorOfThePit.Desc"))
               .Configure();

                FeatureConfigurator.For("ArmorOfThePitFeature")
                                        
                    .AddPrerequisiteFeature("5c4e42124dc2b4647af6e36cf2590500")//Tiefling
                    .Configure();

                FeatureConfigurator.For("ArmorOfThePitNoSSFeature")
                     .SetDisplayName(LocalizationTool.GetString("ArmorOfThePit"))
                    .AddStatBonus(Kingmaker.Enums.ModifierDescriptor.NaturalArmor, stat: Kingmaker.EntitySystem.Stats.StatType.AC, value: 2)
                    .Configure();

                FeatureConfigurator.For("ArmorOfThePitSSFeature")
                     .SetDisplayName(LocalizationTool.GetString("ArmorOfThePit"))
                    .AddStatBonus(Kingmaker.Enums.ModifierDescriptor.NaturalArmor, stat: Kingmaker.EntitySystem.Stats.StatType.AC, value: 2)
                    .Configure();

                


                
            }
            else
            {
                FeatureConfigurator.New("ArmorOfThePitFeature", Main.LotPContext.Blueprints.GetGUID("ArmorOfThePitFeature").m_Guid.ToString())
               .SetDisplayName(LocalizationTool.GetString("ArmorOfThePit"))
               .SetDescription(LocalizationTool.GetString("ArmorOfThePit.Desc"))
               .Configure();
            }
        }

        public static void DoInterop()
        {
            if (Settings.IsEnabled("ArmorOfThePit") && Main.IsAlternateRacialTraitsEnabled())
            {
                
                FeatureConfigurator.For("ArmorOfThePitSSFeature").AddFacts(facts: new() { "c978ada1-28d7-9a5f-97c8-49725ab6a57d", "7e2d150e-5b36-7a59-a022-f0cd9978e8c2", "81cae229-91ff-c457-a2d8-96d422f9d3af" }).Configure();

                FeatureConfigurator.For("ArmorOfThePitFeature").AddFeatureIfHasFact(checkedFact: "ba57996c-9173-955f-a1db-305595234fc3", not: true, feature: "ArmorOfThePitNoSSFeature").AddFeatureIfHasFact(checkedFact: "ba57996c-9173-955f-a1db-305595234fc3", feature: "ArmorOfThePitSSFeature").Configure();
            }
            else
            {
                FeatureConfigurator.For("ArmorOfThePitFeature").AddFacts(new List<Blueprint<Kingmaker.Blueprints.BlueprintUnitFactReference>>() { "ArmorOfThePitNoSSFeature" }).Configure();
            }
        }
    }
}
