using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.BasicEx;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using LibramOfThePhoenix.Bugfixes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static LibramOfThePhoenix.New_Components.UnitPartTouchChargesBirb;
using static TabletopTweaks.Core.MechanicsChanges.MetamagicExtention;

namespace LibramOfThePhoenix.New_Content.Spells
{
    internal class Electrical_Touch_Spells
    {
        public static void Make()
        {
            var icon = BlueprintTool.Get<BlueprintAbility>("ab395d2335d3f384e99dddee8562978f").Icon;

            AbilityConfigurator.NewSpell("GreaterShockingGrasp", Main.LotPContext.Blueprints.GetGUID("GreaterShockingGrasp").m_Guid.ToString(), Kingmaker.Blueprints.Classes.Spells.SpellSchool.Evocation, false, Kingmaker.Blueprints.Classes.Spells.SpellDescriptor.Electricity)
                .SetDisplayName("GreaterShockingGrasp")
                .SetDescription("GreaterShockingGrasp.Desc")
                .Configure();

            AbilityConfigurator.NewSpell("GreaterShockingGraspCast", Main.LotPContext.Blueprints.GetGUID("GreaterShockingGraspCast").m_Guid.ToString(), Kingmaker.Blueprints.Classes.Spells.SpellSchool.Evocation, false, Kingmaker.Blueprints.Classes.Spells.SpellDescriptor.Electricity)
                .SetDisplayName("GreaterShockingGrasp")
                .SetDescription("GreaterShockingGrasp.Desc")
                .Configure();

            AbilityConfigurator.NewSpell("StormingGrasp", Main.LotPContext.Blueprints.GetGUID("StormingGrasp").m_Guid.ToString(), Kingmaker.Blueprints.Classes.Spells.SpellSchool.Evocation, false, Kingmaker.Blueprints.Classes.Spells.SpellDescriptor.Electricity)
                .SetDisplayName("StormingGrasp")
                .SetDescription("StormingGrasp.Desc")
                .Configure();

            AbilityConfigurator.NewSpell("StormingGraspCast", Main.LotPContext.Blueprints.GetGUID("StormingGraspCast").m_Guid.ToString(), Kingmaker.Blueprints.Classes.Spells.SpellSchool.Evocation, false, Kingmaker.Blueprints.Classes.Spells.SpellDescriptor.Electricity)
                .SetDisplayName("StormingGrasp")
                .SetDescription("StormingGrasp.Desc")
               .Configure();

            AbilityConfigurator.NewSpell("LesserStormingGraspCast", Main.LotPContext.Blueprints.GetGUID("LesserStormingGraspCast").m_Guid.ToString(), Kingmaker.Blueprints.Classes.Spells.SpellSchool.Evocation, false, Kingmaker.Blueprints.Classes.Spells.SpellDescriptor.Electricity)
                .SetDisplayName("LesserStormingGrasp")
                .SetDescription("LesserStormingGrasp.Desc")
               .Configure();

            AbilityConfigurator.NewSpell("LesserStormingGrasp", Main.LotPContext.Blueprints.GetGUID("LesserStormingGrasp").m_Guid.ToString(), Kingmaker.Blueprints.Classes.Spells.SpellSchool.Evocation, false, Kingmaker.Blueprints.Classes.Spells.SpellDescriptor.Electricity)
                .SetDisplayName("LesserStormingGrasp")
                .SetDescription("LesserStormingGrasp.Desc")
               .Configure();

            AbilityConfigurator.NewSpell("GreaterStormingGraspCast", Main.LotPContext.Blueprints.GetGUID("GreaterStormingGraspCast").m_Guid.ToString(), Kingmaker.Blueprints.Classes.Spells.SpellSchool.Evocation, false, Kingmaker.Blueprints.Classes.Spells.SpellDescriptor.Electricity)
                .SetDisplayName("GreaterStormingGrasp")
                .SetDescription("GreaterStormingGrasp.Desc")
               .Configure();

            AbilityConfigurator.NewSpell("GreaterStormingGrasp", Main.LotPContext.Blueprints.GetGUID("GreaterStormingGrasp").m_Guid.ToString(), Kingmaker.Blueprints.Classes.Spells.SpellSchool.Evocation, false, Kingmaker.Blueprints.Classes.Spells.SpellDescriptor.Electricity)
                 .SetDisplayName("GreaterStormingGrasp")
                .SetDescription("GreaterStormingGrasp.Desc")
               .Configure();

            if (Settings.IsEnabled("GreaterShockingGrasp"))
            {
                AbilityConfigurator.For("GreaterShockingGraspCast")
                    .AddAbilityEffectStickyTouch(touchDeliveryAbility: "GreaterShockingGrasp")
                    .SetIcon(icon)
                    .SetType(AbilityType.Spell)
                    .SetRange(AbilityRange.Touch)
                    .SetCanTargetEnemies(true)
                    .SetCanTargetSelf(true)
                    .SetShouldTurnToTarget(true)
                    .SetSpellResistance(true)
                    .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                    .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Self)
                    .SetLocalizedSavingThrow(SavingThrow.FortPartial)
                    .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                    .SetAvailableMetamagic(Metamagic.Empower, Metamagic.Maximize, Metamagic.Quicken, Metamagic.Heighten, Metamagic.Reach, Metamagic.Bolstered, Metamagic.CompletelyNormal, Metamagic.Piercing, Metamagic.Intensified)
                    .AddToSpellLists(3, SpellList.Wizard, SpellList.Magus, SpellList.Bloodrager, SpellList.Demon, SpellList.Trickster)
                    .Configure();
                AbilityConfigurator.For("GreaterShockingGrasp")
                   .AddAbilityEffectRunAction(actions: ActionsBuilder.New().DealDamage(damageType: new DamageTypeDescription() { Type = DamageType.Energy, Energy = Kingmaker.Enums.Damage.DamageEnergyType.Electricity}, value:
                        new ContextDiceValue()
                        {

                            DiceType = Kingmaker.RuleSystem.DiceType.D6,
                            DiceCountValue = new ContextValue()
                            {
                                ValueType = ContextValueType.Rank,
                                ValueShared = AbilitySharedValue.Damage,
                                m_AbilityParameter = AbilityParameterType.Level,
                                
                            },
                            BonusValue = ContextValues.Constant(0)
                        }

                   ).SavingThrow(Kingmaker.EntitySystem.Stats.SavingThrowType.Fortitude, onResult: ActionsBuilder.New().ConditionalSaved(failed: ActionsBuilder.New().ApplyBuff(buff: "09d39b38bb7c6014394b6daced9bacd3", durationValue: new ContextDurationValue()
                   {
                       DiceType = Kingmaker.RuleSystem.DiceType.D4,
                       DiceCountValue = ContextValues.Constant(1),
                       BonusValue = ContextValues.Constant(0)
                   }))))
                   .AddAbilitySpawnFx(anchor: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxAnchor.SelectedTarget, time: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxTime.OnApplyEffect, orientationMode: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxOrientation.Copy, prefabLink: new PrefabLink() { AssetId = "3ab291fca61cf3b4da311da82340ee9e" })
                   .AddAbilityDeliverTouch(touchWeapon: "bb337517547de1a4189518d404ec49d4")
                   .AddContextRankConfig(ContextRankConfigs.CasterLevel(max: 10, useMax:true))
                   .SetIcon(icon)
                   .SetType(AbilityType.Spell)
                   .SetRange(AbilityRange.Touch)
                   .SetCanTargetEnemies(true)
                   .SetCanTargetSelf(true)
                   .SetShouldTurnToTarget(true)
                   .SetSpellResistance(true)
                   .SetLocalizedSavingThrow(SavingThrow.FortPartial)
                   .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                   .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Self)
                   .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                   .SetAvailableMetamagic(Metamagic.Empower, Metamagic.Maximize, Metamagic.Quicken, Metamagic.Heighten, Metamagic.Reach, Metamagic.Bolstered, Metamagic.CompletelyNormal, Metamagic.Piercing, Metamagic.Intensified)
                   .Configure();

            }

