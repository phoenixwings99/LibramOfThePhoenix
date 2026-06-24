using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Localization;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.Utilities;
using static TabletopTweaks.Core.MechanicsChanges.MetamagicExtention;

namespace LibramOfThePhoenix.New_Content.Spells
{
    internal class BurstOfRadiance
    {
        public static void Make()
        {
            var flare = BlueprintTools.GetBlueprint<BlueprintAbility>("39a602aa80cc96f4597778b6d4d49c0a");
            var icon = flare.Icon;
            var flarefx = flare.GetComponent<AbilitySpawnFx>();
            var burstDummy = AbilityConfigurator.NewSpell("BurstOfRadiance", Main.LotPContext.Blueprints.GetGUID("BurstOfRadiance").m_Guid.ToString(), SpellSchool.Evocation, true, SpellDescriptor.Good, SpellDescriptor.Blindness)
                .SetIcon(icon)
                .SetDisplayName("BurstOfRadiance")
                .SetDescription("BurstOfRadiance.Desc")
                .SetLocalizedSavingThrow(RefData.ReflexPartial)
                .Configure();
            if (Settings.IsDisabled("BurstOfRadiance"))
            {
                return;
            }
            ActionsBuilder burstAction = ActionsBuilder.New().Conditional(conditions: ConditionsBuilder.New().Alignment(Kingmaker.Enums.AlignmentComponent.Evil), ifTrue: ActionsBuilder.New().DealDamage(damageType: new Kingmaker.RuleSystem.Rules.Damage.DamageTypeDescription()
            {
                Type = Kingmaker.RuleSystem.Rules.Damage.DamageType.Energy,
                Energy = Kingmaker.Enums.Damage.DamageEnergyType.Divine

            }, new Kingmaker.UnitLogic.Mechanics.ContextDiceValue()
            {
                DiceType = Kingmaker.RuleSystem.DiceType.D4,
                DiceCountValue = new Kingmaker.UnitLogic.Mechanics.ContextValue()
                {
                    ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Rank,
                    ValueRank = Kingmaker.Enums.AbilityRankType.DamageDice
                },
                BonusValue = ContextValues.Constant(0)
            }, isAoE: true, halfIfSaved: false).ConditionalSaved(failed: ActionsBuilder.New().ApplyBuff("187f88d96a0ef464280706b63635f2af", ContextDuration.FixedDice(diceType: Kingmaker.RuleSystem.DiceType.D4)), succeed: ActionsBuilder.New().ApplyBuff("df6d1025da07524429afbae248845ecc", ContextDuration.FixedDice(diceType: Kingmaker.RuleSystem.DiceType.D4))));

            var burst = AbilityConfigurator.For("BurstOfRadiance")
                .AddToSpellLists(2, SpellList.Wizard, SpellList.Cleric, SpellList.Druid, SpellList.Warpriest, SpellList.Hunter)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                .SetType(AbilityType.Spell)
                .SetRange(AbilityRange.Close)
                .SetCanTargetPoint(true)
                .SetCanTargetEnemies(true)
                .SetCanTargetFriends(true)
                .SetSpellResistance(true)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Directional)
                .SetAvailableMetamagic(Metamagic.Quicken, Metamagic.Heighten, Metamagic.CompletelyNormal, Metamagic.Reach, Metamagic.Bolstered, Metamagic.Empower, Metamagic.Heighten, Metamagic.Maximize, Metamagic.Persistent, Metamagic.Selective, (Metamagic)CustomMetamagic.Flaring, (Metamagic)CustomMetamagic.Intensified, (Metamagic)CustomMetamagic.Piercing)
                       .AddAbilityEffectRunAction(savingThrowType: Kingmaker.EntitySystem.Stats.SavingThrowType.Reflex, actions: burstAction)
            .AddAbilityTargetsAround(radius: new Kingmaker.Utility.Feet(10f), targetType: Kingmaker.UnitLogic.Abilities.Components.TargetType.Any, spreadSpeed: new Kingmaker.Utility.Feet(10f))
            .AddContextRankConfig(ContextRankConfigs.CasterLevel(type: Kingmaker.Enums.AbilityRankType.DamageDice, max: 5))
            .AddAbilitySpawnFx(anchor: flarefx.Anchor, delay: flarefx.Delay, prefabLink: flarefx.PrefabLink)

            .AddCraftInfoComponent(aOEType: Kingmaker.Craft.CraftAOE.AOE, savingThrow: Kingmaker.Craft.CraftSavingThrow.Reflex, spellType: Kingmaker.Craft.CraftSpellType.Damage);
            var made = burst.Configure();


        }
    }
}
