using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.ActivatableAbilities;
using LibramOfThePhoenix.New_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibramOfThePhoenix.New_Content.Features
{
    internal class KineticistInternalBuffer
    {
        public static void Make()
        {
            string resourceSysName = "InternalBufferResource";
            UnityEngine.Sprite icon = BlueprintTool.Get<BlueprintActivatableAbility>("00b6d36e31548dc4ab0ac9d15e64a980").Icon;
            
            BlueprintGuid guid = Main.LotPContext.Blueprints.GetGUID(resourceSysName);
            AbilityResourceConfigurator internalbufferResourceCOnfig = AbilityResourceConfigurator.New("InternalBufferResource", guid.ToString());
            internalbufferResourceCOnfig.SetLocalizedName("InternalBufferResource");
            internalbufferResourceCOnfig.SetLocalizedDescription("InternalBuffer.Desc");
            internalbufferResourceCOnfig.SetMaxAmount(ResourceAmountBuilder.New(1));

            internalbufferResourceCOnfig.SetIcon(icon);

            BlueprintAbilityResource resourceMade = internalbufferResourceCOnfig.Configure();

            BuffConfigurator buffConfig = BuffConfigurator.New("InternalBufferBuff", Main.LotPContext.Blueprints.GetGUID("InternalBufferBuff").m_Guid.ToString());
            buffConfig.SetDisplayName("InternalBufferBuff").SetDescription("InternalBufferBuff.Desc");
            buffConfig.SetIcon(icon);
            buffConfig.AddComponent<InternalBufferComponent>(x =>
            {

                x.resource = resourceMade.ToReference<BlueprintAbilityResourceReference>();
            });
            Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff buff = buffConfig.Configure();
            ActivatableAbilityConfigurator abilityConfig = ActivatableAbilityConfigurator.New("InternalBufferActivatableAbility", Main.LotPContext.Blueprints.GetGUID("InternalBufferActivatableAbility").m_Guid.ToString());
            abilityConfig.SetDisplayName("InternalBuffer").SetDescription("InternalBuffer.Desc");
            abilityConfig.SetIcon(icon);

           
            abilityConfig.SetBuff(buff);
            abilityConfig.SetActivationType(AbilityActivationType.Immediately);
            abilityConfig.SetActivateWithUnitCommand(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free);
            abilityConfig.AddActivatableAbilityResourceLogic(requiredResource: resourceMade, spendType: ActivatableAbilityResourceLogic.ResourceSpendType.Never);
            abilityConfig.SetDeactivateImmediately(true);
            abilityConfig.AddComponent<RestrictionUnitHasResource>(x =>
            {
                x._blueprintAbilityResourceReference = resourceMade.ToReference<BlueprintAbilityResourceReference>();
            });

            BlueprintActivatableAbility ability = abilityConfig.Configure();

            FeatureConfigurator featureConfig =  FeatureConfigurator.New("InternalBufferFeature", Main.LotPContext.Blueprints.GetGUID("InternalBufferFeature").m_Guid.ToString())
                .SetDisplayName("InternalBuffer").SetDescription("InternalBuffer.Desc").SetIcon(icon);

            featureConfig.SetIsClassFeature(true);

            featureConfig.AddFacts(facts: new() { ability });
            featureConfig.AddAbilityResources(0, resourceMade, restoreOnLevelUp: true);
            BlueprintFeature feature = featureConfig.Configure();

            FeatureConfigurator featureConfig2 = FeatureConfigurator.New("InternalBufferExtraUseFeature", Main.LotPContext.Blueprints.GetGUID("InternalBufferFeature").m_Guid.ToString())
                .SetDisplayName("InternalBuffer.ExtraUse").SetDescription("InternalBuffer.Desc").SetIcon(icon);
            
            featureConfig2.SetIsClassFeature(true);
            featureConfig2.SetRanks(20);

            featureConfig2.AddFacts(facts: new() { ability });
            featureConfig2.AddIncreaseResourceAmount(resourceMade, 1);
            BlueprintFeature feature2 = featureConfig2.Configure();


            Main.LotPContext.Logger.LogPatch(feature);

        }
    }
}
