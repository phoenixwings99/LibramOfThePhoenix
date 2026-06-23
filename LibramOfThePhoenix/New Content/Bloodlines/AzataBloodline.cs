using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.Properties;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Assets.UnitLogic.Mechanics.Properties;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using LibramOfThePhoenix.New_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.NewComponents.Properties;
using TabletopTweaks.Core.NewUnitParts;
using TabletopTweaks.Core.Utilities;
using UnityEngine;
using static Kingmaker.UnitLogic.Mechanics.Properties.PropertySettings;


namespace LibramOfThePhoenix.New_Content.Bloodlines
{
    internal class AzataBloodline
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AzataBloodline));

        public static void Make()
        {
            Logger.Log("Building Azata Bloodline blank blueprints");
            BlueprintAbility[] spells = new BlueprintAbility[9];
            spells[0] = BlueprintTool.Get<BlueprintAbility>("ChallengeEvil");//Temporary Placeholder for https://www.aonprd.com/SpellDisplay.aspx?ItemName=alluring%20light
            spells[1] = BlueprintTool.Get<BlueprintAbility>("HideousLaughter");//Placeholder for https://www.aonprd.com/SpellDisplay.aspx?ItemName=blessing%20of%20liberty 
            spells[2] = BlueprintTool.Get<BlueprintAbility>("GoodHope");
            spells[3] = BlueprintTool.Get<BlueprintAbility>("FreedomOfMovement");
            spells[4] = BlueprintTool.Get<BlueprintAbility>("BreakEnchantment");
            spells[5] = BlueprintTool.Get<BlueprintAbility>("ElementalAssessor");
            spells[6] = BlueprintTool.Get<BlueprintAbility>("BrilliantInspiration");//Placeholder for https://www.aonprd.com/SpellDisplay.aspx?ItemName=unshakable%20zeal or custom Greater Azata Aspect
            spells[7] = BlueprintTool.Get<BlueprintAbility>("ProtectionFromSpells");
            spells[8] = BlueprintTool.Get<BlueprintAbility>("HeroicInvocation");//Placeholder for https://www.aonprd.com/SpellDisplay.aspx?ItemName=freedom

            Sprite StarRayIcon = BlueprintTool.Get<BlueprintAbility>("99343a2490f699b4198868a58fa0416d").Icon;
            Sprite inspirecourageicon = BlueprintTool.Get<BlueprintActivatableAbility>("5250fe10c377fdb49be449dfe050ba70").Icon;
            Sprite resplendantIcon = BlueprintTool.Get<BlueprintBuff>("8a65794ca43f67542a62c2cb5f306022").Icon;

            var AzataBloodlinePrereq = FeatureConfigurator.New("AzataBloodlineRequisiteFeature", Main.LotPContext.Blueprints.GetGUID("AzataBloodlineRequisiteFeature").m_Guid.ToString())
                .SetDisplayName("AzataBloodline")
                .SetDescription("AzataBloodlineRequisite.Desc")
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(true)
                .SetRanks(1)
                .Configure();

            var basebloodline = ProgressionConfigurator.New("SorcererAzataBloodline", Main.LotPContext.Blueprints.GetGUID("SorcererAzataBloodline").m_Guid.ToString())
                .SetDisplayName("AzataBloodline")
                .SetDescription("SorcererAzataBloodline.Desc")
                .Configure();

            var seekerbloodline = ProgressionConfigurator.New("SeekerAzataBloodline", Main.LotPContext.Blueprints.GetGUID("SeekerAzataBloodline").m_Guid.ToString())
                .SetDisplayName("AzataBloodline")
                .SetDescription("SorcererAzataBloodline.Desc")
                .Configure();

            var crossbloodedbloodline = ProgressionConfigurator.New("CrossbloodedAzataBloodline", Main.LotPContext.Blueprints.GetGUID("CrossbloodedAzataBloodline").m_Guid.ToString())
                .SetDisplayName("AzataBloodline")
                .SetDescription("SorcererAzataBloodline.Desc")
                .Configure();

            FeatureConfigurator.New("SorcererAzataBloodlineArcana", Main.LotPContext.Blueprints.GetGUID("SorcererAzataBloodlineArcana").m_Guid.ToString())
                .SetDisplayName("SorcererAzataBloodlineArcana")
                .SetDescription("SorcererAzataBloodlineArcana.Desc")
                .Configure();

            FeatureConfigurator.New("SorcererAzataRayOfStarlightFeature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataRayOfStarlightFeature").m_Guid.ToString())
                .SetDisplayName("SorcererAzataRayOfStarlightFeature")
                .SetDescription("SorcererAzataRayOfStarlightFeature.Desc")
                .Configure();

            AbilityResourceConfigurator.New("SorcererAzataRayOfStarlightResource", Main.LotPContext.Blueprints.GetGUID("SorcererAzataRayOfStarlightResource").m_Guid.ToString())
                .SetLocalizedName("SorcererAzataRayOfStarlightResource")
                .Configure();

            AbilityConfigurator.New("SorcererAzataRayOfStarlightAbility", Main.LotPContext.Blueprints.GetGUID("SorcererAzataRayOfStarlightAbility").m_Guid.ToString())
                .SetDisplayName("SorcererAzataRayOfStarlightFeature")
                .SetDescription("SorcererAzataRayOfStarlightFeature.Desc")
                .Configure();

            FeatureConfigurator.New("SorcererAzataElysianResistancesFeature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataElysianResistancesFeature").m_Guid.ToString())
                .SetDisplayName("SorcererAzataElysianResistancesFeature")
                .SetDescription("SorcererAzataElysianResistancesFeature.Desc")
                .Configure();

            FeatureConfigurator.New("SorcererAzataElysianResistances2Feature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataElysianResistances2Feature").m_Guid.ToString())
                .SetDisplayName("SorcererAzataElysianResistancesFeature")
                .SetDescription("SorcererAzataElysianResistancesFeature.Desc")
                .Configure();

            FeatureConfigurator.New("SorcererAzataElysianResistancesEffectFeature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataElysianResistancesEffectFeature").m_Guid.ToString())
                .SetDisplayName("SorcererAzataElysianResistancesFeature")
                .SetDescription("SorcererAzataElysianResistancesFeature.Desc")
                .SetHideInCharacterSheetAndLevelUp(true)
                .SetHideInUI(true)
                .Configure();

            FeatureConfigurator.New("SorcererAzataElysianResistances2EffectFeature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataElysianResistances2EffectFeature").m_Guid.ToString())
                .SetDisplayName("SorcererAzataElysianResistancesFeature")
                .SetDescription("SorcererAzataElysianResistancesFeature.Desc")
                .SetHideInCharacterSheetAndLevelUp(true)
                .SetHideInUI(true)
                .Configure();

            FeatureConfigurator.New("SorcererAzataSongOfHeroesFeature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataSongOfHeroesFeature").m_Guid.ToString())
                .SetDisplayName("SorcererAzataSongOfHeroesFeature")
                .SetDescription("SorcererAzataSongOfHeroesFeature.Desc")
                .Configure();

            FeatureConfigurator.New("SorcererAzataSongOfHeroes2Feature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataSongOfHeroes2Feature").m_Guid.ToString())
                .SetDisplayName("SorcererAzataSongOfHeroesFeature")
                .SetDescription("SorcererAzataSongOfHeroesFeature.Desc")
                .Configure();

            FeatureConfigurator.New("SorcererAzataResplendantFormFeature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataResplendantFormFeature").m_Guid.ToString())
                .SetDisplayName("SorcererAzataResplendantFormFeature")
                .SetDescription("SorcererAzataResplendantFormFeature.Desc")
                .SetIcon(resplendantIcon)
                .Configure();

            AbilityConfigurator.New("SorcererAzataResplendantFormAbility", Main.LotPContext.Blueprints.GetGUID("SorcererAzataResplendantFormAbility").m_Guid.ToString())
                .SetDisplayName("SorcererAzataResplendantFormFeature")
                .SetDescription("SorcererAzataResplendantFormFeature.Desc")
                .SetIcon(resplendantIcon)
                .Configure();

            BuffConfigurator.New("SorcererAzataResplendantFormBuff", Main.LotPContext.Blueprints.GetGUID("SorcererAzataResplendantFormBuff").m_Guid.ToString())
                .SetDisplayName("SorcererAzataResplendantFormFeature")
                .SetDescription("SorcererAzataResplendantFormBuff.Desc")
                .SetIcon(resplendantIcon)
                .Configure();

            FeatureConfigurator.New("SorcererAzataSoverignOfStarsFeature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataSoverignOfStarsFeature").m_Guid.ToString())
                .SetDisplayName("SorcererAzataSoverignOfStarsFeature")
                .SetDescription("SorcererAzataSoverignOfStarsFeature.Desc")
                .Configure();

            FeatureConfigurator.New("SorcererAzataSpell1Feature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataSpell1Feature").m_Guid.ToString())
                .SetDisplayName(spells[0].m_DisplayName)
                .SetDescription("SorcererAzataSpellFeature.Desc")
                .Configure();

            FeatureConfigurator.New("SorcererAzataSpell2Feature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataSpell2Feature").m_Guid.ToString())
                .SetDisplayName(spells[1].m_DisplayName)
                .SetDescription("SorcererAzataSpellFeature.Desc")
                .Configure();

            FeatureConfigurator.New("SorcererAzataSpell3Feature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataSpell3Feature").m_Guid.ToString())
               .SetDisplayName(spells[2].m_DisplayName)
               .SetDescription("SorcererAzataSpellFeature.Desc")
               .Configure();

            FeatureConfigurator.New("SorcererAzataSpell4Feature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataSpell4Feature").m_Guid.ToString())
               .SetDisplayName(spells[3].m_DisplayName)
               .SetDescription("SorcererAzataSpellFeature.Desc")
               .Configure();

            FeatureConfigurator.New("SorcererAzataSpell5Feature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataSpell5Feature").m_Guid.ToString())
               .SetDisplayName(spells[4].m_DisplayName)
               .SetDescription("SorcererAzataSpellFeature.Desc")
               .Configure();

            FeatureConfigurator.New("SorcererAzataSpell6Feature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataSpell6Feature").m_Guid.ToString())
               .SetDisplayName(spells[5].m_DisplayName)
               .SetDescription("SorcererAzataSpellFeature.Desc")
               .Configure();

            FeatureConfigurator.New("SorcererAzataSpell7Feature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataSpell7Feature").m_Guid.ToString())
               .SetDisplayName(spells[6].m_DisplayName)
               .SetDescription("SorcererAzataSpellFeature.Desc")
               .Configure();

            FeatureConfigurator.New("SorcererAzataSpell8Feature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataSpell8Feature").m_Guid.ToString())
               .SetDisplayName(spells[7].m_DisplayName)
               .SetDescription("SorcererAzataSpellFeature.Desc")
               .Configure();

            FeatureConfigurator.New("SorcererAzataSpell9Feature", Main.LotPContext.Blueprints.GetGUID("SorcererAzataSpell9Feature").m_Guid.ToString())
               .SetDisplayName(spells[8].m_DisplayName)
               .SetDescription("SorcererAzataSpellFeature.Desc")
               .Configure();

            UnitPropertyConfigurator.New("InspireCourageScalingUnitProperty", Main.LotPContext.Blueprints.GetGUID("InspireCourageScalingUnitProperty").m_Guid.ToString()).Configure();
            UnitPropertyConfigurator.New("AzataBloodlineLevelUnitProperty", Main.LotPContext.Blueprints.GetGUID("AzataBloodlineLevelUnitProperty").m_Guid.ToString()).Configure();

            var featselecton = FeatureSelectionConfigurator.New("SorcererAzataFeatSelection", Main.LotPContext.Blueprints.GetGUID("SorcererAzataFeatSelection").m_Guid.ToString())
               .SetDisplayName("SorcererAzataFeatSelection")
               .SetDescription("SorcererAzataFeatSelection.Desc")
               .Configure();

            if (Settings.IsEnabled("AzataSorcererBloodline") && Main.IsTTCInstalled())
            {
                Logger.Log("Building Azata Bloodline");
                BlueprintFeatureReference BloodlineRequisiteFeature = BlueprintTools.GetBlueprint<BlueprintFeature>("e2cfd3ce-df7c-4008-8b25-aa82d6db3c77").ToReference<BlueprintFeatureReference>();

                FeatureConfigurator.For("SorcererAzataBloodlineArcana")
                    .AddIncreaseSpellDescriptorDC(bonusDC: 2, descriptor: new SpellDescriptorWrapper(SpellDescriptor.Emotion | SpellDescriptor.NegativeEmotion))
                    .SetIsClassFeature(true)
                    .SetReapplyOnLevelUp(true)
                    .SetRanks(1)
                    .Configure();
                Logger.Log("Built Azata Bloodline Arcana");

                FeatureConfigurator.For("SorcererAzataRayOfStarlightFeature").AddFacts(["SorcererAzataRayOfStarlightAbility"])
                    .AddAbilityResources(resource: "SorcererAzataRayOfStarlightResource")
                    .SetIsClassFeature(true)
                    .SetReapplyOnLevelUp(true)
                    .SetRanks(1)
                    .SetIcon(StarRayIcon)
                    .Configure();

                AbilityConfigurator.For("SorcererAzataRayOfStarlightAbility")
                   .AddSpellComponent(SpellSchool.Evocation)
                   .AddAbilityDeliverProjectile(projectiles: ["f00eb27234fbc39448b142f1257c8886"], lineWidth: new Kingmaker.Utility.Feet(5f), needAttackRoll: true, weapon: "f6ef95b1f7bb52b408a5b345a330ffe8")
                   .AddAbilityEffectRunAction(ActionsBuilder.New()
                        .Conditional(conditions: ConditionsBuilder.New().Alignment(AlignmentComponent.Good), 
                            ifFalse: ActionsBuilder.New()
                                .Conditional(conditions: ConditionsBuilder.New().Alignment(alignment: AlignmentComponent.Evil), 
                                ifTrue: ActionsBuilder.New().DealDamage(damageType: new DamageTypeDescription() { Type = DamageType.Energy, Energy = DamageEnergyType.Holy }, value: new ContextDiceValue() { DiceType = DiceType.D6, DiceCountValue = new() { Value = 1 }, BonusValue = new() { ValueType = ContextValueType.Rank, ValueRank = AbilityRankType.DamageBonus } }),
                                ifFalse: ActionsBuilder.New().DealDamage(damageType: new DamageTypeDescription() { Type = DamageType.Energy, Energy = DamageEnergyType.Holy }, value: new ContextDiceValue() { DiceType = DiceType.D6, DiceCountValue = new() { Value = 1 }, BonusValue = new() { ValueType = ContextValueType.Rank, ValueRank = AbilityRankType.DamageBonus } }, half: true))))
                   .SetSpellDescriptor(SpellDescriptor.Good)
                   
                   .AddContextRankConfig(ContextRankConfigs.SumClassLevelWithArchetype(classes: ["SorcererClass", "MagusClass"], archetypes: ["EldritchScionArchetype"]).WithDiv2Progression())
                   .AddAbilityResourceLogic(amount: 1, requiredResource: "SorcererAzataRayOfStarlightResource", isSpendResource: true)
                   .SetRange(AbilityRange.Custom)
                   .SetCustomRange(30)
                   .SetCanTargetEnemies(true)
                   .SetCanTargetSelf(true)
                   .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                   .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Directional)
                   .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                   .SetAvailableMetamagic(Metamagic.Empower, Metamagic.Quicken, Metamagic.Heighten, Metamagic.Maximize)
                   .SetIcon(StarRayIcon)
                   .Configure();

                AbilityResourceConfigurator.For("SorcererAzataRayOfStarlightResource")
                    .SetMaxAmount(ResourceAmountBuilder.New(2).IncreaseByStat(Kingmaker.EntitySystem.Stats.StatType.Charisma))
                    .Configure();

                Logger.Log("Built Azata Bloodline Ray Of Starlight Power");

                FeatureConfigurator.For("SorcererAzataElysianResistancesFeature")
                    .AddFeatureOnClassLevel(clazz: "SorcererClass", additionalClasses: new() { "MagusClass" }, archetypes: new() { "EldritchScionArchetype" }, beforeThisLevel: true, level: 9, feature: "SorcererAzataElysianResistancesEffectFeature")
                    .AddFeatureOnClassLevel(clazz: "SorcererClass", additionalClasses: new() { "MagusClass" }, archetypes: new() { "EldritchScionArchetype" }, beforeThisLevel: false, level: 9, feature: "SorcererAzataElysianResistances2EffectFeature")

                    .SetIsClassFeature(true)
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                FeatureConfigurator.For("SorcererAzataElysianResistances2Feature")
                    .SetIsClassFeature(true)
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                FeatureConfigurator.For("SorcererAzataElysianResistancesEffectFeature")
                    .AddDamageResistanceEnergy(type: DamageEnergyType.Fire, value: ContextValues.Constant(5))
                    .AddDamageResistanceEnergy(type: DamageEnergyType.Cold, value: ContextValues.Constant(5))

                    .SetIsClassFeature(true)
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                FeatureConfigurator.For("SorcererAzataElysianResistances2EffectFeature")
                   .AddDamageResistanceEnergy(type: DamageEnergyType.Fire, value: ContextValues.Constant(10))
                   .AddDamageResistanceEnergy(type: DamageEnergyType.Cold, value: ContextValues.Constant(10))

                   .SetIsClassFeature(true)
                   .SetReapplyOnLevelUp(true)
                   .Configure();

                Logger.Log("Built Azata Bloodline Elysian Resistances Power");

                FeatureConfigurator.For("SorcererAzataSongOfHeroesFeature")
                    .AddIncreaseResourcesByClass(resource: "e190ba276831b5c4fa28737e5e49e6a6", characterClass: "SorcererClass", archetype: "EldritchScionArchetype")
                    .AddFacts(facts: ["5250fe10c377fdb49be449dfe050ba70", "be36959e44ac33641ba9e0204f3d227b"])
                    .AddAbilityResources(resource: "e190ba276831b5c4fa28737e5e49e6a6")
                    .SetIsClassFeature(true)
                    .SetReapplyOnLevelUp(true)
                   .Configure();

                FeatureConfigurator.For("SorcererAzataSongOfHeroes2Feature")
                    .AddFacts(facts: ["a4ce06371f09f504fa86fcf6d0e021e4"])
                    .SetIsClassFeature(true)
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                Logger.Log("Built Azata Bloodline Song Of Heroes Power");

                FeatureConfigurator.For("SorcererAzataResplendantFormFeature")
                    .AddFacts(["SorcererAzataResplendantFormAbility"])
                    .SetIsClassFeature(true)
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                UnitPropertyConfigurator.For("AzataBloodlineLevelUnitProperty")
                    .AddComponent<ProgressionRankGetter>(x =>
                    {
                        x.Progression = BlueprintTool.GetRef<BlueprintProgressionReference>("CrossbloodedAzataBloodline");
                        x.Settings = new Kingmaker.UnitLogic.Mechanics.Properties.PropertySettings()
                        {
                            m_Progression = Progression.AsIs
                        };
                    })
                    .Configure();

                AbilityConfigurator.For("SorcererAzataResplendantFormAbility")
                    .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuff(buff: "SorcererAzataResplendantFormBuff", durationValue: new ContextDurationValue()
                    {
                        DiceType = DiceType.Zero,
                        BonusValue = new ContextValue()
                        {
                            ValueRank = AbilityRankType.Default,
                            ValueType = ContextValueType.CasterCustomProperty,
                            m_CustomProperty = BlueprintTool.GetRef<BlueprintUnitPropertyReference>("AzataBloodlineLevelUnitProperty")
                        }
                    }))
                    .SetLocalizedDuration(Duration.RoundPerLevel)

                    .Configure();

                BuffConfigurator.For("SorcererAzataResplendantFormBuff")
                    .AddIncorporealDamageDivisor()
                    .AddComponent<IncorporealDamageDivisorOutgoing>()
                    .SetIsClassFeature(true)
                    .SetFlags()
                    .Configure();

                Logger.Log("Built Azata Bloodline Resplendant Form Power");

                FeatureConfigurator.For("SorcererAzataSoverignOfStarsFeature")
                    .AddEnergyDamageImmunity(DamageEnergyType.Electricity)
                    .AddConditionImmunity(UnitCondition.Petrified)
                    .AddBuffDescriptorImmunity(descriptor: new SpellDescriptorWrapper(SpellDescriptor.MindAffecting))
                    .AddDamageResistancePhysical(alignment: DamageAlignment.Evil, bypassedByAlignment: true, value: ContextValues.Constant(15))
                    .AddDamageResistancePhysical(material: PhysicalDamageMaterial.ColdIron, bypassedByMaterial: true, value: ContextValues.Constant(15))
                    .Configure();

                Logger.Log("Built Azata Bloodline Soveriegn of Stars Capstone");

                ProgressionConfigurator.For("SorcererAzataBloodline")
                    .SetIsClassFeature(true)
                    .SetClasses("SorcererClass", "MagusClass")
                    .AddToArchetypes("EldritchScionArchetype")
                    .AddToGroups(Kingmaker.Blueprints.Classes.FeatureGroup.BloodLine)
                    .SetRanks(1)
                    .SetIsClassFeature(true)
                    .SetGiveFeaturesForPreviousLevels(true)
                    .AddToLevelEntries(1, "SorcererAzataBloodlineArcana", "SorcererAzataRayOfStarlightFeature")
                    .AddToLevelEntries(3, "SorcererAzataSpell1Feature", "SorcererAzataElysianResistancesFeature")
                    .AddToLevelEntries(5, "SorcererAzataSpell2Feature")
                    .AddToLevelEntries(7, "SorcererAzataSpell3Feature")
                    .AddToLevelEntries(9, "SorcererAzataSpell4Feature", "SorcererAzataElysianResistancesFeature", "SorcererAzataSongOfHeroesFeature")
                    .AddToLevelEntries(11, "SorcererAzataSpell5Feature")
                    .AddToLevelEntries(13, "SorcererAzataSpell6Feature")
                    .AddToLevelEntries(15, "SorcererAzataSpell7Feature", "SorcererAzataResplendantFormFeature")
                    .AddToLevelEntries(17, "SorcererAzataSpell8Feature")
                    .AddToLevelEntries(19, "SorcererAzataSpell9Feature")
                    .AddToLevelEntries(20, "SorcererAzataSoverignOfStarsFeature")
                    .AddPrerequisiteFeature(feature: "AzataBloodlineRequisiteFeature", group: Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.Any)
                    .AddPrerequisiteNoFeature(feature: "AzataBloodlineRequisiteFeature", group: Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.Any)
                    .Configure();

                Logger.Log("Built Azata Bloodline Progression");

                ProgressionConfigurator.For(seekerbloodline)
                    .SetIsClassFeature(true)
                    .SetClasses("SorcererClass", "MagusClass")
                    .AddToArchetypes("EldritchScionArchetype")
                    .AddToGroups(Kingmaker.Blueprints.Classes.FeatureGroup.BloodLine)
                    .SetRanks(1)
                    .SetIsClassFeature(true)
                    .SetGiveFeaturesForPreviousLevels(true)
                    .AddToLevelEntries(1, "SorcererAzataBloodlineArcana", "SorcererAzataRayOfStarlightFeature")
                    .AddToLevelEntries(3, "SorcererAzataSpell1Feature")
                    .AddToLevelEntries(5, "SorcererAzataSpell2Feature")
                    .AddToLevelEntries(7, "SorcererAzataSpell3Feature")
                    .AddToLevelEntries(9, "SorcererAzataSpell4Feature", "SorcererAzataSongOfHeroesFeature")
                    .AddToLevelEntries(11, "SorcererAzataSpell5Feature")
                    .AddToLevelEntries(13, "SorcererAzataSpell6Feature")
                    .AddToLevelEntries(15, "SorcererAzataSpell7Feature")
                    .AddToLevelEntries(17, "SorcererAzataSpell8Feature")
                    .AddToLevelEntries(19, "SorcererAzataSpell9Feature")
                    .AddToLevelEntries(20, "SorcererAzataSoverignOfStarsFeature")
                    .AddPrerequisiteFeature(feature: "AzataBloodlineRequisiteFeature", group: Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.Any)
                    .AddPrerequisiteNoFeature(feature: "AzataBloodlineRequisiteFeature", group: Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.Any)
                    .Configure();

                Logger.Log("Built Azata Bloodline Seeker Progression");

                ProgressionConfigurator.For(crossbloodedbloodline)
                   .SetIsClassFeature(true)
                   .SetClasses("SorcererClass", "MagusClass")
                   .AddToArchetypes("EldritchScionArchetype")
                   .AddToGroups(Kingmaker.Blueprints.Classes.FeatureGroup.BloodLine)
                   .SetRanks(1)
                   .SetIsClassFeature(true)
                   .SetGiveFeaturesForPreviousLevels(true)
                    .AddToLevelEntries(1, "SorcererAzataBloodlineArcana", "SorcererAzataBloodlineArcana", "SorcererAzataRayOfStarlightFeature")
                    .AddToLevelEntries(3, "SorcererAzataElysianResistancesFeature")
                    .AddToLevelEntries(9, "SorcererAzataElysianResistancesFeature", "SorcererAzataSongOfHeroesFeature")
                    .AddToLevelEntries(15, "SorcererAzataResplendantFormFeature")
                    .AddToLevelEntries(20, "SorcererAzataSoverignOfStarsFeature")
                    .AddPrerequisiteFeature(feature: "AzataBloodlineRequisiteFeature", group: Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.Any)
                    .AddPrerequisiteNoFeature(feature: "AzataBloodlineRequisiteFeature", group: Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.Any)
                   .Configure();

                Logger.Log("Built Azata Bloodline Crossblooded Progression");

                FeatureSelectionConfigurator.For("SorcererAzataFeatSelection")
                    .SetRanks(1)
                    .SetIsClassFeature(true)
                    .SetHideInUI(true)
                    .SetHideNotAvailibleInUI(true)
                    .AddToAllFeatures("Dodge", "ImprovedInitiative", "IronWill", "SpellFocusEnchantment", "Mobility", "WeaponFinesse")//Missing: https://www.aonprd.com/FeatDisplay.aspx?ItemName=Dazing%20Spell , https://www.aonprd.com/FeatDisplay.aspx?ItemName=Free%20Spirit
                    .Configure();

                Logger.Log("Built Azata Bloodline Bonus Feats");
                //Add Slashing Grace as replacement for WFinesse if auto-give-base-feats mods are active





                List<BlueprintArchetypeReference> inspireCourageArches = new();
                List<BlueprintCharacterClassReference> inspireCourageClasses = new();
                BuffConfigurator.For("6d6d9e06b76f5204a8b7856c78607d5d").EditComponent<ContextRankConfig>(x => { inspireCourageArches.Add(x.Archetype); inspireCourageArches.AddRange(x.m_AdditionalArchetypes); inspireCourageClasses.AddRange(x.m_Class); }).Configure();



                UnitPropertyConfigurator.For("InspireCourageScalingUnitProperty")
                    .AddSummClassLevelGetter(archetypes: inspireCourageArches.Select(x => (Blueprint<BlueprintArchetypeReference>)x).ToList(), clazz: inspireCourageClasses.Select(x => (Blueprint<BlueprintCharacterClassReference>)x).ToList())
                    .SetOperationOnComponents(Kingmaker.UnitLogic.Mechanics.Properties.BlueprintUnitProperty.MathOperation.Sum)
                    .AddComponent<HasFactConditionCustomPropertyGetter>(x =>
                    {
                        x.property = new ProgressionRankGetter() { Progression = BlueprintTool.GetRef<BlueprintProgressionReference>("SorcererAzataBloodline") };
                        x.factReference = BlueprintTool.GetRef<BlueprintUnitFactReference>("SorcererAzataSongOfHeroesFeature");
                        x.Settings = new Kingmaker.UnitLogic.Mechanics.Properties.PropertySettings()
                        {
                            m_Progression = Progression.AsIs
                        };
                    })
                    .AddComponent<HasFactConditionCustomPropertyGetter>(x =>
                    {
                        x.property = new ProgressionRankGetter() { Progression = BlueprintTool.GetRef<BlueprintProgressionReference>("SeekerAzataBloodline") };
                        x.factReference = BlueprintTool.GetRef<BlueprintUnitFactReference>("SorcererAzataSongOfHeroesFeature");
                        x.Settings = new Kingmaker.UnitLogic.Mechanics.Properties.PropertySettings()
                        {
                            m_Progression = Progression.AsIs
                        };
                    })
                    .AddComponent<HasFactConditionCustomPropertyGetter>(x =>
                    {
                        x.property = new ProgressionRankGetter() { Progression = BlueprintTool.GetRef<BlueprintProgressionReference>("CrossbloodedAzataBloodline") };
                        x.factReference = BlueprintTool.GetRef<BlueprintUnitFactReference>("SorcererAzataSongOfHeroesFeature");
                        x.Settings = new Kingmaker.UnitLogic.Mechanics.Properties.PropertySettings()
                        {
                            m_Progression = Progression.AsIs
                        };

                    })
                    .AddSimplePropertyGetter(property: Kingmaker.UnitLogic.Mechanics.Properties.UnitProperty.MythicLevel, settings: new Kingmaker.UnitLogic.Mechanics.Properties.PropertySettings()
                    {
                        m_CustomProgression = [
                            new CustomProgressionItem() { BaseValue = 1, ProgressionValue = 1},
                            new CustomProgressionItem() { BaseValue = 2, ProgressionValue = 1},
                            new CustomProgressionItem() { BaseValue = 3, ProgressionValue = 1},
                            new CustomProgressionItem() { BaseValue = 4, ProgressionValue = 2},
                            new CustomProgressionItem() { BaseValue = 5, ProgressionValue = 2},
                            new CustomProgressionItem() { BaseValue = 6, ProgressionValue = 2},
                            new CustomProgressionItem() { BaseValue = 7, ProgressionValue = 3},
                            new CustomProgressionItem() { BaseValue = 8, ProgressionValue = 3},
                            new CustomProgressionItem() { BaseValue = 9, ProgressionValue = 3},
                            new CustomProgressionItem() { BaseValue = 0, ProgressionValue = 4}

                        ]

                    }).Configure();

                AbilityAreaEffectConfigurator.For("5d4308fa344af0243b2dd3b1e500b2cc")
                    .RemoveComponents(x =>
                        {
                            if (x is AbilityAreaEffectBuff &&
                                ((x as AbilityAreaEffectBuff).m_Buff.Equals(BlueprintTools.GetBlueprintReference<BlueprintBuffReference>("6d6d9e06b76f5204a8b7856c78607d5d")) ||
                                    (x as AbilityAreaEffectBuff).m_Buff.Equals(BlueprintTools.GetBlueprintReference<BlueprintBuffReference>("c3b8fe0f71eb4744b2a544aa2261a97a")))

                                )
                                return true;
                            else
                                return false;
                        })
                    .AddAbilityAreaEffectBuff(buff: "6d6d9e06b76f5204a8b7856c78607d5d", condition: ConditionsBuilder.New().IsAlly())
                    .Configure();

                Logger.Log("Modified Bardic Performance Resource and Scaling to support Azata Bloodline");

                BloodlineTools.RegisterSorcererFeatSelection(featselecton, basebloodline);


                BloodlineTools.RegisterSorcererBloodline(basebloodline);
                BloodlineTools.RegisterCrossbloodedBloodline(crossbloodedbloodline);
                BloodlineTools.RegisterSeekerBloodline(seekerbloodline);
                Logger.Log("Ran BlueprintTools");
                for (int i = 0; i < 9; i++)
                {
                    FeatureConfigurator.For($"SorcererAzataSpell{i + 1}Feature")
                        .AddKnownSpell(characterClass: "SorcererClass", spell: spells[i], spellLevel: i + 1)
                        .AddKnownSpell(characterClass: "MagusClass", archetype: "EldritchScionArchetype", spell: spells[i], spellLevel: i + 1)
                        .SetIcon(spells[i].Icon)
                        .AddPrerequisiteNoArchetype(characterClass: "SorcererClass", archetype: "65a630aa291f65047b90a2af5df75d83")
                        .AddPrerequisiteNoArchetype(characterClass: "SorcererClass", archetype: "a0e56a59ad0b44b8add84185da6bb845")
                        .AddSpellsToDescription(spells: new List<Blueprint<BlueprintAbilityReference>>() { spells[i] })
                        .Configure();
                    Logger.Log($"Built Azata Bloodline Spell Feature {spells[i].NameSafe()} ");
                }



            }
        }
    }
}

