using Kingmaker.Enums.Damage;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibramOfThePhoenix.New_Components
{
    internal class IncorporealDamageDivisorOutgoing : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateDamage>, IRulebookHandler<RuleCalculateDamage>, ISubscriber, ITargetRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateDamage evt)
        {
            foreach (BaseDamage baseDamage in evt.DamageBundle)
            {
                if (baseDamage.Reality != DamageRealityType.Ghost)
                {
                    PhysicalDamage physicalDamage = baseDamage as PhysicalDamage;
                    if (physicalDamage != null)
                    {
                        DamageDeclineType type = (physicalDamage.EnchantmentTotal > 0) ? DamageDeclineType.ByHalf : DamageDeclineType.Total;
                        baseDamage.AddDecline(new DamageDecline(type, base.Fact)
                        {
                            IncorporealDamageDivisor = true
                        });
                    }
                    else
                    {
                        EnergyDamage energyDamage = baseDamage as EnergyDamage;
                        if (energyDamage != null && energyDamage.EnergyType != DamageEnergyType.Holy && energyDamage.EnergyType != DamageEnergyType.Unholy && energyDamage.EnergyType != DamageEnergyType.PositiveEnergy && energyDamage.EnergyType != DamageEnergyType.NegativeEnergy && energyDamage.EnergyType != DamageEnergyType.Divine)
                        {
                            baseDamage.AddDecline(new DamageDecline(DamageDeclineType.ByHalf, base.Fact)
                            {
                                IncorporealDamageDivisor = true
                            });
                        }
                    }
                }
            }
        }

        public void OnEventDidTrigger(RuleCalculateDamage evt)
        {
           
        }
    }
}