            if (Settings.IsEnabled("StormingGraspLine"))
            {
                AbilityConfigurator.For("LesserStormingGraspCast")
                    .AddAbilityEffectStickyTouch(touchDeliveryAbility: "LesserStormingGrasp")
                    .SetIcon(icon)
                    .SetType(AbilityType.Spell)
                    .SetRange(AbilityRange.Touch)
                    .SetCanTargetEnemies(true)
                    .SetCanTargetSelf(true)
                    .SetShouldTurnToTarget(true)
                    .SetSpellResistance(true)
                    .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                    .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Self)
                    .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                    .SetAvailableMetamagic(Metamagic.Empower, Metamagic.Maximize, Metamagic.Quicken, Metamagic.Heighten, Metamagic.Reach, Metamagic.Bolstered, Metamagic.CompletelyNormal, Metamagic.Piercing, Metamagic.Intensified)
                    .AddToSpellLists(1, SpellList.Wizard, SpellList.Magus, SpellList.Bloodrager, SpellList.Demon, SpellList.Trickster)
                    .Configure();

                AbilityConfigurator.For("LesserStormingGrasp")
                   .AddAbilityEffectRunAction(actions: ActionsBuilder.New().DealDamage(damageType: new DamageTypeDescription() { Type = DamageType.Energy, Energy = Kingmaker.Enums.Damage.DamageEnergyType.Electricity }, value:
                        new ContextDiceValue()
                        {

                            DiceType = Kingmaker.RuleSystem.DiceType.D6,
                            BonusValue = new ContextValue()
                            {
                                ValueType = ContextValueType.Rank,
                                ValueShared = AbilitySharedValue.Damage,
                                m_AbilityParameter = AbilityParameterType.Level,

                            },
                            DiceCountValue = ContextValues.Constant(1)
                        }

                   ))
                   .AddAbilitySpawnFx(anchor: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxAnchor.SelectedTarget, time: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxTime.OnApplyEffect, orientationMode: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxOrientation.Copy, prefabLink: new PrefabLink() { AssetId = "3ab291fca61cf3b4da311da82340ee9e" })
                   .AddAbilityDeliverTouch(touchWeapon: "bb337517547de1a4189518d404ec49d4")
                   .AddContextRankConfig(ContextRankConfigs.CasterLevel(max: 5, useMax: true))
                   .AddContextRankConfig(ContextRankConfigs.CasterLevel(type: Kingmaker.Enums.AbilityRankType.ProjectilesCount))
                   .AddComponent(new TouchChargesBirb(ContextValues.Rank(Kingmaker.Enums.AbilityRankType.ProjectilesCount)))
                   .SetIcon(icon)
                   .SetType(AbilityType.Spell)
                   .SetRange(AbilityRange.Touch)
                   .SetCanTargetEnemies(true)
                   .SetCanTargetSelf(true)
                   .SetShouldTurnToTarget(true)
                   .SetSpellResistance(true)
                   .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                   .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Self)
                   .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                   .SetAvailableMetamagic(Metamagic.Empower, Metamagic.Maximize, Metamagic.Quicken, Metamagic.Heighten, Metamagic.Reach, Metamagic.Bolstered, Metamagic.CompletelyNormal, Metamagic.Piercing, Metamagic.Intensified)
                   .Configure();

                AbilityConfigurator.For("StormingGraspCast")
                   .AddAbilityEffectStickyTouch(touchDeliveryAbility: "StormingGrasp")
                   .SetIcon(icon)
                   .SetType(AbilityType.Spell)
                   .SetRange(AbilityRange.Touch)
                   .SetCanTargetEnemies(true)
                   .SetCanTargetSelf(true)
                   .SetShouldTurnToTarget(true)
                   .SetSpellResistance(true)
                   .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                   .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Self)
                   .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                   
                   
                   .SetAvailableMetamagic(Metamagic.Empower, Metamagic.Maximize, Metamagic.Quicken, Metamagic.Heighten, Metamagic.Reach, Metamagic.Bolstered, Metamagic.CompletelyNormal, Metamagic.Piercing, Metamagic.Intensified)
                   .AddToSpellLists(3, SpellList.Wizard, SpellList.Magus, SpellList.Bloodrager, SpellList.Demon, SpellList.Trickster)
                   .Configure();

                AbilityConfigurator.For("StormingGrasp")
                  .AddAbilityEffectRunAction(actions: ActionsBuilder.New().DealDamage(damageType: new DamageTypeDescription() { Type = DamageType.Energy, Energy = Kingmaker.Enums.Damage.DamageEnergyType.Electricity }, value:
                       new ContextDiceValue()
                       {

                           DiceType = Kingmaker.RuleSystem.DiceType.D6,
                           DiceCountValue = new ContextValue()
                           {
                               ValueType = ContextValueType.Rank,
                               ValueShared = AbilitySharedValue.Damage,
                               m_AbilityParameter = AbilityParameterType.Level,

                           },
                           BonusValue = ContextValues.Constant(0)
                       }

                  ))
                  .AddAbilitySpawnFx(anchor: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxAnchor.SelectedTarget, time: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxTime.OnApplyEffect, orientationMode: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxOrientation.Copy, prefabLink: new PrefabLink() { AssetId = "3ab291fca61cf3b4da311da82340ee9e" })
                  .AddAbilityDeliverTouch(touchWeapon: "bb337517547de1a4189518d404ec49d4")
                  .AddContextRankConfig(ContextRankConfigs.CasterLevel(max: 5, useMax: true))
                  .AddContextRankConfig(ContextRankConfigs.CasterLevel(type: Kingmaker.Enums.AbilityRankType.ProjectilesCount))
                   .AddComponent(new TouchChargesBirb(ContextValues.Rank(Kingmaker.Enums.AbilityRankType.ProjectilesCount)))
                  .SetIcon(icon)
                  .SetType(AbilityType.Spell)
                  .SetRange(AbilityRange.Touch)
                  .SetCanTargetEnemies(true)
                  .SetCanTargetSelf(true)
                  .SetShouldTurnToTarget(true)
                  .SetSpellResistance(true)
                  
                   
                  .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                  .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Self)
                  .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                  .SetAvailableMetamagic(Metamagic.Empower, Metamagic.Maximize, Metamagic.Quicken, Metamagic.Heighten, Metamagic.Reach, Metamagic.Bolstered, Metamagic.CompletelyNormal, Metamagic.Piercing, Metamagic.Intensified)
                  .Configure();
            }

            if (Settings.IsEnabled("StormingGraspLine") && Settings.IsEnabled("GreaterShockingGrasp"))
            {
                AbilityConfigurator.For("GreaterStormingGraspCast")
                   .AddAbilityEffectStickyTouch(touchDeliveryAbility: "GreaterStormingGrasp")
                   .SetIcon(icon)
                   .SetType(AbilityType.Spell)
                   .SetRange(AbilityRange.Touch)
                   .SetCanTargetEnemies(true)
                   .SetCanTargetSelf(true)
                   .SetShouldTurnToTarget(true)
                   .SetSpellResistance(true)
                   .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                   .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Self)
                   .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                   .SetAvailableMetamagic(Metamagic.Empower, Metamagic.Maximize, Metamagic.Quicken, Metamagic.Heighten, Metamagic.Reach, Metamagic.Bolstered, Metamagic.CompletelyNormal, Metamagic.Piercing, Metamagic.Intensified)
                   .AddToSpellLists(5, SpellList.Wizard, SpellList.Magus, SpellList.Demon, SpellList.Trickster)
                   .AddToSpellLists(4, SpellList.Bloodrager)
                   .SetLocalizedSavingThrow(SavingThrow.FortPartial)
                   .Configure();
                
                AbilityConfigurator.For("GreaterStormingGrasp")
                   .AddAbilityEffectRunAction(actions: ActionsBuilder.New().DealDamage(damageType: new DamageTypeDescription() { Type = DamageType.Energy, Energy = Kingmaker.Enums.Damage.DamageEnergyType.Electricity }, value:
                        new ContextDiceValue()
                        {

                            DiceType = Kingmaker.RuleSystem.DiceType.D6,
                            DiceCountValue = new ContextValue()
                            {
                                ValueType = ContextValueType.Rank,
                                ValueShared = AbilitySharedValue.Damage,
                                m_AbilityParameter = AbilityParameterType.Level,

                            },
                            BonusValue = ContextValues.Constant(0)
                        }
                        

                   ).SavingThrow(Kingmaker.EntitySystem.Stats.SavingThrowType.Fortitude, onResult: ActionsBuilder.New().ConditionalSaved(failed: ActionsBuilder.New().ApplyBuff(buff: "09d39b38bb7c6014394b6daced9bacd3", durationValue: new ContextDurationValue()
                   {
                       DiceType = Kingmaker.RuleSystem.DiceType.D4,
                       DiceCountValue = ContextValues.Constant(1),
                        BonusValue = ContextValues.Constant(0)
                   }))))
                   .AddAbilitySpawnFx(anchor: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxAnchor.SelectedTarget, time: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxTime.OnApplyEffect, orientationMode: Kingmaker.UnitLogic.Abilities.Components.Base.AbilitySpawnFxOrientation.Copy, prefabLink: new PrefabLink() { AssetId = "3ab291fca61cf3b4da311da82340ee9e" })
                   .AddAbilityDeliverTouch(touchWeapon: "bb337517547de1a4189518d404ec49d4")
                   .AddContextRankConfig(ContextRankConfigs.CasterLevel(max: 10, useMax: true))
                   .AddContextRankConfig(ContextRankConfigs.CasterLevel(type: Kingmaker.Enums.AbilityRankType.ProjectilesCount))
                   .AddComponent(new TouchChargesBirb(ContextValues.Rank(Kingmaker.Enums.AbilityRankType.ProjectilesCount)))

                   .SetIcon(icon)
                   .SetType(AbilityType.Spell)
                   .SetRange(AbilityRange.Touch)
                   .SetLocalizedSavingThrow(SavingThrow.FortPartial)
                   .SetCanTargetEnemies(true)
                   .SetCanTargetSelf(true)
                   .SetShouldTurnToTarget(true)
                   .SetSpellResistance(true)
                   .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                   .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Self)
                   .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                   .SetAvailableMetamagic(Metamagic.Empower, Metamagic.Maximize, Metamagic.Quicken, Metamagic.Heighten, Metamagic.Reach, Metamagic.Bolstered, Metamagic.CompletelyNormal, Metamagic.Piercing, Metamagic.Intensified)
                   .Configure();
            }

        }
    }

    //Greater Shocking Grasp
    //Elec Evoc, Bloodrager/Magus/Wizard/Sorc 3
    //This spell functions as shocking grasp except it deals 1d6 points of electricity damage per caster level (maximum 10d6). The target must also make a Fortitude save or be stunned for 1d4 rounds. 

    //Storming Grasp, lesser
    //Elec evoc, Bloodrager/Magus/Wizard/Sorc 2 (reduce to 1, it's a chill touch equivalent)
    //This spell functions as shocking grasp, lesser except you can make one touch per caster level within the duration. Each touch is a separate standard action. This touch attack can be delivered as part of spell combat and spellstrike without needing to cast the spell, so long as it is active. 
    //Use Chill Touch numbers, unscaling LSG would be to weak, scaling LSG would be too strong *if* combined with Bloodline Arcana / Blood Havoc

    //Storming Grasp
    //Elec evoc, Bloodrager/Magus/Wizard/Sorc 3
    //This spell functions as shocking grasp except you can make one touch per caster level within the duration. Each touch is a separate standard action. This touch attack can be delivered as part of spell combat and spellstrike without needing to cast the spell. 

    //Storming Grasp, Greater
    //Elec evoc, Bloodrager 4, Magus/Wizard/Sorc 5
    //This spell functions as shocking grasp, greater except you can make one touch per caster level within the duration. Each touch is a separate standard action. This touch attack can be delivered as part of spell combat and spellstrike without needing to cast the spell, so long as it is active. 

    //Use Charop plus implementation
}
