using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibramOfThePhoenix.Bugfixes
{
    internal class FixExtraHits
    {
        public static void FixRandomWeaponsRiders()
        {
            if (Settings.IsEnabled("FixExtraHitsSmallDragon"))
            {
                BuffConfigurator smdbuff = BuffConfigurator.For("d37d0c19b37808d4895c836c474f04e3");
                smdbuff.RemoveComponents(x => x is AddInitiatorAttackWithWeaponTrigger);

                ContextDiceValue size = new ContextDiceValue() { BonusValue = 0, DiceCountValue = 1, DiceType = Kingmaker.RuleSystem.DiceType.D4 };
                smdbuff.AdditionalDiceOnAttack(distanceLessEqual: new Kingmaker.Utility.Feet(), damageType: new DamageTypeDescription()
                {
                    Type = DamageType.Energy,
                    Energy = DamageEnergyType.Fire
                }, randomizeDamage: true, value: new ContextDiceValue()
                {
                    BonusValue = 0,
                    DiceCountValue = 1,
                    DiceType = Kingmaker.RuleSystem.DiceType.D4
                }, damageEntries: new List<AdditionalDiceOnAttack.DamageEntry>() { new AdditionalDiceOnAttack.DamageEntry() { DamageType = new DamageTypeDescription() { Type = DamageType.Energy, Energy = DamageEnergyType.Fire }, Value = size }, new AdditionalDiceOnAttack.DamageEntry() { DamageType = new DamageTypeDescription() { Type = DamageType.Energy, Energy = DamageEnergyType.Cold }, Value = size }, new AdditionalDiceOnAttack.DamageEntry() { DamageType = new DamageTypeDescription() { Type = DamageType.Energy, Energy = DamageEnergyType.Acid }, Value = size }, new AdditionalDiceOnAttack.DamageEntry() { DamageType = new DamageTypeDescription() { Type = DamageType.Energy, Energy = DamageEnergyType.Electricity }, Value = size } });
                /* smdbuff.AddComponent(new RandomEnergyDamageOnWeaponAttack()
                 {
                     amount = new Kingmaker.RuleSystem.DiceFormula(1, Kingmaker.RuleSystem.DiceType.D4),
                     damageDescriptions = new DamageEnergyType[] { DamageEnergyType.Acid, DamageEnergyType.Fire, DamageEnergyType.Cold, DamageEnergyType.Electricity }

                 });*/
                //If I find hodos torch and it's busted, fix
                smdbuff.Configure();
            }
        }
    }
}