/*
 * 
 * Class Skill: Perform (any).

Bonus Spells: alluring light (3rd) blessing of liberty (5th) good hope (7th) freedom of movement (9th) break enchantment (11th) elemental assessor (13th) unshakable zeal (15th) protection from spells (17th) freedom (19th).

Bonus Feats: Dazing Spell, Dodge, Free Spirit, Improved Initiative, Iron Will, Mobility, Spell Focus (enchantment), Weapon Finesse.

Bloodline Arcana: Whenever you cast a spell of the charm subschool, increase the spell's DC by +2.

Bloodline Powers: The power of Elysium in your body and soul grants you supernatural charm and power for good, but the azata lords will see to it that you don't abuse them.

    Ray of Starlight (Sp): Starting at 1st level, you can unleash a ray of sparkling motes as a standard action, targeting any foe within 30 feet as a ranged touch attack. Against evil creatures, this ray deals 1d6 points of holy damage + 1 for every two sorcerer levels you possess. Neutral creatures take half damage from the ray, while good creatures are not damaged. You can use this ability a number of times per day equal to 3 + your Charisma modifier.

    Elysian Resistances (Ex): At 3rd level, you gain resist cold 5 and resist fire 5. At 9th level, your resistances increase to 10.

    Song of Heroes (Ex): At 9th level, you gain the ability to use bardic performance as a bard of your same level for a number of rounds per day equal to your sorcerer level, using any one Perform skill of your choice as your performance skill. You gain the following types of bardic performance: inspire courage, inspire greatness, and, at 15th level, inspire heroics. If you already possess bardic performance from another source, your sorcerer levels stack with your equivalent bard level for the purpose of determining your total number of performance rounds and the effects of the performances gained with this power.

    Resplendent Form (Su): At 15th level, once per day, you can assume a light form similar to that of a ghaele for 1 round per sorcerer level. In light form, you gain the incorporeal subtype. You only take half damage from corporeal sources as long as they are magic (you take no damage from non-magic weapons and objects). Likewise, your spells deal only half damage to corporeal creatures. Spells and other effects that do not deal damage function normally.

    Sovereign of Stars (Su): At 20th level, your soul is one with the power of Elysium. You gain immunity to electricity, mind-affecting effects, and petrification, and DR 15/cold iron or evil. Finally, you gain the ability to speak with any creature that has a language (as per the tongues spell).
 */
